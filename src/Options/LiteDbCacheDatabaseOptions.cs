namespace Nefarius.HttpClient.LiteDbCache.Options;

/// <summary>
///     Configuration properties for a <see cref="LiteDB"/> instance to use for request caching.
/// </summary>
public sealed class LiteDbCacheDatabaseOptions
{
    internal LiteDbCacheDatabaseOptions()
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

    /// <summary>
    ///     The parameters that apply to each cache entries' expiration strategy etc.
    /// </summary>
    public LiteDbCacheEntryOptions EntryOptions { get; set; } = new();
}