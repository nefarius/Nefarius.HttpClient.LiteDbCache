using System.Net.Http;

using Nefarius.HttpClient.LiteDbCache.Options;

namespace Nefarius.HttpClient.LiteDbCache;

/// <summary>
///     Cache-specific options for <see cref="HttpRequestMessage"/>.
/// </summary>
public static class LiteDbCacheHttpRequestOptions
{
    /// <summary>
    ///     Cache-entry-specific <see cref="LiteDbCacheEntryOptions"/>.
    /// </summary>
    public const string EntryOptions = nameof(LiteDbCacheEntryOptions);
}