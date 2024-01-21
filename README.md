<img src="assets/NSS-128x128.png" align="right" />

# Nefarius.HttpClient.LiteDbCache

![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/nefarius/Nefarius.HttpClient.LiteDbCache/build.yml) ![Requirements](https://img.shields.io/badge/Requires-.NET%20%3E%3D6.0-blue.svg) [![Nuget](https://img.shields.io/nuget/v/Nefarius.HttpClient.LiteDbCache)](https://www.nuget.org/packages/Nefarius.HttpClient.LiteDbCache/) ![Nuget](https://img.shields.io/nuget/dt/Nefarius.HttpClient.LiteDbCache)

Adds disk-based response caching to HttpClient named instances using LiteDB.

Work in progress...

## How to use

Register one or
more [named HTTP clients](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests#named-clients)
with `AddLiteDbCache`. This example snippet registers a cached client that will query your public IP address
using [`https://ifconfig.me/`](https://ifconfig.me/) and cache the response indefinitely to a local embedded database
instance:

```csharp
builder.Services.AddHttpClient("ifconfig", cfg =>
{
    cfg.BaseAddress = new Uri("https://ifconfig.me");
    
}).AddLiteDbCache(options =>
{
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

## Sources & 3rd party credits

This library benefits from these awesome projects ❤ (appearance in no special order):

- [LiteDB](https://github.com/mbdavid/LiteDB)
