using System.Diagnostics.CodeAnalysis;

using Nefarius.HttpClient.LiteDbCache.Options;

namespace Nefarius.HttpClient.LiteDbCache.Internal;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
internal sealed class DatabaseInstanceOptions
{
    public string ClientName { get; internal set; }

    public string CollectionName { get; internal set; }

    public string ConnectionString { get; set; }

    public LiteDbCacheEntryOptions EntryOptions { get; internal set; }
}