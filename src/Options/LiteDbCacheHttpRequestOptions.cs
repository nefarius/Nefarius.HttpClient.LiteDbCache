using System.Net.Http;

namespace Nefarius.HttpClient.LiteDbCache.Options;

/// <summary>
///     Cache-specific options for <see cref="HttpRequestMessage"/>.
/// </summary>
public static class LiteDbCacheHttpRequestOptions
{
    /// <summary>
    ///     Cache-entry-specific <see cref="LiteDbCacheEntryOptions"/>.
    /// </summary>
    public const string EntryOptions = nameof(LiteDbCacheEntryOptions);

    /// <summary>
    ///     Strongly typed <see cref="HttpRequestOptionsKey{TValue}"/> for
    ///     <see cref="LiteDbCacheEntryOptions"/> stored on <see cref="HttpRequestMessage.Options"/>.
    /// </summary>
    public static HttpRequestOptionsKey<LiteDbCacheEntryOptions> EntryOptionsKey { get; } = new(EntryOptions);
}
