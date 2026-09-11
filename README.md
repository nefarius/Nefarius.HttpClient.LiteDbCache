# <img src="assets/NSS-128x128.png" align="left" />Nefarius.HttpClient.LiteDbCache

![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/nefarius/Nefarius.HttpClient.LiteDbCache/build.yml)
![Requirements](https://img.shields.io/badge/Requires-.NET%20%3E%3D6.0-blue.svg)
[![Nuget](https://img.shields.io/nuget/v/Nefarius.HttpClient.LiteDbCache)](https://www.nuget.org/packages/Nefarius.HttpClient.LiteDbCache/)
![Nuget](https://img.shields.io/nuget/dt/Nefarius.HttpClient.LiteDbCache)

Adds disk-based response caching to HttpClient named instances using LiteDB.

## Motivation

Sometimes a response from a remote HTTP service doesn't change frequently and fetching it again multiple times within a
certain time span is wasteful and puts unnecessary delays on the caller. Offline caching to the rescue! However,
manually storing and fetching responses gets verbose and complex fast, why not hide that complexity away and
let `IHttpClientFactory` deal with it behind the scenes?

This library provides the extension method `AddLiteDbCache` you can chain your named HTTP client call with and specify
an embedded database location to use for offline caching, no other code changes are required.

### Why not use [`IMemoryCache`](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/memory)?

The goal of the cache is to survive application/service restarts.

### Why not use [`IDistributedCache`](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/distributed)?

This library is aimed at end-user clients where you wish to drag in as little dependency on 3rd party services as
possible. An embedded database sitting in some folder does the trick there perfectly. It's usually not the brightest
idea to require spinning up a Redis or MongoDB instance on a client's machine just to get some basic persisted storage
capabilities. 😉

## Features

- Each named HTTP client gets its own backing cache database instance which is kept exclusively open by default
  throughout application lifetime for performance benefits.
- Cached entries expiration (and exclusion) can be configured globally per named instance or overridden per request.
- Upstream `Cache-Control` / `Expires` headers can optionally bound or skip storage.
- Expired entries can optionally be served when a refresh fails (stale-if-error / offline fallback).

## How to use

Register one or
more [named HTTP clients](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests#named-clients)
with `AddLiteDbCache`. This example snippet registers a cached client that will query your public IP address
using [`https://ifconfig.me/`](https://ifconfig.me/) and cache the response for 10 minutes to a local embedded database
instance:

```csharp
builder.Services.AddHttpClient("ifconfig", cfg =>
{
    cfg.BaseAddress = new Uri("https://ifconfig.me");
    
}).AddLiteDbCache(options =>
{
    // note: ensure that the path given already exists or you'll get a runtime exception
    options.ConnectionString = @"C:\Temp\ifconfig.db";
    options.CollectionName = "ifconfig-response-cache";
    options.EntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
});
```

This cached named client can now be consumed like any other `HttpClient` wherever needed:

```csharp
HttpClient client = _clientFactory.CreateClient("ifconfig");

HttpResponseMessage result = await client.GetAsync("/", ct);

string? publicIP = await result.Content.ReadAsStringAsync(ct);
```

If a cached entry exists, the response (headers, body content etc.) will be pulled and returned from the local database
and no remote web request will be issued until the cache entry expires.

### Honour upstream cache headers

Set `HonorCacheControl` to let the remote `Cache-Control` and `Expires` headers influence storage. This stays **off** by
default so existing clients keep their configured TTLs.

```csharp
}).AddLiteDbCache(options =>
{
    options.ConnectionString = @"C:\Temp\ifconfig.db";
    options.CollectionName = "ifconfig-response-cache";
    options.EntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
    options.EntryOptions.HonorCacheControl = true;
});
```

When enabled:

- `Cache-Control: no-store` and `no-cache` skip caching entirely (`no-cache` is treated as a bypass, not as HTTP
  revalidation with `ETag` / `Last-Modified`).
- `max-age` (minus `Age` / apparent age from `Date`) is an upper bound on how long the entry may be reused.
- `Expires` is used only when `max-age` is absent.
- Responses that are already stale on arrival are not stored.
- Local expiration options still apply; the earliest expiry wins.

### Per-request cache options

Attach a `LiteDbCacheEntryOptions` instance to override the named client's defaults for that call only. Convenience
overloads exist for GET, HEAD, DELETE, POST, PUT, and PATCH (the verbs the cache engine currently keys), including the
common [`System.Net.Http.Json`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions)
helpers:

```csharp
LiteDbCacheEntryOptions cache = new()
{
    HonorCacheControl = true,
    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
};

HttpResponseMessage result = await client.GetAsync("/", cache, ct);

IpResponse? ip = await client.GetFromJsonAsync<IpResponse>("/", cache, ct);
```

You can still attach options to an existing `HttpRequestMessage` when you need custom methods, headers, or completion
behavior:

```csharp
HttpRequestMessage request = new(HttpMethod.Get, "/");
request.Headers.Accept.Add(new("application/json"));
HttpResponseMessage result = await client.SendAsync(request, cache, ct);
```

### Serve stale content when refresh fails

By default an expired entry is discarded before the remote call. Set `ServeStaleOnError` to keep it and return that
snapshot when the refresh fails (transport error, timeout, or a non-success status). Successful refreshes replace the
entry as usual. Caller cancellation is not treated as a failure.

```csharp
options.EntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
options.EntryOptions.ServeStaleOnError = true;
```

Stale fallbacks still report `IsCached()` and also `IsStale()`, and include the `X-LiteDb-Cache-Stale` header.

## Advanced usage

### Cache database access

Inject the `ILiteDbCacheDatabaseInstances` interface to get access to the `LiteDatabase` instances and other database
management methods (cache purge and alike).

## Documentation

[Link to API docs](docs/index.md).

## Sources & 3rd party credits

This library benefits from these awesome projects ❤ (appearance in no special order):

- [LiteDB](https://github.com/mbdavid/LiteDB)
