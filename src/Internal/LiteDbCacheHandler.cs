using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Nefarius.HttpClient.LiteDbCache.Options;

namespace Nefarius.HttpClient.LiteDbCache.Internal;

/// <summary>
///     Pulls a cached <see cref="HttpResponseMessage" /> from a <see cref="LiteDatabase" /> cache instance, if available.
///     Also does housekeeping like scrubbing expired entries etc.
/// </summary>
internal sealed class LiteDbCacheHandler(
    IOptionsSnapshot<DatabaseInstanceOptions> options,
    string instanceName,
    ILogger<LiteDbCacheHandler> logger,
    LiteDbCacheDatabaseInstances instances)
    : DelegatingHandler
{
    private const string ContentFileName = "content.bin";

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // get instance options
        DatabaseInstanceOptions instanceOptions = options.Get(instanceName);
        LiteDbCacheEntryOptions entryOptions = instanceOptions.EntryOptions;

        // probe for request-specific cache options
        if (request.Options.TryGetValue(
                new HttpRequestOptionsKey<LiteDbCacheEntryOptions>(LiteDbCacheHttpRequestOptions.EntryOptions),
                out LiteDbCacheEntryOptions entryOpts))
        {
            logger.LogDebug("Request-specific caching options found, overriding global options");

            // if a request-specific options set exists it takes priority over the global one
            entryOptions = entryOpts;
        }

        // check for URI exclusion
        if (entryOptions.UriExclusionRegex is not null &&
            request.RequestUri is not null &&
            entryOptions.UriExclusionRegex.IsMatch(request.RequestUri.ToString()))
        {
            logger.LogDebug("{@Request} excluded from caching as per {Regex}", request,
                entryOptions.UriExclusionRegex);

            return await base.SendAsync(request, cancellationToken);
        }

        LiteDatabase db = instances.GetOrCreateDatabase(instanceName);
        ILiteStorage<string> fs = db.FileStorage;

        ILiteCollection<CachedHttpResponseMessage> col =
            db.GetCollection<CachedHttpResponseMessage>(instanceOptions.CollectionName);

        string requestKey = await request.ToCacheKey(cancellationToken);

        // probe cache
        CachedHttpResponseMessage cacheEntry = col.FindOne(message => message.Key == requestKey);

        // cache hit
        if (cacheEntry is not null)
        {
            logger.LogDebug("Cached entry found for {@Request}", request);

            // make sure the fetched entry can be deserialized properly (might cause issues on major version upgrades)
            if (cacheEntry.SchemaVersion != CachedHttpResponseMessage.CurrentSchemaVersion)
            {
                logger.LogDebug("Schema version {EntrySchema} differs from {CurrentSchema}, invalidating entry",
                    cacheEntry.SchemaVersion, CachedHttpResponseMessage.CurrentSchemaVersion);
                if (!string.IsNullOrEmpty(cacheEntry.ContentFileId))
                {
                    fs.Delete(cacheEntry.ContentFileId);
                }

                col.Delete(cacheEntry.Id);
                goto fetch;
            }

            // we get the content type only after the last request was cached, if in exclusion, ignore and fetch again
            if (entryOptions.ExcludedContentTypes.Any(x =>
                    x.Equals(cacheEntry.ContentType, StringComparison.OrdinalIgnoreCase)))
            {
                logger.LogDebug("Request content type {ContentType} excluded for {CacheEntry}",
                    cacheEntry.ContentType, cacheEntry);
                if (!string.IsNullOrEmpty(cacheEntry.ContentFileId))
                {
                    fs.Delete(cacheEntry.ContentFileId);
                }

                col.Delete(cacheEntry.Id);
                goto fetch;
            }

            // absolute lifetime expired
            if (entryOptions.AbsoluteExpiration is not null &&
                entryOptions.AbsoluteExpiration <= DateTimeOffset.UtcNow)
            {
                logger.LogDebug("Absolute lifetime {AbsoluteExpiration} expired for {CacheEntry}",
                    entryOptions.AbsoluteExpiration, cacheEntry);
                if (!string.IsNullOrEmpty(cacheEntry.ContentFileId))
                {
                    fs.Delete(cacheEntry.ContentFileId);
                }

                col.Delete(cacheEntry.Id);
                goto fetch;
            }

            // absolute period has been reached
            if (entryOptions.AbsoluteExpirationRelativeToNow is not null &&
                cacheEntry.CreatedAt.Add(entryOptions.AbsoluteExpirationRelativeToNow.Value) <= DateTimeOffset.UtcNow)
            {
                logger.LogDebug("Absolute lifetime period {AbsoluteExpirationRelativeToNow} expired for {CacheEntry}",
                    entryOptions.AbsoluteExpirationRelativeToNow, cacheEntry);
                if (!string.IsNullOrEmpty(cacheEntry.ContentFileId))
                {
                    fs.Delete(cacheEntry.ContentFileId);
                }

                col.Delete(cacheEntry.Id);
                goto fetch;
            }

            // sliding period expired
            if (entryOptions.SlidingExpiration is not null &&
                cacheEntry.LastAccessedAt is not null &&
                cacheEntry.LastAccessedAt.Value.Add(entryOptions.SlidingExpiration.Value) <= DateTimeOffset.UtcNow)
            {
                logger.LogDebug("Sliding lifetime period {SlidingExpiration} expired for {CacheEntry}",
                    entryOptions.SlidingExpiration, cacheEntry);
                if (!string.IsNullOrEmpty(cacheEntry.ContentFileId))
                {
                    fs.Delete(cacheEntry.ContentFileId);
                }

                col.Delete(cacheEntry.Id);
                goto fetch;
            }

            // update metadata
            cacheEntry.LastAccessedAt = DateTimeOffset.UtcNow;
            col.Update(cacheEntry);

            // deserialize cached response
            HttpResponseMessage cachedResponse = request.CreateResponse(cacheEntry.StatusCode);

            // we should have a body...
            if (!string.IsNullOrEmpty(cacheEntry.ContentFileId))
            {
                LiteFileInfo<string> file = fs.FindById(cacheEntry.ContentFileId);

                // ...we indeed have a cached file!
                if (file is not null)
                {
                    cachedResponse.Content = new StreamContent(file.OpenRead());
                }
            }

            // clone headers (might be empty)
            foreach ((string key, List<string> value) in cacheEntry.Headers)
            {
                cachedResponse.Headers.Add(key, value);
            }

            // add extra info for the caller to check if it was pulled from cache
            cachedResponse.Headers.Add(LiteDbCacheHeaders.CacheHit, true.ToString());
            cachedResponse.Headers.Add(LiteDbCacheHeaders.CacheId, cacheEntry.Id.ToString());
            cachedResponse.Headers.Add(LiteDbCacheHeaders.CacheInstance, instanceName);
            cachedResponse.Headers.Add(LiteDbCacheHeaders.CacheCreatedAt,
                cacheEntry.CreatedAt.ToString("o", CultureInfo.InvariantCulture));

            return cachedResponse;
        }

        fetch:

        logger.LogDebug("Sending request to remote target for {@Request}", request);

        // cache miss, send request
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        // skip cache if unsuccessful and configured to skip
        if (!response.IsSuccessStatusCode && !instanceOptions.EntryOptions.CacheErrors)
        {
            logger.LogDebug("Remote request didn't succeed, skipping caching {@Request}", request);
            return response;
        }

        MemoryStream responseMs = new();
        await response.Content.CopyToAsync(responseMs, cancellationToken);

        // copy response to cache-able item
        cacheEntry = new CachedHttpResponseMessage
        {
            Key = requestKey,
            SchemaVersion = CachedHttpResponseMessage.CurrentSchemaVersion,
            StatusCode = response.StatusCode,
            ContentType = response.Content.Headers.ContentType?.ToString()
        };

        // clone headers only if desired
        if (entryOptions.CacheResponseHeaders)
        {
            cacheEntry.Headers = response.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList());
        }

        // omit content, if configured
        if (entryOptions.CacheResponseContent)
        {
            responseMs.Position = 0;
            LiteFileInfo<string> file = fs.Upload(requestKey, ContentFileName, responseMs);
            cacheEntry.ContentFileId = file.Id;
        }

        col.Insert(cacheEntry);

        logger.LogDebug("Added new cached entry {Entry}", cacheEntry);

        // rewind and replace original stream
        responseMs.Position = 0;
        response.Content = new StreamContent(responseMs);

        return response;
    }
}