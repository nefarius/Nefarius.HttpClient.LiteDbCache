# ILiteDbCacheDatabaseInstances

Namespace: Nefarius.HttpClient.LiteDbCache

Grants access to the underlying cache database instances.

```csharp
public interface ILiteDbCacheDatabaseInstances
```

## Methods

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
void Purge(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The client/instance name.
