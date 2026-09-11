# HttpClientJsonExtensions

Namespace: Nefarius.HttpClient.LiteDbCache

Cache-aware [HttpClientJsonExtensions](https://learn.microsoft.com/dotnet/api/system.net.http.json.httpclientjsonextensions) overloads that attach
 [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md) to the outgoing request.

```csharp
public static class HttpClientJsonExtensions
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [HttpClientJsonExtensions](./nefarius.httpclient.litedbcache.httpclientjsonextensions.md)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### <a id="methods-deletefromjsonasync"/>**DeleteFromJsonAsync&lt;TValue&gt;(HttpClient, String, LiteDbCacheEntryOptions, CancellationToken)**

Sends a DELETE request and deserializes the JSON response using request-specific cache options.

```csharp
public static Task<TValue> DeleteFromJsonAsync<TValue>(HttpClient client, string requestUri, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<TValue>

### <a id="methods-deletefromjsonasync"/>**DeleteFromJsonAsync&lt;TValue&gt;(HttpClient, Uri, LiteDbCacheEntryOptions, CancellationToken)**

Sends a DELETE request and deserializes the JSON response using request-specific cache options.

```csharp
public static Task<TValue> DeleteFromJsonAsync<TValue>(HttpClient client, Uri requestUri, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<TValue>

### <a id="methods-deletefromjsonasync"/>**DeleteFromJsonAsync&lt;TValue&gt;(HttpClient, String, LiteDbCacheEntryOptions, JsonSerializerOptions, CancellationToken)**

Sends a DELETE request and deserializes the JSON response using request-specific cache options.

```csharp
public static Task<TValue> DeleteFromJsonAsync<TValue>(HttpClient client, string requestUri, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions options, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<TValue>

### <a id="methods-deletefromjsonasync"/>**DeleteFromJsonAsync&lt;TValue&gt;(HttpClient, Uri, LiteDbCacheEntryOptions, JsonSerializerOptions, CancellationToken)**

Sends a DELETE request and deserializes the JSON response using request-specific cache options.

```csharp
public static Task<TValue> DeleteFromJsonAsync<TValue>(HttpClient client, Uri requestUri, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions options, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<TValue>

### <a id="methods-deletefromjsonasync"/>**DeleteFromJsonAsync&lt;TValue&gt;(HttpClient, String, LiteDbCacheEntryOptions, JsonTypeInfo&lt;TValue&gt;, CancellationToken)**

Sends a DELETE request and deserializes the JSON response using request-specific cache options.

```csharp
public static Task<TValue> DeleteFromJsonAsync<TValue>(HttpClient client, string requestUri, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`jsonTypeInfo` [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1)<TValue><br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<TValue>

### <a id="methods-deletefromjsonasync"/>**DeleteFromJsonAsync&lt;TValue&gt;(HttpClient, Uri, LiteDbCacheEntryOptions, JsonTypeInfo&lt;TValue&gt;, CancellationToken)**

Sends a DELETE request and deserializes the JSON response using request-specific cache options.

```csharp
public static Task<TValue> DeleteFromJsonAsync<TValue>(HttpClient client, Uri requestUri, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`jsonTypeInfo` [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1)<TValue><br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<TValue>

### <a id="methods-getfromjsonasync"/>**GetFromJsonAsync&lt;TValue&gt;(HttpClient, String, LiteDbCacheEntryOptions, CancellationToken)**

Sends a GET request and deserializes the JSON response using request-specific cache options.

```csharp
public static Task<TValue> GetFromJsonAsync<TValue>(HttpClient client, string requestUri, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<TValue>

### <a id="methods-getfromjsonasync"/>**GetFromJsonAsync&lt;TValue&gt;(HttpClient, Uri, LiteDbCacheEntryOptions, CancellationToken)**

Sends a GET request and deserializes the JSON response using request-specific cache options.

```csharp
public static Task<TValue> GetFromJsonAsync<TValue>(HttpClient client, Uri requestUri, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<TValue>

### <a id="methods-getfromjsonasync"/>**GetFromJsonAsync&lt;TValue&gt;(HttpClient, String, LiteDbCacheEntryOptions, JsonSerializerOptions, CancellationToken)**

Sends a GET request and deserializes the JSON response using request-specific cache options.

```csharp
public static Task<TValue> GetFromJsonAsync<TValue>(HttpClient client, string requestUri, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions options, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<TValue>

### <a id="methods-getfromjsonasync"/>**GetFromJsonAsync&lt;TValue&gt;(HttpClient, Uri, LiteDbCacheEntryOptions, JsonSerializerOptions, CancellationToken)**

Sends a GET request and deserializes the JSON response using request-specific cache options.

```csharp
public static Task<TValue> GetFromJsonAsync<TValue>(HttpClient client, Uri requestUri, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions options, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<TValue>

### <a id="methods-getfromjsonasync"/>**GetFromJsonAsync&lt;TValue&gt;(HttpClient, String, LiteDbCacheEntryOptions, JsonTypeInfo&lt;TValue&gt;, CancellationToken)**

Sends a GET request and deserializes the JSON response using request-specific cache options.

```csharp
public static Task<TValue> GetFromJsonAsync<TValue>(HttpClient client, string requestUri, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`jsonTypeInfo` [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1)<TValue><br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<TValue>

### <a id="methods-getfromjsonasync"/>**GetFromJsonAsync&lt;TValue&gt;(HttpClient, Uri, LiteDbCacheEntryOptions, JsonTypeInfo&lt;TValue&gt;, CancellationToken)**

Sends a GET request and deserializes the JSON response using request-specific cache options.

```csharp
public static Task<TValue> GetFromJsonAsync<TValue>(HttpClient client, Uri requestUri, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`jsonTypeInfo` [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1)<TValue><br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<TValue>

### <a id="methods-patchasjsonasync"/>**PatchAsJsonAsync&lt;TValue&gt;(HttpClient, String, TValue, LiteDbCacheEntryOptions, CancellationToken)**

Sends a PATCH request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(HttpClient client, string requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-patchasjsonasync"/>**PatchAsJsonAsync&lt;TValue&gt;(HttpClient, Uri, TValue, LiteDbCacheEntryOptions, CancellationToken)**

Sends a PATCH request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(HttpClient client, Uri requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-patchasjsonasync"/>**PatchAsJsonAsync&lt;TValue&gt;(HttpClient, String, TValue, LiteDbCacheEntryOptions, JsonSerializerOptions, CancellationToken)**

Sends a PATCH request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(HttpClient client, string requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions options, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-patchasjsonasync"/>**PatchAsJsonAsync&lt;TValue&gt;(HttpClient, Uri, TValue, LiteDbCacheEntryOptions, JsonSerializerOptions, CancellationToken)**

Sends a PATCH request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(HttpClient client, Uri requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions options, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-patchasjsonasync"/>**PatchAsJsonAsync&lt;TValue&gt;(HttpClient, String, TValue, LiteDbCacheEntryOptions, JsonTypeInfo&lt;TValue&gt;, CancellationToken)**

Sends a PATCH request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(HttpClient client, string requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`jsonTypeInfo` [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1)<TValue><br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-patchasjsonasync"/>**PatchAsJsonAsync&lt;TValue&gt;(HttpClient, Uri, TValue, LiteDbCacheEntryOptions, JsonTypeInfo&lt;TValue&gt;, CancellationToken)**

Sends a PATCH request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(HttpClient client, Uri requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`jsonTypeInfo` [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1)<TValue><br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-postasjsonasync"/>**PostAsJsonAsync&lt;TValue&gt;(HttpClient, String, TValue, LiteDbCacheEntryOptions, CancellationToken)**

Sends a POST request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(HttpClient client, string requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-postasjsonasync"/>**PostAsJsonAsync&lt;TValue&gt;(HttpClient, Uri, TValue, LiteDbCacheEntryOptions, CancellationToken)**

Sends a POST request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(HttpClient client, Uri requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-postasjsonasync"/>**PostAsJsonAsync&lt;TValue&gt;(HttpClient, String, TValue, LiteDbCacheEntryOptions, JsonSerializerOptions, CancellationToken)**

Sends a POST request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(HttpClient client, string requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions options, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-postasjsonasync"/>**PostAsJsonAsync&lt;TValue&gt;(HttpClient, Uri, TValue, LiteDbCacheEntryOptions, JsonSerializerOptions, CancellationToken)**

Sends a POST request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(HttpClient client, Uri requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions options, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-postasjsonasync"/>**PostAsJsonAsync&lt;TValue&gt;(HttpClient, String, TValue, LiteDbCacheEntryOptions, JsonTypeInfo&lt;TValue&gt;, CancellationToken)**

Sends a POST request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(HttpClient client, string requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`jsonTypeInfo` [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1)<TValue><br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-postasjsonasync"/>**PostAsJsonAsync&lt;TValue&gt;(HttpClient, Uri, TValue, LiteDbCacheEntryOptions, JsonTypeInfo&lt;TValue&gt;, CancellationToken)**

Sends a POST request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(HttpClient client, Uri requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`jsonTypeInfo` [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1)<TValue><br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-putasjsonasync"/>**PutAsJsonAsync&lt;TValue&gt;(HttpClient, String, TValue, LiteDbCacheEntryOptions, CancellationToken)**

Sends a PUT request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PutAsJsonAsync<TValue>(HttpClient client, string requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-putasjsonasync"/>**PutAsJsonAsync&lt;TValue&gt;(HttpClient, Uri, TValue, LiteDbCacheEntryOptions, CancellationToken)**

Sends a PUT request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PutAsJsonAsync<TValue>(HttpClient client, Uri requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-putasjsonasync"/>**PutAsJsonAsync&lt;TValue&gt;(HttpClient, String, TValue, LiteDbCacheEntryOptions, JsonSerializerOptions, CancellationToken)**

Sends a PUT request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PutAsJsonAsync<TValue>(HttpClient client, string requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions options, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-putasjsonasync"/>**PutAsJsonAsync&lt;TValue&gt;(HttpClient, Uri, TValue, LiteDbCacheEntryOptions, JsonSerializerOptions, CancellationToken)**

Sends a PUT request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PutAsJsonAsync<TValue>(HttpClient client, Uri requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonSerializerOptions options, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)<br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-putasjsonasync"/>**PutAsJsonAsync&lt;TValue&gt;(HttpClient, String, TValue, LiteDbCacheEntryOptions, JsonTypeInfo&lt;TValue&gt;, CancellationToken)**

Sends a PUT request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PutAsJsonAsync<TValue>(HttpClient client, string requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`jsonTypeInfo` [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1)<TValue><br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>

### <a id="methods-putasjsonasync"/>**PutAsJsonAsync&lt;TValue&gt;(HttpClient, Uri, TValue, LiteDbCacheEntryOptions, JsonTypeInfo&lt;TValue&gt;, CancellationToken)**

Sends a PUT request with a JSON body using request-specific cache options.

```csharp
public static Task<HttpResponseMessage> PutAsJsonAsync<TValue>(HttpClient client, Uri requestUri, TValue value, LiteDbCacheEntryOptions cacheOptions, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`client` [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient)<br>

`requestUri` [Uri](https://learn.microsoft.com/dotnet/api/system.uri)<br>

`value` TValue<br>

`cacheOptions` [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>

`jsonTypeInfo` [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1)<TValue><br>

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1)<[HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)>
