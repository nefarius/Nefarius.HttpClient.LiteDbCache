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
        if (request.Options.TryGetValue(LiteDbCacheHttpRequestOptions.EntryOptionsKey,
                out LiteDbCacheEntryOptions entryOpts) &&
            entryOpts is not null)
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
        CachedHttpResponseMessage staleCandidate = null;

        // cache hit
        if (cacheEntry is not null)
        {
            logger.LogDebug("Cached entry found for {@Request}", request);

            DateTimeOffset now = DateTimeOffset.UtcNow;

            // make sure the fetched entry can be deserialized properly (might cause issues on major version upgrades)
            if (cacheEntry.SchemaVersion != CachedHttpResponseMessage.CurrentSchemaVersion)
            {
                logger.LogDebug("Schema version {EntrySchema} differs from {CurrentSchema}, invalidating entry",
                    cacheEntry.SchemaVersion, CachedHttpResponseMessage.CurrentSchemaVersion);
                DeleteCacheEntry(cacheEntry, fs, col);
                goto fetch;
            }

            // we get the content type only after the last request was cached, if in exclusion, ignore and fetch again
            if (entryOptions.ExcludedContentTypes.Any(x =>
                    x.Equals(cacheEntry.ContentType, StringComparison.OrdinalIgnoreCase)))
            {
                logger.LogDebug("Request content type {ContentType} excluded for {CacheEntry}",
                    cacheEntry.ContentType, cacheEntry);
                DeleteCacheEntry(cacheEntry, fs, col);
                goto fetch;
            }

            if (IsEntryExpired(cacheEntry, entryOptions, now, logger))
            {
                if (entryOptions.ServeStaleOnError)
                {
                    logger.LogDebug("Keeping expired {CacheEntry} as stale fallback", cacheEntry);
                    staleCandidate = cacheEntry;
                }
                else
                {
                    DeleteCacheEntry(cacheEntry, fs, col);
                }

                goto fetch;
            }

            // update metadata
            cacheEntry.LastAccessedAt = now;
            col.Update(cacheEntry);

            return CreateCachedResponse(request, cacheEntry, fs, false);
        }

        fetch:

        logger.LogDebug("Sending request to remote target for {@Request}", request);

        HttpResponseMessage response;

        try
        {
            // cache miss or expired, send request
            response = await base.SendAsync(request, cancellationToken);
        }
        catch (Exception ex) when (ShouldServeStale(ex, staleCandidate, entryOptions, cancellationToken))
        {
            logger.LogDebug(ex, "Remote request failed, serving stale {CacheEntry}", staleCandidate);
            return CreateCachedResponse(request, staleCandidate, fs, true);
        }

        // skip cache if unsuccessful and configured to skip
        if (!response.IsSuccessStatusCode && !entryOptions.CacheErrors)
        {
            if (staleCandidate is not null && entryOptions.ServeStaleOnError)
            {
                logger.LogDebug("Remote request didn't succeed, serving stale {CacheEntry}", staleCandidate);
                response.Dispose();
                return CreateCachedResponse(request, staleCandidate, fs, true);
            }

            logger.LogDebug("Remote request didn't succeed, skipping caching {@Request}", request);
            return response;
        }

        MemoryStream responseMs = await BufferContentAsync(response, cancellationToken);

        DateTimeOffset fetchedAt = DateTimeOffset.UtcNow;
        DateTimeOffset? upstreamExpiresAt = null;

        if (entryOptions.HonorCacheControl)
        {
            CacheabilityDecision decision = CacheControlEvaluator.Evaluate(response, fetchedAt);
            if (!decision.CanCache)
            {
                logger.LogDebug("Upstream cache directives forbid storing {@Request}", request);
                if (staleCandidate is not null)
                {
                    DeleteCacheEntry(staleCandidate, fs, col);
                }

                return response;
            }

            upstreamExpiresAt = decision.ExpiresAt;
        }

        if (staleCandidate is not null)
        {
            DeleteCacheEntry(staleCandidate, fs, col);
        }

        // copy response to cache-able item
        cacheEntry = new CachedHttpResponseMessage
        {
            Key = requestKey,
            SchemaVersion = CachedHttpResponseMessage.CurrentSchemaVersion,
            StatusCode = response.StatusCode,
            ContentType = response.Content.Headers.ContentType?.ToString(),
            UpstreamExpiresAt = upstreamExpiresAt
        };

        // clone headers only if desired
        if (entryOptions.CacheResponseHeaders)
        {
            cacheEntry.Headers = CloneHeaders(response.Headers);
        }

        // omit content, if configured
        if (entryOptions.CacheResponseContent)
        {
            cacheEntry.ContentHeaders = CloneHeaders(response.Content.Headers);
            responseMs.Position = 0;
            LiteFileInfo<string> file = fs.Upload(requestKey, ContentFileName, responseMs);
            cacheEntry.ContentFileId = file.Id;
            responseMs.Position = 0;
        }

        col.Insert(cacheEntry);

        logger.LogDebug("Added new cached entry {Entry}", cacheEntry);

        return response;
    }

    private static bool IsEntryExpired(CachedHttpResponseMessage cacheEntry, LiteDbCacheEntryOptions entryOptions,
        DateTimeOffset now, ILogger logger)
    {
        // absolute lifetime expired
        if (entryOptions.AbsoluteExpiration is not null &&
            entryOptions.AbsoluteExpiration <= now)
        {
            logger.LogDebug("Absolute lifetime {AbsoluteExpiration} expired for {CacheEntry}",
                entryOptions.AbsoluteExpiration, cacheEntry);
            return true;
        }

        // absolute period has been reached
        if (entryOptions.AbsoluteExpirationRelativeToNow is not null &&
            cacheEntry.CreatedAt.Add(entryOptions.AbsoluteExpirationRelativeToNow.Value) <= now)
        {
            logger.LogDebug("Absolute lifetime period {AbsoluteExpirationRelativeToNow} expired for {CacheEntry}",
                entryOptions.AbsoluteExpirationRelativeToNow, cacheEntry);
            return true;
        }

        // sliding period expired
        if (entryOptions.SlidingExpiration is not null &&
            cacheEntry.LastAccessedAt is not null &&
            cacheEntry.LastAccessedAt.Value.Add(entryOptions.SlidingExpiration.Value) <= now)
        {
            logger.LogDebug("Sliding lifetime period {SlidingExpiration} expired for {CacheEntry}",
                entryOptions.SlidingExpiration, cacheEntry);
            return true;
        }

        if (entryOptions.HonorCacheControl &&
            cacheEntry.UpstreamExpiresAt is not null &&
            cacheEntry.UpstreamExpiresAt <= now)
        {
            logger.LogDebug("Upstream freshness lifetime {UpstreamExpiresAt} expired for {CacheEntry}",
                cacheEntry.UpstreamExpiresAt, cacheEntry);
            return true;
        }

        return false;
    }

    private static bool ShouldServeStale(Exception exception, CachedHttpResponseMessage staleCandidate,
        LiteDbCacheEntryOptions entryOptions, CancellationToken cancellationToken)
    {
        if (staleCandidate is null || !entryOptions.ServeStaleOnError || cancellationToken.IsCancellationRequested)
        {
            return false;
        }

        return exception is HttpRequestException or IOException or OperationCanceledException;
    }

    private HttpResponseMessage CreateCachedResponse(HttpRequestMessage request,
        CachedHttpResponseMessage cacheEntry, ILiteStorage<string> fs, bool stale)
    {
        HttpResponseMessage cachedResponse = request.CreateResponse(cacheEntry.StatusCode);

        if (!string.IsNullOrEmpty(cacheEntry.ContentFileId))
        {
            LiteFileInfo<string> file = fs.FindById(cacheEntry.ContentFileId);

            if (file is not null)
            {
                cachedResponse.Content = new StreamContent(file.OpenRead());
            }
        }

        ApplyHeaders(cachedResponse.Headers, cacheEntry.Headers);
        ApplyHeaders(cachedResponse.Content.Headers, cacheEntry.ContentHeaders);

        cachedResponse.Headers.Add(LiteDbCacheHeaders.CacheHit, true.ToString());
        cachedResponse.Headers.Add(LiteDbCacheHeaders.CacheId, cacheEntry.Id.ToString());
        cachedResponse.Headers.Add(LiteDbCacheHeaders.CacheInstance, instanceName);
        cachedResponse.Headers.Add(LiteDbCacheHeaders.CacheCreatedAt,
            cacheEntry.CreatedAt.ToString("o", CultureInfo.InvariantCulture));

        if (stale)
        {
            cachedResponse.Headers.Add(LiteDbCacheHeaders.CacheStale, true.ToString());
        }

        return cachedResponse;
    }

    private static async Task<MemoryStream> BufferContentAsync(HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        HttpContent originalContent = response.Content;
        Dictionary<string, List<string>> contentHeaders = CloneHeaders(originalContent.Headers);

        MemoryStream responseMs = new();
        await originalContent.CopyToAsync(responseMs, cancellationToken);
        originalContent.Dispose();

        responseMs.Position = 0;
        StreamContent bufferedContent = new(responseMs);
        ApplyHeaders(bufferedContent.Headers, contentHeaders);
        response.Content = bufferedContent;

        return responseMs;
    }

    private static Dictionary<string, List<string>> CloneHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
    {
        return headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList());
    }

    private static void ApplyHeaders(System.Net.Http.Headers.HttpHeaders target,
        Dictionary<string, List<string>> headers)
    {
        foreach ((string key, List<string> value) in headers)
        {
            target.TryAddWithoutValidation(key, value);
        }
    }

    private static void DeleteCacheEntry(CachedHttpResponseMessage cacheEntry, ILiteStorage<string> fs,
        ILiteCollection<CachedHttpResponseMessage> col)
    {
        if (!string.IsNullOrEmpty(cacheEntry.ContentFileId))
        {
            fs.Delete(cacheEntry.ContentFileId);
        }

        col.Delete(cacheEntry.Id);
    }
}
