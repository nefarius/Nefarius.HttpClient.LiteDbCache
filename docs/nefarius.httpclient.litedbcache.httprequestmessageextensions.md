# HttpRequestMessageExtensions

Namespace: Nefarius.HttpClient.LiteDbCache

[HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) extensions for LiteDB cache configuration.

```csharp
public static class HttpRequestMessageExtensions
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [HttpRequestMessageExtensions](./nefarius.httpclient.litedbcache.httprequestmessageextensions.md)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### <a id="methods-setlitedbcacheentryoptions"/>**SetLiteDbCacheEntryOptions(HttpRequestMessage, LiteDbCacheEntryOptions)**

Attaches request-specific [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md) that override the named client's defaults.

```csharp
public static HttpRequestMessage SetLiteDbCacheEntryOptions(HttpRequestMessage request, LiteDbCacheEntryOptions options)
```

#### Parameters

`request` [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage)<br>
The request to configure.

`options` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>
The cache options that apply to this request only.

#### Returns

The same `request` for chaining.

### <a id="methods-trygetlitedbcacheentryoptions"/>**TryGetLiteDbCacheEntryOptions(HttpRequestMessage, ref LiteDbCacheEntryOptions)**

Tries to read request-specific [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md).

```csharp
public static bool TryGetLiteDbCacheEntryOptions(HttpRequestMessage request, ref LiteDbCacheEntryOptions options)
```

#### Parameters

`request` [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage)<br>
The request to inspect.

`options` [LiteDbCacheEntryOptions&](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions&.md)<br>
The attached options, if any.

#### Returns

`true` when request-specific options are present.
