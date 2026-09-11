#nullable enable

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Nefarius.HttpClient.LiteDbCache.Options;

using NetHttpClient = global::System.Net.Http.HttpClient;

namespace Nefarius.HttpClient.LiteDbCache;

/// <summary>
///     Cache-aware <see cref="System.Net.Http.HttpClient" /> verb overloads that attach
///     <see cref="LiteDbCacheEntryOptions" /> to the outgoing request.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    ///     Sends <paramref name="request" /> after attaching <paramref name="cacheOptions" />.
    /// </summary>
    public static Task<HttpResponseMessage> SendAsync(this NetHttpClient client, HttpRequestMessage request,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return SendCore(client, request, cacheOptions, null, cancellationToken);
    }

    /// <summary>
    ///     Sends <paramref name="request" /> after attaching <paramref name="cacheOptions" />.
    /// </summary>
    public static Task<HttpResponseMessage> SendAsync(this NetHttpClient client, HttpRequestMessage request,
        HttpCompletionOption completionOption, LiteDbCacheEntryOptions cacheOptions,
        CancellationToken cancellationToken = default)
    {
        return SendCore(client, request, cacheOptions, completionOption, cancellationToken);
    }

    /// <summary>
    ///     Sends a GET request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> GetAsync(this NetHttpClient client, string? requestUri,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Get, requestUri, null, cacheOptions, null, cancellationToken);
    }

    /// <summary>
    ///     Sends a GET request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> GetAsync(this NetHttpClient client, Uri? requestUri,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Get, requestUri, null, cacheOptions, null, cancellationToken);
    }

    /// <summary>
    ///     Sends a GET request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> GetAsync(this NetHttpClient client, string? requestUri,
        HttpCompletionOption completionOption, LiteDbCacheEntryOptions cacheOptions,
        CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Get, requestUri, null, cacheOptions, completionOption, cancellationToken);
    }

    /// <summary>
    ///     Sends a GET request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> GetAsync(this NetHttpClient client, Uri? requestUri,
        HttpCompletionOption completionOption, LiteDbCacheEntryOptions cacheOptions,
        CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Get, requestUri, null, cacheOptions, completionOption, cancellationToken);
    }

    /// <summary>
    ///     Sends a HEAD request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> HeadAsync(this NetHttpClient client, string? requestUri,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Head, requestUri, null, cacheOptions, null, cancellationToken);
    }

    /// <summary>
    ///     Sends a HEAD request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> HeadAsync(this NetHttpClient client, Uri? requestUri,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Head, requestUri, null, cacheOptions, null, cancellationToken);
    }

    /// <summary>
    ///     Sends a HEAD request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> HeadAsync(this NetHttpClient client, string? requestUri,
        HttpCompletionOption completionOption, LiteDbCacheEntryOptions cacheOptions,
        CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Head, requestUri, null, cacheOptions, completionOption, cancellationToken);
    }

    /// <summary>
    ///     Sends a HEAD request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> HeadAsync(this NetHttpClient client, Uri? requestUri,
        HttpCompletionOption completionOption, LiteDbCacheEntryOptions cacheOptions,
        CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Head, requestUri, null, cacheOptions, completionOption, cancellationToken);
    }

    /// <summary>
    ///     Sends a DELETE request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> DeleteAsync(this NetHttpClient client, string? requestUri,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Delete, requestUri, null, cacheOptions, null, cancellationToken);
    }

    /// <summary>
    ///     Sends a DELETE request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> DeleteAsync(this NetHttpClient client, Uri? requestUri,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Delete, requestUri, null, cacheOptions, null, cancellationToken);
    }

    /// <summary>
    ///     Sends a POST request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PostAsync(this NetHttpClient client, string? requestUri, HttpContent? content,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Post, requestUri, content, cacheOptions, null, cancellationToken);
    }

    /// <summary>
    ///     Sends a POST request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PostAsync(this NetHttpClient client, Uri? requestUri, HttpContent? content,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Post, requestUri, content, cacheOptions, null, cancellationToken);
    }

    /// <summary>
    ///     Sends a PUT request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PutAsync(this NetHttpClient client, string? requestUri, HttpContent? content,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Put, requestUri, content, cacheOptions, null, cancellationToken);
    }

    /// <summary>
    ///     Sends a PUT request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PutAsync(this NetHttpClient client, Uri? requestUri, HttpContent? content,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Put, requestUri, content, cacheOptions, null, cancellationToken);
    }

    /// <summary>
    ///     Sends a PATCH request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PatchAsync(this NetHttpClient client, string? requestUri, HttpContent? content,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Patch, requestUri, content, cacheOptions, null, cancellationToken);
    }

    /// <summary>
    ///     Sends a PATCH request with request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PatchAsync(this NetHttpClient client, Uri? requestUri, HttpContent? content,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return Send(client, HttpMethod.Patch, requestUri, content, cacheOptions, null, cancellationToken);
    }

    private static Task<HttpResponseMessage> Send(NetHttpClient client, HttpMethod method, string? requestUri,
        HttpContent? content, LiteDbCacheEntryOptions cacheOptions, HttpCompletionOption? completionOption,
        CancellationToken cancellationToken)
    {
        return SendCore(client, CreateRequest(method, requestUri, content), cacheOptions, completionOption,
            cancellationToken);
    }

    private static Task<HttpResponseMessage> Send(NetHttpClient client, HttpMethod method, Uri? requestUri,
        HttpContent? content, LiteDbCacheEntryOptions cacheOptions, HttpCompletionOption? completionOption,
        CancellationToken cancellationToken)
    {
        return SendCore(client, CreateRequest(method, requestUri, content), cacheOptions, completionOption,
            cancellationToken);
    }

    private static Task<HttpResponseMessage> SendCore(NetHttpClient client, HttpRequestMessage request,
        LiteDbCacheEntryOptions cacheOptions, HttpCompletionOption? completionOption,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(cacheOptions);

        request.SetLiteDbCacheEntryOptions(cacheOptions);

        return completionOption is { } option
            ? client.SendAsync(request, option, cancellationToken)
            : client.SendAsync(request, cancellationToken);
    }

    private static HttpRequestMessage CreateRequest(HttpMethod method, string? requestUri, HttpContent? content)
    {
        return new HttpRequestMessage(method, requestUri) { Content = content };
    }

    private static HttpRequestMessage CreateRequest(HttpMethod method, Uri? requestUri, HttpContent? content)
    {
        return new HttpRequestMessage(method, requestUri) { Content = content };
    }
}
