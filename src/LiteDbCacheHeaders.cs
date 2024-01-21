namespace Nefarius.HttpClient.LiteDbCache;

/// <summary>
///     Cached response additional headers.
/// </summary>
public static class LiteDbCacheHeaders
{
    /// <summary>
    ///     Gets whether the entry was pulled from cache.
    /// </summary>
    public const string CacheHit = "X-LiteDb-Cache-Hit";

    /// <summary>
    ///     Gets the internal database ID of the entry.
    /// </summary>
    public const string CacheId = "X-LiteDb-Cache-Id";

    /// <summary>
    ///     Gets the instance name (HTTP/Database instance) the entry was pulled out of.
    /// </summary>
    public const string CacheInstance = "X-LiteDb-Cache-Instance";

    /// <summary>
    ///     Gets the entry creation timestamp.
    /// </summary>
    public const string CacheCreatedAt = "X-LiteDb-Cache-Created-At";
}