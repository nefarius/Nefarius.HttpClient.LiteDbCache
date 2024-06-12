# <img src="assets/NSS-128x128.png" align="left" />Nefarius.HttpClient.LiteDbCache

![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/nefarius/Nefarius.HttpClient.LiteDbCache/build.yml) ![Requirements](https://img.shields.io/badge/Requires-.NET%20%3E%3D6.0-blue.svg) [![Nuget](https://img.shields.io/nuget/v/Nefarius.HttpClient.LiteDbCache)](https://www.nuget.org/packages/Nefarius.HttpClient.LiteDbCache/) ![Nuget](https://img.shields.io/nuget/dt/Nefarius.HttpClient.LiteDbCache)

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
capabilities 😉

## Features

- Each named HTTP client gets its own backing cache database instance which is kept exclusively open by default
  throughout application lifetime for performance benefits.
- Cached entries expiration (and exclusion) can be configured globally per named instance.

## Some ideas

- Per-request cache entry expiration options (
  like [`MemoryCacheEntryOptions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.caching.memory.memorycacheentryoptions)
  and similar) are technically possible; however due to how the `HttpClient` class is structured would require writing a
  ton of wrapper methods that supply these options to each `HttpRequestMessage` (which can not be conveniently
  overwritten); a task I am currently not fond of since it's a low priority anyway.
- The upstream `Cache-Control` header is currently completely ignored; it could be taken into consideration, if the user
  configured it to be honored.
- Add some unit tests... maybe. Someday. 😅

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

## Advanced usage

### Cache database access

Inject the `ILiteDbCacheDatabaseInstances` interface to get access to the `LiteDatabase` instances and other database
management methods (cache purge and alike).

## Documentation

[Link to API docs](docs/index.md).

### Generating documentation

```PowerShell
dotnet tool install --global Nefarius.Tools.XMLDoc2Markdown
dotnet publish -c Release -f net7.0 .\src\Nefarius.HttpClient.LiteDbCache.csproj
xmldoc2md .\src\bin\net7.0\publish\Nefarius.HttpClient.LiteDbCache.dll .\docs\
```

## Sources & 3rd party credits

This library benefits from these awesome projects ❤ (appearance in no special order):

- [LiteDB](https://github.com/mbdavid/LiteDB)
