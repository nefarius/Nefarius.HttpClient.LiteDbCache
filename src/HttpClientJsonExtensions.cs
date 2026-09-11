#nullable enable

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

using Nefarius.HttpClient.LiteDbCache.Options;

using NetHttpClient = global::System.Net.Http.HttpClient;

namespace Nefarius.HttpClient.LiteDbCache;

/// <summary>
///     Cache-aware <see cref="System.Net.Http.Json.HttpClientJsonExtensions" /> overloads that attach
///     <see cref="LiteDbCacheEntryOptions" /> to the outgoing request.
/// </summary>
public static class HttpClientJsonExtensions
{
    /// <summary>
    ///     Sends a GET request and deserializes the JSON response using request-specific cache options.
    /// </summary>
    public static Task<TValue?> GetFromJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return GetFromJsonAsync<TValue>(client, requestUri, cacheOptions, options: null, cancellationToken);
    }

    /// <summary>
    ///     Sends a GET request and deserializes the JSON response using request-specific cache options.
    /// </summary>
    public static Task<TValue?> GetFromJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return GetFromJsonAsync<TValue>(client, requestUri, cacheOptions, options: null, cancellationToken);
    }

    /// <summary>
    ///     Sends a GET request and deserializes the JSON response using request-specific cache options.
    /// </summary>
    public static Task<TValue?> GetFromJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options,
        CancellationToken cancellationToken = default)
    {
        return GetFromJsonAsyncCore<TValue>(client, HttpMethod.Get, requestUri, cacheOptions, options,
            cancellationToken);
    }

    /// <summary>
    ///     Sends a GET request and deserializes the JSON response using request-specific cache options.
    /// </summary>
    public static Task<TValue?> GetFromJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options,
        CancellationToken cancellationToken = default)
    {
        return GetFromJsonAsyncCore<TValue>(client, HttpMethod.Get, requestUri, cacheOptions, options,
            cancellationToken);
    }

    /// <summary>
    ///     Sends a GET request and deserializes the JSON response using request-specific cache options.
    /// </summary>
    public static Task<TValue?> GetFromJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo,
        CancellationToken cancellationToken = default)
    {
        return GetFromJsonAsyncCore(client, HttpMethod.Get, requestUri, cacheOptions, jsonTypeInfo, cancellationToken);
    }

    /// <summary>
    ///     Sends a GET request and deserializes the JSON response using request-specific cache options.
    /// </summary>
    public static Task<TValue?> GetFromJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo,
        CancellationToken cancellationToken = default)
    {
        return GetFromJsonAsyncCore(client, HttpMethod.Get, requestUri, cacheOptions, jsonTypeInfo, cancellationToken);
    }

    /// <summary>
    ///     Sends a DELETE request and deserializes the JSON response using request-specific cache options.
    /// </summary>
    public static Task<TValue?> DeleteFromJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return DeleteFromJsonAsync<TValue>(client, requestUri, cacheOptions, options: null, cancellationToken);
    }

    /// <summary>
    ///     Sends a DELETE request and deserializes the JSON response using request-specific cache options.
    /// </summary>
    public static Task<TValue?> DeleteFromJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return DeleteFromJsonAsync<TValue>(client, requestUri, cacheOptions, options: null, cancellationToken);
    }

    /// <summary>
    ///     Sends a DELETE request and deserializes the JSON response using request-specific cache options.
    /// </summary>
    public static Task<TValue?> DeleteFromJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options,
        CancellationToken cancellationToken = default)
    {
        return GetFromJsonAsyncCore<TValue>(client, HttpMethod.Delete, requestUri, cacheOptions, options,
            cancellationToken);
    }

    /// <summary>
    ///     Sends a DELETE request and deserializes the JSON response using request-specific cache options.
    /// </summary>
    public static Task<TValue?> DeleteFromJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options,
        CancellationToken cancellationToken = default)
    {
        return GetFromJsonAsyncCore<TValue>(client, HttpMethod.Delete, requestUri, cacheOptions, options,
            cancellationToken);
    }

    /// <summary>
    ///     Sends a DELETE request and deserializes the JSON response using request-specific cache options.
    /// </summary>
    public static Task<TValue?> DeleteFromJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo,
        CancellationToken cancellationToken = default)
    {
        return GetFromJsonAsyncCore(client, HttpMethod.Delete, requestUri, cacheOptions, jsonTypeInfo,
            cancellationToken);
    }

    /// <summary>
    ///     Sends a DELETE request and deserializes the JSON response using request-specific cache options.
    /// </summary>
    public static Task<TValue?> DeleteFromJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo,
        CancellationToken cancellationToken = default)
    {
        return GetFromJsonAsyncCore(client, HttpMethod.Delete, requestUri, cacheOptions, jsonTypeInfo,
            cancellationToken);
    }

    /// <summary>
    ///     Sends a POST request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return PostAsJsonAsync(client, requestUri, value, cacheOptions, options: null, cancellationToken);
    }

    /// <summary>
    ///     Sends a POST request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return PostAsJsonAsync(client, requestUri, value, cacheOptions, options: null, cancellationToken);
    }

    /// <summary>
    ///     Sends a POST request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options,
        CancellationToken cancellationToken = default)
    {
        return SendAsJsonAsync(client, HttpMethod.Post, requestUri, value, cacheOptions, options, cancellationToken);
    }

    /// <summary>
    ///     Sends a POST request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options,
        CancellationToken cancellationToken = default)
    {
        return SendAsJsonAsync(client, HttpMethod.Post, requestUri, value, cacheOptions, options, cancellationToken);
    }

    /// <summary>
    ///     Sends a POST request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo,
        CancellationToken cancellationToken = default)
    {
        return SendAsJsonAsync(client, HttpMethod.Post, requestUri, value, cacheOptions, jsonTypeInfo,
            cancellationToken);
    }

    /// <summary>
    ///     Sends a POST request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo,
        CancellationToken cancellationToken = default)
    {
        return SendAsJsonAsync(client, HttpMethod.Post, requestUri, value, cacheOptions, jsonTypeInfo,
            cancellationToken);
    }

    /// <summary>
    ///     Sends a PUT request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PutAsJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return PutAsJsonAsync(client, requestUri, value, cacheOptions, options: null, cancellationToken);
    }

    /// <summary>
    ///     Sends a PUT request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PutAsJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return PutAsJsonAsync(client, requestUri, value, cacheOptions, options: null, cancellationToken);
    }

    /// <summary>
    ///     Sends a PUT request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PutAsJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options,
        CancellationToken cancellationToken = default)
    {
        return SendAsJsonAsync(client, HttpMethod.Put, requestUri, value, cacheOptions, options, cancellationToken);
    }

    /// <summary>
    ///     Sends a PUT request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PutAsJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options,
        CancellationToken cancellationToken = default)
    {
        return SendAsJsonAsync(client, HttpMethod.Put, requestUri, value, cacheOptions, options, cancellationToken);
    }

    /// <summary>
    ///     Sends a PUT request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PutAsJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo,
        CancellationToken cancellationToken = default)
    {
        return SendAsJsonAsync(client, HttpMethod.Put, requestUri, value, cacheOptions, jsonTypeInfo, cancellationToken);
    }

    /// <summary>
    ///     Sends a PUT request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PutAsJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo,
        CancellationToken cancellationToken = default)
    {
        return SendAsJsonAsync(client, HttpMethod.Put, requestUri, value, cacheOptions, jsonTypeInfo, cancellationToken);
    }

    /// <summary>
    ///     Sends a PATCH request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return PatchAsJsonAsync(client, requestUri, value, cacheOptions, options: null, cancellationToken);
    }

    /// <summary>
    ///     Sends a PATCH request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken = default)
    {
        return PatchAsJsonAsync(client, requestUri, value, cacheOptions, options: null, cancellationToken);
    }

    /// <summary>
    ///     Sends a PATCH request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options,
        CancellationToken cancellationToken = default)
    {
        return SendAsJsonAsync(client, HttpMethod.Patch, requestUri, value, cacheOptions, options, cancellationToken);
    }

    /// <summary>
    ///     Sends a PATCH request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options,
        CancellationToken cancellationToken = default)
    {
        return SendAsJsonAsync(client, HttpMethod.Patch, requestUri, value, cacheOptions, options, cancellationToken);
    }

    /// <summary>
    ///     Sends a PATCH request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this NetHttpClient client, string? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo,
        CancellationToken cancellationToken = default)
    {
        return SendAsJsonAsync(client, HttpMethod.Patch, requestUri, value, cacheOptions, jsonTypeInfo,
            cancellationToken);
    }

    /// <summary>
    ///     Sends a PATCH request with a JSON body using request-specific cache options.
    /// </summary>
    public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this NetHttpClient client, Uri? requestUri,
        TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo,
        CancellationToken cancellationToken = default)
    {
        return SendAsJsonAsync(client, HttpMethod.Patch, requestUri, value, cacheOptions, jsonTypeInfo,
            cancellationToken);
    }

    private static Task<TValue?> GetFromJsonAsyncCore<TValue>(NetHttpClient client, HttpMethod method, string? requestUri,
        LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options, CancellationToken cancellationToken)
    {
        return ReadJsonResponseAsync<TValue>(
            client.SendAsync(new HttpRequestMessage(method, requestUri),
                HttpCompletionOption.ResponseHeadersRead, cacheOptions, cancellationToken),
            options, cancellationToken);
    }

    private static Task<TValue?> GetFromJsonAsyncCore<TValue>(NetHttpClient client, HttpMethod method, Uri? requestUri,
        LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options, CancellationToken cancellationToken)
    {
        return ReadJsonResponseAsync<TValue>(
            client.SendAsync(new HttpRequestMessage(method, requestUri),
                HttpCompletionOption.ResponseHeadersRead, cacheOptions, cancellationToken),
            options, cancellationToken);
    }

    private static Task<TValue?> GetFromJsonAsyncCore<TValue>(NetHttpClient client, HttpMethod method, string? requestUri,
        LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(jsonTypeInfo);

        return ReadJsonResponseAsync(
            client.SendAsync(new HttpRequestMessage(method, requestUri),
                HttpCompletionOption.ResponseHeadersRead, cacheOptions, cancellationToken),
            jsonTypeInfo, cancellationToken);
    }

    private static Task<TValue?> GetFromJsonAsyncCore<TValue>(NetHttpClient client, HttpMethod method, Uri? requestUri,
        LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(jsonTypeInfo);

        return ReadJsonResponseAsync(
            client.SendAsync(new HttpRequestMessage(method, requestUri),
                HttpCompletionOption.ResponseHeadersRead, cacheOptions, cancellationToken),
            jsonTypeInfo, cancellationToken);
    }

    private static Task<HttpResponseMessage> SendAsJsonAsync<TValue>(NetHttpClient client, HttpMethod method,
        string? requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options,
        CancellationToken cancellationToken)
    {
        return client.SendAsync(new HttpRequestMessage(method, requestUri)
        {
            Content = JsonContent.Create(value, mediaType: null, options)
        }, cacheOptions, cancellationToken);
    }

    private static Task<HttpResponseMessage> SendAsJsonAsync<TValue>(NetHttpClient client, HttpMethod method,
        Uri? requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions? options,
        CancellationToken cancellationToken)
    {
        return client.SendAsync(new HttpRequestMessage(method, requestUri)
        {
            Content = JsonContent.Create(value, mediaType: null, options)
        }, cacheOptions, cancellationToken);
    }

    private static Task<HttpResponseMessage> SendAsJsonAsync<TValue>(NetHttpClient client, HttpMethod method,
        string? requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(jsonTypeInfo);

        return client.SendAsync(new HttpRequestMessage(method, requestUri)
        {
            Content = CreateJsonContent(value, jsonTypeInfo)
        }, cacheOptions, cancellationToken);
    }

    private static Task<HttpResponseMessage> SendAsJsonAsync<TValue>(NetHttpClient client, HttpMethod method,
        Uri? requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(jsonTypeInfo);

        return client.SendAsync(new HttpRequestMessage(method, requestUri)
        {
            Content = CreateJsonContent(value, jsonTypeInfo)
        }, cacheOptions, cancellationToken);
    }

    private static async Task<TValue?> ReadJsonResponseAsync<TValue>(Task<HttpResponseMessage> responseTask,
        JsonSerializerOptions? options, CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await responseTask;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TValue>(options, cancellationToken);
    }

    private static async Task<TValue?> ReadJsonResponseAsync<TValue>(Task<HttpResponseMessage> responseTask,
        JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await responseTask;
        response.EnsureSuccessStatusCode();

        await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return await JsonSerializer.DeserializeAsync(stream, jsonTypeInfo, cancellationToken);
    }

    private static ByteArrayContent CreateJsonContent<TValue>(TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
    {
        ByteArrayContent content = new(JsonSerializer.SerializeToUtf8Bytes(value, jsonTypeInfo));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json")
        {
            CharSet = Encoding.UTF8.WebName
        };
        return content;
    }
}
