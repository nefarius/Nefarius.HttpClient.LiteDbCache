# HttpClientBuilderExtensions

Namespace: Nefarius.HttpClient.LiteDbCache

Extensions for [IHttpClientBuilder](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder).

```csharp
public static class HttpClientBuilderExtensions
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [HttpClientBuilderExtensions](./nefarius.httpclient.litedbcache.httpclientbuilderextensions.md)<br>
Attributes [ExtensionAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### <a id="methods-addlitedbcache"/>**AddLiteDbCache(IHttpClientBuilder, Action&lt;LiteDbCacheDatabaseOptions&gt;)**

Configures a named LiteDB cache instance for a given [IHttpClientBuilder](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder).

```csharp
public static IHttpClientBuilder AddLiteDbCache(IHttpClientBuilder builder, Action<LiteDbCacheDatabaseOptions> configuration)
```

#### Parameters

`builder` [IHttpClientBuilder](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder)<br>

`configuration` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)<[LiteDbCacheDatabaseOptions](./nefarius.httpclient.litedbcache.options.litedbcachedatabaseoptions.md)><br>

#### Returns

[IHttpClientBuilder](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder)
