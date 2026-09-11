using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

using Xunit;

using Nefarius.HttpClient.LiteDbCache;
using Nefarius.HttpClient.LiteDbCache.Options;

using NetHttpClient = global::System.Net.Http.HttpClient;

namespace Nefarius.HttpClient.LiteDbCache.Tests;

public class HttpClientJsonExtensionsTests
{
    private static readonly LiteDbCacheEntryOptions CacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
    };

    private static readonly JsonSerializerOptions WebJson = new(JsonSerializerDefaults.Web);

    [Fact]
    public async Task GetFromJsonAsync_DeserializesSuccessfulResponse()
    {
        using NetHttpClient client = CreateClient(_ => StubHttpHandler.Ok("""{"name":"cached"}""", "application/json"));

        JsonDto? dto = await client.GetFromJsonAsync<JsonDto>("/resource", CacheOptions);

        Assert.Equal("cached", dto?.Name);
    }

    [Fact]
    public async Task GetFromJsonAsync_UriAndSerializerOptions_Deserializes()
    {
        using NetHttpClient client = CreateClient(_ => StubHttpHandler.Ok("""{"name":"from-uri"}""", "application/json"));

        JsonDto? dto = await client.GetFromJsonAsync<JsonDto>(new Uri("https://cache.test/resource"), CacheOptions,
            WebJson);

        Assert.Equal("from-uri", dto?.Name);
    }

    [Fact]
    public async Task GetFromJsonAsync_JsonTypeInfo_Deserializes()
    {
        using NetHttpClient client = CreateClient(_ => StubHttpHandler.Ok("""{"name":"typed"}""", "application/json"));

        JsonDto? dto = await client.GetFromJsonAsync("/resource", CacheOptions, JsonDtoTypeInfo());

        Assert.Equal("typed", dto?.Name);
    }

    [Fact]
    public async Task DeleteFromJsonAsync_DeserializesSuccessfulResponse()
    {
        HttpRequestMessage? captured = null;
        using NetHttpClient client = CreateClient(request =>
        {
            captured = request;
            return StubHttpHandler.Ok("""{"name":"deleted"}""", "application/json");
        });

        JsonDto? dto = await client.DeleteFromJsonAsync<JsonDto>("/resource", CacheOptions);

        Assert.Equal(HttpMethod.Delete, captured!.Method);
        Assert.True(captured.TryGetLiteDbCacheEntryOptions(out LiteDbCacheEntryOptions? attached));
        Assert.Same(CacheOptions, attached);
        Assert.Equal("deleted", dto?.Name);
    }

    [Theory]
    [InlineData("POST")]
    [InlineData("PUT")]
    [InlineData("PATCH")]
    public async Task WriteAsJsonAsync_SerializesBodyAndAttachesOptions(string methodName)
    {
        HttpRequestMessage? captured = null;
        using NetHttpClient client = CreateClient(request =>
        {
            captured = request;
            return StubHttpHandler.Ok();
        });

        JsonDto payload = new() { Name = "payload" };
        HttpMethod method = new(methodName);
        await SendAsJsonAsync(client, method, "/resource", payload, CacheOptions);

        Assert.NotNull(captured);
        Assert.Equal(method, captured!.Method);
        Assert.Equal("application/json", captured.Content!.Headers.ContentType?.MediaType);
        Assert.True(captured.TryGetLiteDbCacheEntryOptions(out LiteDbCacheEntryOptions? attached));
        Assert.Same(CacheOptions, attached);

        JsonDto? sent = JsonSerializer.Deserialize<JsonDto>(await captured.Content.ReadAsStringAsync(), WebJson);
        Assert.Equal("payload", sent?.Name);
    }

    [Fact]
    public async Task PostAsJsonAsync_JsonTypeInfo_SerializesBody()
    {
        HttpRequestMessage? captured = null;
        using NetHttpClient client = CreateClient(request =>
        {
            captured = request;
            return StubHttpHandler.Ok();
        });

        await client.PostAsJsonAsync("/resource", new JsonDto { Name = "typed" }, CacheOptions, JsonDtoTypeInfo());

        Assert.Equal("application/json", captured!.Content!.Headers.ContentType?.MediaType);
        JsonDto? sent = JsonSerializer.Deserialize(await captured.Content.ReadAsStringAsync(), JsonDtoTypeInfo());
        Assert.Equal("typed", sent?.Name);
    }

    [Fact]
    public async Task GetFromJsonAsync_UnsuccessfulStatus_Throws()
    {
        using NetHttpClient client = CreateClient(_ => new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent("""{"name":"nope"}""")
        });

        await Assert.ThrowsAsync<HttpRequestException>(() =>
            client.GetFromJsonAsync<JsonDto>("/resource", CacheOptions));
    }

    [Fact]
    public async Task NullClient_Throws()
    {
        NetHttpClient client = null!;

        ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            client.GetFromJsonAsync<JsonDto>("/resource", CacheOptions));

        Assert.Equal("client", exception.ParamName);
    }

    [Fact]
    public async Task NullJsonTypeInfo_Throws()
    {
        using NetHttpClient client = CreateClient();

        ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            client.GetFromJsonAsync("/resource", CacheOptions, (JsonTypeInfo<JsonDto>)null!));

        Assert.Equal("jsonTypeInfo", exception.ParamName);
    }

    private static Task<HttpResponseMessage> SendAsJsonAsync(NetHttpClient client, HttpMethod method, string requestUri,
        JsonDto value, LiteDbCacheEntryOptions cacheOptions)
    {
        if (method == HttpMethod.Post)
        {
            return client.PostAsJsonAsync(requestUri, value, cacheOptions);
        }

        if (method == HttpMethod.Put)
        {
            return client.PutAsJsonAsync(requestUri, value, cacheOptions);
        }

        if (method == HttpMethod.Patch)
        {
            return client.PatchAsJsonAsync(requestUri, value, cacheOptions);
        }

        throw new ArgumentOutOfRangeException(nameof(method));
    }

    private static JsonTypeInfo<JsonDto> JsonDtoTypeInfo()
    {
        return (JsonTypeInfo<JsonDto>)WebJson.GetTypeInfo(typeof(JsonDto));
    }

    private static NetHttpClient CreateClient(Func<HttpRequestMessage, HttpResponseMessage>? responder = null)
    {
        StubHttpHandler stub = new(responder ?? (_ => StubHttpHandler.Ok()));
        return new NetHttpClient(stub) { BaseAddress = new Uri("https://cache.test") };
    }

    public sealed class JsonDto
    {
        public string? Name { get; set; }
    }
}
