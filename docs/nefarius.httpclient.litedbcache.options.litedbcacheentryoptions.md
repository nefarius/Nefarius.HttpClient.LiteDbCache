# LiteDbCacheEntryOptions

Namespace: Nefarius.HttpClient.LiteDbCache.Options

Provides the cache options for an entry in a LiteDb cache instance.

```csharp
public sealed class LiteDbCacheEntryOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)

## Properties

### <a id="properties-absoluteexpiration"/>**AbsoluteExpiration**

Gets or sets an absolute expiration date for the cache entry.

```csharp
public Nullable<DateTimeOffset> AbsoluteExpiration { get; set; }
```

#### Property Value

[Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### <a id="properties-absoluteexpirationrelativetonow"/>**AbsoluteExpirationRelativeToNow**

Gets or sets an absolute expiration time, relative to now.

```csharp
public Nullable<TimeSpan> AbsoluteExpirationRelativeToNow { get; set; }
```

#### Property Value

[Nullable&lt;TimeSpan&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### <a id="properties-cacheerrors"/>**CacheErrors**

Gets or sets whether a non-success HTTP response should also be added to the cache.

```csharp
public bool CacheErrors { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-slidingexpiration"/>**SlidingExpiration**

Gets or sets how long a cache entry can be inactive (e.g. not accessed) before it will be removed.
 This will not extend the entry lifetime beyond the absolute expiration (if set).

```csharp
public Nullable<TimeSpan> SlidingExpiration { get; set; }
```

#### Property Value

[Nullable&lt;TimeSpan&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### <a id="properties-uriexclusionregex"/>**UriExclusionRegex**

Gets or sets a regular expression of URIs to exclude from caching.

```csharp
public Regex UriExclusionRegex { get; set; }
```

#### Property Value

Regex<br>

## Constructors

### <a id="constructors-.ctor"/>**LiteDbCacheEntryOptions()**

```csharp
public LiteDbCacheEntryOptions()
```
