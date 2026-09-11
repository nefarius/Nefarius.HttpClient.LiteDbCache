using System.Net;

using Xunit;

using Nefarius.HttpClient.LiteDbCache;
using Nefarius.HttpClient.LiteDbCache.Options;

using NetHttpClient = global::System.Net.Http.HttpClient;

namespace Nefarius.HttpClient.LiteDbCache.Tests;

public class HttpClientExtensionsTests
{
    private static readonly LiteDbCacheEntryOptions CacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
    };

    [Theory]
    [InlineData("GET")]
    [InlineData("HEAD")]
    [InlineData("DELETE")]
    [InlineData("POST")]
    [InlineData("PUT")]
    [InlineData("PATCH")]
    public async Task VerbOverloads_ForwardMethodUriContentAndCacheOptions(string methodName)
    {
        HttpRequestMessage? captured = null;
        using NetHttpClient client = CreateClient(request =>
        {
            captured = request;
            return StubHttpHandler.Ok();
        });

        HttpMethod method = new(methodName);
        StringContent? content = NeedsContent(method) ? new StringContent("payload") : null;
        HttpResponseMessage response = await SendVerbAsync(client, method, "/resource", content, CacheOptions);

        Assert.NotNull(captured);
        Assert.Equal(method, captured!.Method);
        Assert.Equal("/resource", captured.RequestUri?.AbsolutePath);
        Assert.True(captured.TryGetLiteDbCacheEntryOptions(out LiteDbCacheEntryOptions? attached));
        Assert.Same(CacheOptions, attached);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        if (content is not null)
        {
            Assert.Equal("payload", await captured.Content!.ReadAsStringAsync());
        }
    }

    [Fact]
    public async Task UriOverloads_ForwardAbsoluteUri()
    {
        HttpRequestMessage? captured = null;
        using NetHttpClient client = CreateClient(request =>
        {
            captured = request;
            return StubHttpHandler.Ok();
        });

        Uri uri = new("https://cache.test/from-uri");
        await client.GetAsync(uri, CacheOptions);
        await client.PostAsync(uri, new StringContent("body"), CacheOptions);

        Assert.NotNull(captured);
        Assert.Equal(uri, captured!.RequestUri);
    }

    [Fact]
    public async Task SendAsync_AttachesOptionsToExistingRequest()
    {
        HttpRequestMessage? captured = null;
        using NetHttpClient client = CreateClient(request =>
        {
            captured = request;
            return StubHttpHandler.Ok();
        });

        HttpRequestMessage request = new(HttpMethod.Get, "/custom");
        request.Headers.Add("X-Test", "1");

        await client.SendAsync(request, CacheOptions);

        Assert.NotNull(captured);
        Assert.Equal("1", captured!.Headers.GetValues("X-Test").Single());
        Assert.True(captured.TryGetLiteDbCacheEntryOptions(out LiteDbCacheEntryOptions? attached));
        Assert.Same(CacheOptions, attached);
    }

    [Fact]
    public async Task CompletionOptionOverload_CompletesSuccessfully()
    {
        using NetHttpClient client = CreateClient();

        HttpResponseMessage response = await client.GetAsync("/resource",
            HttpCompletionOption.ResponseHeadersRead, CacheOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CancellationToken_IsHonored()
    {
        using NetHttpClient client = CreateClient();
        using CancellationTokenSource cts = new();
        await cts.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            client.GetAsync("/resource", CacheOptions, cts.Token));
    }

    [Fact]
    public async Task NullClient_Throws()
    {
        NetHttpClient client = null!;

        ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            client.GetAsync("/resource", CacheOptions));

        Assert.Equal("client", exception.ParamName);
    }

    [Fact]
    public async Task NullCacheOptions_Throws()
    {
        using NetHttpClient client = CreateClient();

        ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            client.GetAsync("/resource", null!));

        Assert.Equal("cacheOptions", exception.ParamName);
    }

    [Fact]
    public async Task NullRequest_Throws()
    {
        using NetHttpClient client = CreateClient();

        ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            client.SendAsync(null!, CacheOptions));

        Assert.Equal("request", exception.ParamName);
    }

    private static async Task<HttpResponseMessage> SendVerbAsync(NetHttpClient client, HttpMethod method,
        string requestUri, HttpContent? content, LiteDbCacheEntryOptions cacheOptions)
    {
        if (method == HttpMethod.Get)
        {
            return await client.GetAsync(requestUri, cacheOptions);
        }

        if (method == HttpMethod.Head)
        {
            return await client.HeadAsync(requestUri, cacheOptions);
        }

        if (method == HttpMethod.Delete)
        {
            return await client.DeleteAsync(requestUri, cacheOptions);
        }

        if (method == HttpMethod.Post)
        {
            return await client.PostAsync(requestUri, content, cacheOptions);
        }

        if (method == HttpMethod.Put)
        {
            return await client.PutAsync(requestUri, content, cacheOptions);
        }

        if (method == HttpMethod.Patch)
        {
            return await client.PatchAsync(requestUri, content, cacheOptions);
        }

        throw new ArgumentOutOfRangeException(nameof(method));
    }

    private static bool NeedsContent(HttpMethod method)
    {
        return method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Patch;
    }

    private static NetHttpClient CreateClient(Func<HttpRequestMessage, HttpResponseMessage>? responder = null)
    {
        StubHttpHandler stub = new(responder ?? (_ => StubHttpHandler.Ok()));
        return new NetHttpClient(stub) { BaseAddress = new Uri("https://cache.test") };
    }
}
