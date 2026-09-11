# HttpClientExtensions

Namespace: Nefarius.HttpClient.LiteDbCache

Cache-aware [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) verb overloads that attach
 [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md) to the outgoing request.

```csharp
public static class HttpClientExtensions
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [HttpClientExtensions](./nefarius.httpclient.litedbcache.httpclientextensions.md)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### <a id="methods-deleteasync"/>**DeleteAsync(HttpClient, String, LiteDbCacheEntryOptions, CancellationToken)**

Sends a DELETE request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> DeleteAsync(HttpClient client, string requestUri, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-deleteasync"/>**DeleteAsync(HttpClient, Uri, LiteDbCacheEntryOptions, CancellationToken)**

Sends a DELETE request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> DeleteAsync(HttpClient client, Uri requestUri, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-getasync"/>**GetAsync(HttpClient, String, LiteDbCacheEntryOptions, CancellationToken)**

Sends a GET request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> GetAsync(HttpClient client, string requestUri, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-getasync"/>**GetAsync(HttpClient, Uri, LiteDbCacheEntryOptions, CancellationToken)**

Sends a GET request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> GetAsync(HttpClient client, Uri requestUri, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-getasync"/>**GetAsync(HttpClient, String, HttpCompletionOption, LiteDbCacheEntryOptions, CancellationToken)**

Sends a GET request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> GetAsync(HttpClient client, string requestUri, HttpCompletionOption completionOption, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`completionOption` [HttpCompletionOption](https://learn.microsoft.com/dotnet/api/system.net.http.httpcompletionoption)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-getasync"/>**GetAsync(HttpClient, Uri, HttpCompletionOption, LiteDbCacheEntryOptions, CancellationToken)**

Sends a GET request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> GetAsync(HttpClient client, Uri requestUri, HttpCompletionOption completionOption, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`completionOption` [HttpCompletionOption](https://learn.microsoft.com/dotnet/api/system.net.http.httpcompletionoption)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-headasync"/>**HeadAsync(HttpClient, String, LiteDbCacheEntryOptions, CancellationToken)**

Sends a HEAD request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> HeadAsync(HttpClient client, string requestUri, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-headasync"/>**HeadAsync(HttpClient, Uri, LiteDbCacheEntryOptions, CancellationToken)**

Sends a HEAD request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> HeadAsync(HttpClient client, Uri requestUri, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-headasync"/>**HeadAsync(HttpClient, String, HttpCompletionOption, LiteDbCacheEntryOptions, CancellationToken)**

Sends a HEAD request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> HeadAsync(HttpClient client, string requestUri, HttpCompletionOption completionOption, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`completionOption` [HttpCompletionOption](https://learn.microsoft.com/dotnet/api/system.net.http.httpcompletionoption)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-headasync"/>**HeadAsync(HttpClient, Uri, HttpCompletionOption, LiteDbCacheEntryOptions, CancellationToken)**

Sends a HEAD request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> HeadAsync(HttpClient client, Uri requestUri, HttpCompletionOption completionOption, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`completionOption` [HttpCompletionOption](https://learn.microsoft.com/dotnet/api/system.net.http.httpcompletionoption)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-patchasync"/>**PatchAsync(HttpClient, String, HttpContent, LiteDbCacheEntryOptions, CancellationToken)**

Sends a PATCH request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PatchAsync(HttpClient client, string requestUri, HttpContent content, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`content` [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-patchasync"/>**PatchAsync(HttpClient, Uri, HttpContent, LiteDbCacheEntryOptions, CancellationToken)**

Sends a PATCH request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PatchAsync(HttpClient client, Uri requestUri, HttpContent content, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`content` [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-postasync"/>**PostAsync(HttpClient, String, HttpContent, LiteDbCacheEntryOptions, CancellationToken)**

Sends a POST request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PostAsync(HttpClient client, string requestUri, HttpContent content, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`content` [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-postasync"/>**PostAsync(HttpClient, Uri, HttpContent, LiteDbCacheEntryOptions, CancellationToken)**

Sends a POST request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PostAsync(HttpClient client, Uri requestUri, HttpContent content, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`content` [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-putasync"/>**PutAsync(HttpClient, String, HttpContent, LiteDbCacheEntryOptions, CancellationToken)**

Sends a PUT request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PutAsync(HttpClient client, string requestUri, HttpContent content, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`content` [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-putasync"/>**PutAsync(HttpClient, Uri, HttpContent, LiteDbCacheEntryOptions, CancellationToken)**

Sends a PUT request with request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PutAsync(HttpClient client, Uri requestUri, HttpContent content, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`content` [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-sendasync"/>**SendAsync(HttpClient, HttpRequestMessage, LiteDbCacheEntryOptions, CancellationToken)**

Sends `request` after attaching `cacheOptions`.

```csharp
public static Task<HttpResponseMessage> SendAsync(HttpClient client, HttpRequestMessage request, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`request` [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-sendasync"/>**SendAsync(HttpClient, HttpRequestMessage, HttpCompletionOption, LiteDbCacheEntryOptions, CancellationToken)**

Sends `request` after attaching `cacheOptions`.

```csharp
public static Task<HttpResponseMessage> SendAsync(HttpClient client, HttpRequestMessage request, HttpCompletionOption completionOption, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`request` [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage)<br>

`completionOption` [HttpCompletionOption](https://learn.microsoft.com/dotnet/api/system.net.http.httpcompletionoption)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>
