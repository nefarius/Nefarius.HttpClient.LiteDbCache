namespace Nefarius.HttpClient.LiteDbCache.Internal;

internal sealed class DatabaseInstanceOptions
{
    public string ClientName { get; internal set; }

    public string ConnectionString { get; internal set; }

    public string CollectionName { get; internal set; }
}