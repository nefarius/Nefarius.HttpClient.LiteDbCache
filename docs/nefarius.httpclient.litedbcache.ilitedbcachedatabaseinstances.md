# ILiteDbCacheDatabaseInstances

Namespace: Nefarius.HttpClient.LiteDbCache

Grants access to the underlying cache database instances.

```csharp
public interface ILiteDbCacheDatabaseInstances
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Methods

### <a id="methods-delete"/>**Delete(String, ObjectId)**

Deletes a cached entry in the given instance

```csharp
bool Delete(string name, ObjectId id)
```

#### Parameters

`name` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>
The client/instance name.

`id` ObjectId<br>
The ObjectId of the database record.

#### Returns

Whether the delete succeeded.

### <a id="methods-getdatabase"/>**GetDatabase(String)**

Gets a LiteDatabase instance for a given name.

```csharp
LiteDatabase GetDatabase(string name)
```

#### Parameters

`name` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>
The client/instance name.

#### Returns

The LiteDatabase object or null if not found.

### <a id="methods-purge"/>**Purge(String)**

Purges all cached entries for the given instance.

```csharp
int Purge(string name)
```

#### Parameters

`name` [String](https://learn.microsoft.com/dotnet/api/system.string)<br>
The client/instance name.

#### Returns

The number of deleted entries.
