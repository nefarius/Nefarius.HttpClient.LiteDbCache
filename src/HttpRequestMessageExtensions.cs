#nullable enable

using System;
using System.Net.Http;

using Nefarius.HttpClient.LiteDbCache.Options;

namespace Nefarius.HttpClient.LiteDbCache;

/// <summary>
///     <see cref="HttpRequestMessage" /> extensions for LiteDB cache configuration.
/// </summary>
public static class HttpRequestMessageExtensions
{
    /// <summary>
    ///     Attaches request-specific <see cref="LiteDbCacheEntryOptions" /> that override the named client's defaults.
    /// </summary>
    /// <param name="request">The request to configure.</param>
    /// <param name="options">The cache options that apply to this request only.</param>
    /// <returns>The same <paramref name="request" /> for chaining.</returns>
    public static HttpRequestMessage SetLiteDbCacheEntryOptions(this HttpRequestMessage request,
        LiteDbCacheEntryOptions options)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(options);

        request.Options.Set(LiteDbCacheHttpRequestOptions.EntryOptionsKey, options);
        return request;
    }

    /// <summary>
    ///     Tries to read request-specific <see cref="LiteDbCacheEntryOptions" />.
    /// </summary>
    /// <param name="request">The request to inspect.</param>
    /// <param name="options">The attached options, if any.</param>
    /// <returns><see langword="true" /> when request-specific options are present.</returns>
    public static bool TryGetLiteDbCacheEntryOptions(this HttpRequestMessage request,
        out LiteDbCacheEntryOptions? options)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request.Options.TryGetValue(LiteDbCacheHttpRequestOptions.EntryOptionsKey, out options);
    }
}
