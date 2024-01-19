namespace Nefarius.HttpClient.LiteDbCache.Options;

/// <summary>
///     Configuration properties for a <see cref="LiteDB"/> instance to use for request caching.
/// </summary>
public sealed class CacheDatabaseOptions
{
    internal CacheDatabaseOptions()
    {
    }

    /// <summary>
    ///     The <see cref="LiteDB"/> connection string.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    ///     The <see cref="LiteDB"/> collection name.
    /// </summary>
    public string CollectionName { get; set; }
}