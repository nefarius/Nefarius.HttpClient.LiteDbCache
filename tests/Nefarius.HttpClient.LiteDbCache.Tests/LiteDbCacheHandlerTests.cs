using System.Net;
using System.Net.Http.Headers;

using LiteDB;

using Xunit;

using Nefarius.HttpClient.LiteDbCache.Internal;
using Nefarius.HttpClient.LiteDbCache.Options;

namespace Nefarius.HttpClient.LiteDbCache.Tests;

public class LiteDbCacheHandlerTests
{
    [Fact]
    public async Task SecondRequest_IsServedFromCache()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10));

        HttpResponseMessage first = await host.Client.GetAsync("/resource");
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(1, host.Stub.CallCount);
        Assert.False(first.IsCached());
        Assert.True(second.IsCached());
        Assert.Equal("cached-body", await second.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Default_IgnoresCacheControlNoStore()
    {
        await using CacheTestHost host = CacheTestHost.Create(
            options => options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            (_, _) => StubHttpHandler.Ok(configure: response =>
                response.Headers.CacheControl = new CacheControlHeaderValue { NoStore = true }));

        await host.Client.GetAsync("/resource");
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(1, host.Stub.CallCount);
        Assert.True(second.IsCached());
    }

    [Fact]
    public async Task HonorCacheControl_NoStore_DoesNotCache()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            options.HonorCacheControl = true;
        }, (_, _) => StubHttpHandler.Ok(configure: response =>
            response.Headers.CacheControl = new CacheControlHeaderValue { NoStore = true }));

        await host.Client.GetAsync("/resource");
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(2, host.Stub.CallCount);
        Assert.False(second.IsCached());
    }

    [Fact]
    public async Task HonorCacheControl_NoCache_DoesNotCache()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            options.HonorCacheControl = true;
        }, (_, _) => StubHttpHandler.Ok(configure: response =>
            response.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true }));

        await host.Client.GetAsync("/resource");
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(2, host.Stub.CallCount);
        Assert.False(second.IsCached());
    }

    [Fact]
    public async Task HonorCacheControl_FreshMaxAge_Caches()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.HonorCacheControl = true;
        }, (_, _) => StubHttpHandler.Ok(configure: response =>
        {
            response.Headers.Date = DateTimeOffset.UtcNow;
            response.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = TimeSpan.FromMinutes(5) };
        }));

        await host.Client.GetAsync("/resource");
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(1, host.Stub.CallCount);
        Assert.True(second.IsCached());
    }

    [Fact]
    public async Task HonorCacheControl_StaleMaxAgeOnArrival_DoesNotCache()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            options.HonorCacheControl = true;
        }, (_, _) => StubHttpHandler.Ok(configure: response =>
        {
            response.Headers.Date = DateTimeOffset.UtcNow.AddMinutes(-10);
            response.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = TimeSpan.FromMinutes(1) };
        }));

        await host.Client.GetAsync("/resource");
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(2, host.Stub.CallCount);
        Assert.False(second.IsCached());
    }

    [Fact]
    public async Task HonorCacheControl_ExpiredUpstreamEntry_IsRefetched()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.HonorCacheControl = true;
        }, (_, _) => StubHttpHandler.Ok(configure: response =>
        {
            response.Headers.Date = DateTimeOffset.UtcNow;
            response.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = TimeSpan.FromMinutes(5) };
        }));

        await host.Client.GetAsync("/resource");
        ExpireUpstreamEntry(host);
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(2, host.Stub.CallCount);
        Assert.False(second.IsCached());
    }

    [Fact]
    public async Task HonorCacheControl_ExpiresFallback_Caches()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.HonorCacheControl = true;
        }, (_, _) => StubHttpHandler.Ok(configure: response =>
            response.Content.Headers.Expires = DateTimeOffset.UtcNow.AddMinutes(10)));

        await host.Client.GetAsync("/resource");
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(1, host.Stub.CallCount);
        Assert.True(second.IsCached());
    }

    [Fact]
    public async Task HonorCacheControl_ExpiresInThePast_DoesNotCache()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.HonorCacheControl = true;
        }, (_, _) => StubHttpHandler.Ok(configure: response =>
            response.Content.Headers.Expires = DateTimeOffset.UtcNow.AddMinutes(-1)));

        await host.Client.GetAsync("/resource");
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(2, host.Stub.CallCount);
        Assert.False(second.IsCached());
    }

    [Fact]
    public async Task HonorCacheControl_MaxAgeTakesPrecedenceOverStaleExpires()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.HonorCacheControl = true;
        }, (_, _) => StubHttpHandler.Ok(configure: response =>
        {
            response.Headers.Date = DateTimeOffset.UtcNow;
            response.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = TimeSpan.FromMinutes(5) };
            response.Content.Headers.Expires = DateTimeOffset.UtcNow.AddHours(-1);
        }));

        await host.Client.GetAsync("/resource");
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(1, host.Stub.CallCount);
        Assert.True(second.IsCached());
    }

    [Fact]
    public async Task LocalAbsoluteExpiration_ExpiresBeforeUpstreamMaxAge()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.HonorCacheControl = true;
            options.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(-1);
        }, (_, _) => StubHttpHandler.Ok(configure: response =>
        {
            response.Headers.Date = DateTimeOffset.UtcNow;
            response.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = TimeSpan.FromHours(1) };
        }));

        await host.Client.GetAsync("/resource");
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(2, host.Stub.CallCount);
        Assert.False(second.IsCached());
    }

    [Fact]
    public async Task ContentHeaders_ArePreservedOnFetchAndCacheHit()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            (_, _) => StubHttpHandler.Ok("json-body", "application/json"));

        HttpResponseMessage first = await host.Client.GetAsync("/resource");
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal("application/json", first.Content.Headers.ContentType?.MediaType);
        Assert.Equal("application/json", second.Content.Headers.ContentType?.MediaType);
        Assert.Equal("json-body", await second.Content.ReadAsStringAsync());
        Assert.True(second.IsCached());
    }

    [Fact]
    public async Task PerRequestOptions_CanEnableHonorCacheControl()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            options.HonorCacheControl = false;
        }, (_, _) => StubHttpHandler.Ok(configure: response =>
            response.Headers.CacheControl = new CacheControlHeaderValue { NoStore = true }));

        HttpRequestMessage first = new(HttpMethod.Get, "/resource");
        first.SetLiteDbCacheEntryOptions(new LiteDbCacheEntryOptions
        {
            HonorCacheControl = true,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });

        await host.Client.SendAsync(first);

        HttpRequestMessage second = new(HttpMethod.Get, "/resource");
        second.SetLiteDbCacheEntryOptions(new LiteDbCacheEntryOptions
        {
            HonorCacheControl = true,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });

        HttpResponseMessage cachedProbe = await host.Client.SendAsync(second);

        Assert.Equal(2, host.Stub.CallCount);
        Assert.False(cachedProbe.IsCached());
    }

    [Fact]
    public async Task PerRequestCacheErrors_OverridesGlobalSetting()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            options.CacheErrors = false;
        }, (_, _) => new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent("boom")
        });

        HttpRequestMessage first = new(HttpMethod.Get, "/error");
        first.SetLiteDbCacheEntryOptions(new LiteDbCacheEntryOptions
        {
            CacheErrors = true,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });

        await host.Client.SendAsync(first);
        HttpResponseMessage second = await host.Client.GetAsync("/error");

        Assert.Equal(1, host.Stub.CallCount);
        Assert.True(second.IsCached());
        Assert.Equal(HttpStatusCode.InternalServerError, second.StatusCode);
    }

    [Fact]
    public async Task Default_ExpiredEntry_IsDiscardedWhenRefreshFails()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            (_, n) => n == 1
                ? StubHttpHandler.Ok("fresh")
                : new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent("down")
                });

        await host.Client.GetAsync("/resource");
        AgeEntry(host);
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(2, host.Stub.CallCount);
        Assert.False(second.IsCached());
        Assert.False(second.IsStale());
        Assert.Equal(HttpStatusCode.ServiceUnavailable, second.StatusCode);
    }

    [Fact]
    public async Task ServeStaleOnError_ReturnsExpiredEntryWhenStatusFails()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            options.ServeStaleOnError = true;
        }, (_, n) => n == 1
            ? StubHttpHandler.Ok("fresh")
            : new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
            {
                Content = new StringContent("down")
            });

        await host.Client.GetAsync("/resource");
        AgeEntry(host);
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(2, host.Stub.CallCount);
        Assert.True(second.IsCached());
        Assert.True(second.IsStale());
        Assert.Equal(HttpStatusCode.OK, second.StatusCode);
        Assert.Equal("fresh", await second.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task ServeStaleOnError_ReturnsExpiredEntryWhenTransportFails()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            options.ServeStaleOnError = true;
        }, (_, n) => n == 1
            ? StubHttpHandler.Ok("fresh")
            : throw new HttpRequestException("upstream unreachable"));

        await host.Client.GetAsync("/resource");
        AgeEntry(host);
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.Equal(2, host.Stub.CallCount);
        Assert.True(second.IsStale());
        Assert.Equal("fresh", await second.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task ServeStaleOnError_SuccessfulRefresh_ReplacesEntry()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            options.ServeStaleOnError = true;
        }, (_, n) => StubHttpHandler.Ok(n == 1 ? "v1" : "v2"));

        await host.Client.GetAsync("/resource");
        AgeEntry(host);
        HttpResponseMessage second = await host.Client.GetAsync("/resource");
        HttpResponseMessage third = await host.Client.GetAsync("/resource");

        Assert.Equal(2, host.Stub.CallCount);
        Assert.False(second.IsCached());
        Assert.Equal("v2", await second.Content.ReadAsStringAsync());
        Assert.True(third.IsCached());
        Assert.False(third.IsStale());
        Assert.Equal("v2", await third.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task ServeStaleOnError_WithoutCachedEntry_DoesNotSwallowFailure()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.ServeStaleOnError = true;
        }, (_, _) => throw new HttpRequestException("upstream unreachable"));

        await Assert.ThrowsAsync<HttpRequestException>(() => host.Client.GetAsync("/resource"));
        Assert.Equal(1, host.Stub.CallCount);
    }

    [Fact]
    public async Task ServeStaleOnError_DoesNotTreatCallerCancellationAsFailure()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            options.ServeStaleOnError = true;
        }, (_, n) => n == 1
            ? StubHttpHandler.Ok("fresh")
            : throw new HttpRequestException("should not be reached after cancel"));

        await host.Client.GetAsync("/resource");
        AgeEntry(host);

        using CancellationTokenSource cts = new();
        await cts.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            host.Client.GetAsync("/resource", cts.Token));
    }

    [Fact]
    public async Task PerRequestOptions_CanEnableServeStaleOnError()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            options.ServeStaleOnError = false;
        }, (_, n) => n == 1
            ? StubHttpHandler.Ok("fresh")
            : new HttpResponseMessage(HttpStatusCode.BadGateway)
            {
                Content = new StringContent("down")
            });

        await host.Client.GetAsync("/resource");
        AgeEntry(host);

        HttpRequestMessage refresh = new(HttpMethod.Get, "/resource");
        refresh.SetLiteDbCacheEntryOptions(new LiteDbCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            ServeStaleOnError = true
        });

        HttpResponseMessage second = await host.Client.SendAsync(refresh);

        Assert.Equal(2, host.Stub.CallCount);
        Assert.True(second.IsStale());
        Assert.Equal("fresh", await second.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task CacheResponseContentDisabled_DoesNotRestoreContentHeadersOnHit()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            options.CacheResponseContent = false;
        }, (_, _) => StubHttpHandler.Ok("payload", "application/json"));

        await host.Client.GetAsync("/resource");
        HttpResponseMessage second = await host.Client.GetAsync("/resource");

        Assert.True(second.IsCached());
        Assert.Null(second.Content.Headers.ContentType);
        Assert.True(string.IsNullOrEmpty(await second.Content.ReadAsStringAsync()));
    }

    [Fact]
    public async Task HonorCacheControl_NoStore_DeletesStaleCandidate()
    {
        await using CacheTestHost host = CacheTestHost.Create(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            options.HonorCacheControl = true;
            options.ServeStaleOnError = true;
        }, (_, n) => n switch
        {
            1 => StubHttpHandler.Ok("fresh"),
            2 => StubHttpHandler.Ok("live", configure: response =>
                response.Headers.CacheControl = new CacheControlHeaderValue { NoStore = true }),
            _ => new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
            {
                Content = new StringContent("down")
            }
        });

        await host.Client.GetAsync("/resource");
        AgeEntry(host);
        HttpResponseMessage refreshed = await host.Client.GetAsync("/resource");
        HttpResponseMessage third = await host.Client.GetAsync("/resource");

        Assert.Equal(3, host.Stub.CallCount);
        Assert.False(refreshed.IsCached());
        Assert.Equal("live", await refreshed.Content.ReadAsStringAsync());
        Assert.False(third.IsCached());
        Assert.False(third.IsStale());
        Assert.Equal(HttpStatusCode.ServiceUnavailable, third.StatusCode);
    }

    private static void AgeEntry(CacheTestHost host)
    {
        LiteDatabase db = host.Databases.GetDatabase(CacheTestHost.ClientName)
                          ?? throw new InvalidOperationException("Cache database was not created.");
        ILiteCollection<CachedHttpResponseMessage> col = db.GetCollection<CachedHttpResponseMessage>("cache");
        CachedHttpResponseMessage entry = col.FindAll().Single();
        entry.CreatedAt = DateTimeOffset.UtcNow.AddHours(-1);
        col.Update(entry);
    }

    private static void ExpireUpstreamEntry(CacheTestHost host)
    {
        LiteDatabase db = host.Databases.GetDatabase(CacheTestHost.ClientName)
                          ?? throw new InvalidOperationException("Cache database was not created.");
        ILiteCollection<CachedHttpResponseMessage> col = db.GetCollection<CachedHttpResponseMessage>("cache");
        CachedHttpResponseMessage entry = col.FindAll().Single();
        entry.UpstreamExpiresAt = DateTimeOffset.UtcNow.AddMinutes(-1);
        col.Update(entry);
    }
}
