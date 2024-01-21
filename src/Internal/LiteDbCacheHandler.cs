﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Nefarius.HttpClient.LiteDbCache.Options;

namespace Nefarius.HttpClient.LiteDbCache.Internal;

internal sealed class LiteDbCacheHandler : DelegatingHandler
{
    private readonly IOptionsSnapshot<DatabaseInstanceOptions> _options;
    private readonly string _instanceName;
    private readonly ILogger<LiteDbCacheHandler> _logger;
    private readonly CacheDatabaseInstances _instances;

    public LiteDbCacheHandler(IOptionsSnapshot<DatabaseInstanceOptions> options, string instanceName,
        ILogger<LiteDbCacheHandler> logger, CacheDatabaseInstances instances)
    {
        _options = options;
        _instanceName = instanceName;
        _logger = logger;
        _instances = instances;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // get instance options
        DatabaseInstanceOptions options = _options.Get(_instanceName);
        LiteDbCacheEntryOptions entryOptions = options.EntryOptions;

        LiteDatabase db = _instances.GetDatabase(_instanceName);

        ILiteCollection<CachedHttpResponseMessage> col =
            db.GetCollection<CachedHttpResponseMessage>(options.CollectionName);

        // probe cache
        CachedHttpResponseMessage cacheEntry = col.FindOne(message => message.Uri == request.RequestUri);

        // cache hit
        if (cacheEntry is not null)
        {
            _logger.LogDebug("Cached entry found for {@Request}", request);

            // absolute lifetime expired
            if (entryOptions.AbsoluteExpiration is not null &&
                entryOptions.AbsoluteExpiration <= DateTimeOffset.UtcNow)
            {
                _logger.LogDebug("Absolute lifetime {AbsoluteExpiration} expired for {CacheEntry}",
                    entryOptions.AbsoluteExpiration, cacheEntry);
                col.Delete(cacheEntry.Id);
                goto fetch;
            }

            // absolute period has been reached
            if (entryOptions.AbsoluteExpirationRelativeToNow is not null &&
                cacheEntry.CreatedAt.Add(entryOptions.AbsoluteExpirationRelativeToNow.Value) <= DateTimeOffset.UtcNow
               )
            {
                _logger.LogDebug("Absolute lifetime period {AbsoluteExpirationRelativeToNow} expired for {CacheEntry}",
                    entryOptions.AbsoluteExpirationRelativeToNow, cacheEntry);
                col.Delete(cacheEntry.Id);
                goto fetch;
            }

            // sliding period expired
            if (entryOptions.SlidingExpiration is not null &&
                cacheEntry.LastAccessedAt is not null &&
                cacheEntry.LastAccessedAt.Value.Add(entryOptions.SlidingExpiration.Value) <= DateTimeOffset.UtcNow
               )
            {
                _logger.LogDebug("Sliding lifetime period {SlidingExpiration} expired for {CacheEntry}",
                    entryOptions.SlidingExpiration, cacheEntry);
                col.Delete(cacheEntry.Id);
                goto fetch;
            }

            // update metadata
            cacheEntry.LastAccessedAt = DateTimeOffset.UtcNow;
            col.Update(cacheEntry);

            // deserialize cached response
            HttpResponseMessage cachedResponse = request.CreateResponse(cacheEntry.StatusCode);
            cachedResponse.Content = new ByteArrayContent(cacheEntry.Content);

            // clone headers
            foreach ((string key, List<string> value) in cacheEntry.Headers)
            {
                cachedResponse.Headers.Add(key, value);
            }

            // add extra info for the caller to check if it was pulled from cache
            cachedResponse.Headers.Add("X-LiteDb-Cache-Hit", true.ToString());
            cachedResponse.Headers.Add("X-LiteDb-Cache-Id", cacheEntry.Id.ToString());
            cachedResponse.Headers.Add("X-LiteDb-Cache-Instance", _instanceName);
            cachedResponse.Headers.Add("X-LiteDb-Cache-Created-At",
                cacheEntry.CreatedAt.ToString("o", CultureInfo.InvariantCulture));

            return cachedResponse;
        }

        fetch:

        _logger.LogDebug("Sending request to remote target for {@Request}", request);

        // cache miss, send request
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        // skip cache if unsuccessful and configured to skip
        if (!response.IsSuccessStatusCode && !options.EntryOptions.CacheErrors)
        {
            _logger.LogDebug("Remote request didn't succeed, skipping caching {@Request}", request);
            return response;
        }

        MemoryStream responseMs = new();
        await response.Content.CopyToAsync(responseMs, cancellationToken);

        // copy response to cache-able item
        cacheEntry =
            new CachedHttpResponseMessage
            {
                Uri = request.RequestUri,
                StatusCode = response.StatusCode,
                Content = responseMs.ToArray(),
                Headers = response.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList())
            };

        col.Insert(cacheEntry);
        
        _logger.LogDebug("Added new cached entry {Entry}", cacheEntry);

        // rewind and replace original stream
        responseMs.Position = 0;
        response.Content = new ByteArrayContent(responseMs.ToArray());

        return response;
    }
}