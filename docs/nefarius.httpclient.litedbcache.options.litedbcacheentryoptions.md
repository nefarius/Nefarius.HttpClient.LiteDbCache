# LiteDbCacheEntryOptions

Namespace: Nefarius.HttpClient.LiteDbCache.Options

Provides the cache options for an entry in a LiteDb cache instance.

```csharp
public sealed class LiteDbCacheEntryOptions
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### <a id="properties-absoluteexpiration"/>**AbsoluteExpiration**

Gets or sets an absolute expiration date for the cache entry.

```csharp
public Nullable<DateTimeOffset> AbsoluteExpiration { internal get; set; }
```

#### Property Value

[Nullable](https://learn.microsoft.com/dotnet/api/system.nullable-1)<[DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)><br>

### <a id="properties-absoluteexpirationrelativetonow"/>**AbsoluteExpirationRelativeToNow**

Gets or sets an absolute expiration time, relative to now.

```csharp
public Nullable<TimeSpan> AbsoluteExpirationRelativeToNow { internal get; set; }
```

#### Property Value

[Nullable](https://learn.microsoft.com/dotnet/api/system.nullable-1)<[TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)><br>

### <a id="properties-cacheerrors"/>**CacheErrors**

Gets or sets whether a non-success HTTP response should also be added to the cache.

```csharp
public bool CacheErrors { internal get; set; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/dotnet/api/system.boolean)<br>

**Remarks:**

Disabled by default.

### <a id="properties-cacheresponsecontent"/>**CacheResponseContent**

Gets or sets whether the response content (body) should be cached.

```csharp
public bool CacheResponseContent { internal get; set; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/dotnet/api/system.boolean)<br>

**Remarks:**

Enabled by default.

### <a id="properties-cacheresponseheaders"/>**CacheResponseHeaders**

Gets or sets whether the response headers should be cached for each request in addition to the content.

```csharp
public bool CacheResponseHeaders { internal get; set; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/dotnet/api/system.boolean)<br>

**Remarks:**

Enabled by default.

### <a id="properties-excludedcontenttypes"/>**ExcludedContentTypes**

Collection of content types that should never be pulled from cache (e.g. application/octet-stream).

```csharp
public List<String> ExcludedContentTypes { get; internal set; }
```

#### Property Value

[List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)<[String](https://learn.microsoft.com/dotnet/api/system.string)><br>

### <a id="properties-slidingexpiration"/>**SlidingExpiration**

Gets or sets how long a cache entry can be inactive (e.g. not accessed) before it will be removed.
 This will not extend the entry lifetime beyond the absolute expiration (if set).

```csharp
public Nullable<TimeSpan> SlidingExpiration { internal get; set; }
```

#### Property Value

[Nullable](https://learn.microsoft.com/dotnet/api/system.nullable-1)<[TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)><br>

### <a id="properties-uriexclusionregex"/>**UriExclusionRegex**

Gets or sets a regular expression of URIs to exclude from caching.

```csharp
public Regex UriExclusionRegex { internal get; set; }
```

#### Property Value

[Regex](https://learn.microsoft.com/dotnet/api/system.text.regularexpressions.regex)<br>
