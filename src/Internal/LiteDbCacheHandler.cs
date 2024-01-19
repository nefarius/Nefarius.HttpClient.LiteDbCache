using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Microsoft.Extensions.Options;

namespace Nefarius.HttpClient.LiteDbCache.Internal;

internal sealed class LiteDbCacheHandler : DelegatingHandler
{
    private readonly IOptionsSnapshot<DatabaseInstanceOptions> _options;
    private readonly string _instanceName;

    public LiteDbCacheHandler(IOptionsSnapshot<DatabaseInstanceOptions> options, string instanceName)
    {
        _options = options;
        _instanceName = instanceName;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // get instance options
        DatabaseInstanceOptions options = _options.Get(_instanceName);

        using LiteDatabase db = new(options.ConnectionString);

        ILiteCollection<CachedHttpResponseMessage> col =
            db.GetCollection<CachedHttpResponseMessage>(options.CollectionName);

        CachedHttpResponseMessage cacheEntry = col.FindOne(message => message.Uri == request.RequestUri);

        // cache hit
        if (cacheEntry is not null)
        {
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

        // cache miss, send request
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

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

        // rewind and replace stream
        responseMs.Position = 0;
        response.Content = new ByteArrayContent(responseMs.ToArray());

        return response;
    }
}