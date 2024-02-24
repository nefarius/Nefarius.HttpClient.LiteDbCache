#nullable enable

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using LiteDB;

namespace Nefarius.HttpClient.LiteDbCache;

/// <summary>
///     <see cref="HttpResponseMessage" /> extensions.
/// </summary>
public static class HttpResponseMessageExtensions
{
    /// <summary>
    ///     Checks whether a <see cref="HttpResponseMessage" /> was pulled form a <see cref="LiteDatabase" /> cache instance.
    /// </summary>
    /// <param name="message">The <see cref="HttpResponseMessage" /> to check.</param>
    /// <returns>True if pulled from cache, false otherwise.</returns>
    public static bool IsCached(this HttpResponseMessage message)
    {
        return message.Headers.TryGetValues(LiteDbCacheHeaders.CacheHit, out IEnumerable<string>? values) &&
               values.Any(v => bool.TryParse(v, out bool isSet) && isSet);
    }

    /// <summary>
    ///     Gets the <see cref="LiteDbCacheHeaders.CacheId" /> of the <see cref="HttpResponseMessage" />.
    /// </summary>
    /// <param name="message">The <see cref="HttpResponseMessage" /> to read.</param>
    /// <returns>The <see cref="ObjectId"/> or null.</returns>
    public static ObjectId? GetCacheId(this HttpResponseMessage message)
    {
        return !message.Headers.TryGetValues(LiteDbCacheHeaders.CacheId, out IEnumerable<string>? cacheId)
            ? null
            : new ObjectId(cacheId.Single());
    }
}