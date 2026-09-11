# LiteDbCacheHttpRequestOptions

Namespace: Nefarius.HttpClient.LiteDbCache.Options

Cache-specific options for [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage).

```csharp
public static class LiteDbCacheHttpRequestOptions
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [LiteDbCacheHttpRequestOptions](./nefarius.httpclient.litedbcache.options.litedbcachehttprequestoptions.md)

## Fields

### <a id="fields-entryoptions"/>**EntryOptions**

Cache-entry-specific [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md).

```csharp
public static string EntryOptions;
```

## Properties

### <a id="properties-entryoptionskey"/>**EntryOptionsKey**

Strongly typed [HttpRequestOptionsKey](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestoptionskey-1) for
 [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md) stored on [Options](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage.options).

```csharp
public static HttpRequestOptionsKey<LiteDbCacheEntryOptions> EntryOptionsKey { get; }
```

#### Property Value

[HttpRequestOptionsKey](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestoptionskey-1)<[LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)><br>
