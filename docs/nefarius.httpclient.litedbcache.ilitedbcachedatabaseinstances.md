# ILiteDbCacheDatabaseInstances

Namespace: Nefarius.HttpClient.LiteDbCache

Grants access to the underlying cache database instances.

```csharp
public interface ILiteDbCacheDatabaseInstances
```

## Methods

### <a id="methods-delete"/>**Delete(String, ObjectId)**

Deletes a cached entry in the given instance

```csharp
bool Delete(string name, ObjectId id)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The client/instance name.

`id` ObjectId<br>
The  of the database record.

#### Returns

Whether the delete succeeded.

### <a id="methods-getdatabase"/>**GetDatabase(String)**

Gets a  instance for a given name.

```csharp
LiteDatabase GetDatabase(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The client/instance name.

#### Returns

The  object or null if not found.

### <a id="methods-purge"/>**Purge(String)**

Purges all cached entries for the given instance.

```csharp
int Purge(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The client/instance name.

#### Returns

The number of deleted entries.
