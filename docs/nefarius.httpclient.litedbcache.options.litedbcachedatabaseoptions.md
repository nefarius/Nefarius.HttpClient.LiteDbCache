# LiteDbCacheDatabaseOptions

Namespace: Nefarius.HttpClient.LiteDbCache.Options

Configuration properties for a LiteDB instance to use for request caching.

```csharp
public sealed class LiteDbCacheDatabaseOptions
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [LiteDbCacheDatabaseOptions](./nefarius.httpclient.litedbcache.options.litedbcachedatabaseoptions.md)

## Properties

### <a id="properties-collectionname"/>**CollectionName**

The LiteDB collection name.

```csharp
public string CollectionName { get; set; }
```

#### Property Value

[String](https://learn.microsoft.com/dotnet/api/system.string)<br>

### <a id="properties-connectionstring"/>**ConnectionString**

The LiteDB connection string.

```csharp
public string ConnectionString { get; set; }
```

#### Property Value

[String](https://learn.microsoft.com/dotnet/api/system.string)<br>

**Remarks:**

See full documentation online at https://github.com/mbdavid/LiteDB/wiki/Connection-String

### <a id="properties-entryoptions"/>**EntryOptions**

The parameters that apply to each cache entries' expiration strategy etc.

```csharp
public LiteDbCacheEntryOptions EntryOptions { get; set; }
```

#### Property Value

[LiteDbCacheEntryOptions](./nefarius.httpclient.litedbcache.options.litedbcacheentryoptions.md)<br>
