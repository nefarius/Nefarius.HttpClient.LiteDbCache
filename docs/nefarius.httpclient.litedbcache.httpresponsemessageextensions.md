# HttpResponseMessageExtensions

Namespace: Nefarius.HttpClient.LiteDbCache

extensions.

```csharp
public static class HttpResponseMessageExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [HttpResponseMessageExtensions](./nefarius.httpclient.litedbcache.httpresponsemessageextensions.md)

## Methods

### <a id="methods-getcacheid"/>**GetCacheId(HttpResponseMessage)**

Gets the [LiteDbCacheHeaders.CacheId](./nefarius.httpclient.litedbcache.litedbcacheheaders.md#cacheid) of the .

```csharp
public static ObjectId GetCacheId(HttpResponseMessage message)
```

#### Parameters

`message` HttpResponseMessage<br>
The  to read.

#### Returns

The  or null.

### <a id="methods-iscached"/>**IsCached(HttpResponseMessage)**

Checks whether a  was pulled form a  cache instance.

```csharp
public static bool IsCached(HttpResponseMessage message)
```

#### Parameters

`message` HttpResponseMessage<br>
The  to check.

#### Returns

True if pulled from cache, false otherwise.
