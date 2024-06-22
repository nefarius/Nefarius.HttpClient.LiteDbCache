# HttpClientBuilderExtensions

Namespace: Nefarius.HttpClient.LiteDbCache

Extensions for .

```csharp
public static class HttpClientBuilderExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [HttpClientBuilderExtensions](./nefarius.httpclient.litedbcache.httpclientbuilderextensions.md)

## Methods

### <a id="methods-addlitedbcache"/>**AddLiteDbCache(IHttpClientBuilder, Action&lt;LiteDbCacheDatabaseOptions&gt;)**

Configures a named  cache instance for a given .

```csharp
public static IHttpClientBuilder AddLiteDbCache(IHttpClientBuilder builder, Action<LiteDbCacheDatabaseOptions> configuration)
```

#### Parameters

`builder` IHttpClientBuilder<br>

`configuration` [Action&lt;LiteDbCacheDatabaseOptions&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>

#### Returns

IHttpClientBuilder
