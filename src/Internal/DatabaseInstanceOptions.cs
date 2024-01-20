using LiteDB;

using Nefarius.HttpClient.LiteDbCache.Options;

namespace Nefarius.HttpClient.LiteDbCache.Internal;

internal sealed class DatabaseInstanceOptions
{
    public string ClientName { get; internal set; }

    public string CollectionName { get; internal set; }

    public LiteDatabase Database { get; set; }

    public LiteDbCacheEntryOptions EntryOptions { get; internal set; }
}