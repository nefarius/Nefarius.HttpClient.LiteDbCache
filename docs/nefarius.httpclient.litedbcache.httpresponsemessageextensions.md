# HttpResponseMessageExtensions

Namespace: Nefarius.HttpClient.LiteDbCache

[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) extensions.

```csharp
public static class HttpResponseMessageExtensions
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [HttpResponseMessageExtensions](./nefarius.httpclient.litedbcache.httpresponsemessageextensions.md)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### <a id="methods-getcacheid"/>**GetCacheId(HttpResponseMessage)**

Gets the [LiteDbCacheHeaders.CacheId](./nefarius.httpclient.litedbcache.litedbcacheheaders.md#cacheid) of the [HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage).

```csharp
public static ObjectId GetCacheId(HttpResponseMessage message)
```

#### Parameters

`message` [HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)<br>
The [HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) to read.

#### Returns

The ObjectId or null.

### <a id="methods-iscached"/>**IsCached(HttpResponseMessage)**

Checks whether a [HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) was pulled form a LiteDatabase cache instance.

```csharp
public static bool IsCached(HttpResponseMessage message)
```

#### Parameters

`message` [HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)<br>
The [HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) to check.

#### Returns

True if pulled from cache, false otherwise.

### <a id="methods-isstale"/>**IsStale(HttpResponseMessage)**

Checks whether a [HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) was served from an expired cache entry after a failed refresh.

```csharp
public static bool IsStale(HttpResponseMessage message)
```

#### Parameters

`message` [HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)<br>
The [HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) to check.

#### Returns

True if a stale cached entry was returned, false otherwise.
