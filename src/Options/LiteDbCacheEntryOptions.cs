#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Nefarius.HttpClient.LiteDbCache.Options;

/// <summary>
///     Provides the cache options for an entry in a LiteDb cache instance.
/// </summary>
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public sealed class LiteDbCacheEntryOptions
{
    private TimeSpan? _absoluteExpirationRelativeToNow;
    private TimeSpan? _slidingExpiration;

    /// <summary>
    ///     Gets or sets an absolute expiration date for the cache entry.
    /// </summary>
    public DateTimeOffset? AbsoluteExpiration { internal get; set; }

    /// <summary>
    ///     Gets or sets an absolute expiration time, relative to now.
    /// </summary>
    public TimeSpan? AbsoluteExpirationRelativeToNow
    {
        internal get => _absoluteExpirationRelativeToNow;
        set
        {
            if (value <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(AbsoluteExpirationRelativeToNow),
                    value,
                    "The relative expiration value must be positive.");
            }

            _absoluteExpirationRelativeToNow = value;
        }
    }

    /// <summary>
    ///     Gets or sets how long a cache entry can be inactive (e.g. not accessed) before it will be removed.
    ///     This will not extend the entry lifetime beyond the absolute expiration (if set).
    /// </summary>
    public TimeSpan? SlidingExpiration
    {
        internal get => _slidingExpiration;
        set
        {
            if (value <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(SlidingExpiration),
                    value,
                    "The sliding expiration value must be positive.");
            }

            _slidingExpiration = value;
        }
    }

    /// <summary>
    ///     Gets or sets whether a non-success HTTP response should also be added to the cache.
    /// </summary>
    /// <remarks>Disabled by default.</remarks>
    public bool CacheErrors { internal get; set; } = false;

    /// <summary>
    ///     Gets or sets a regular expression of URIs to exclude from caching.
    /// </summary>
    public Regex? UriExclusionRegex { internal get; set; }

    /// <summary>
    ///     Gets or sets whether the response headers should be cached for each request in addition to the content.
    /// </summary>
    /// <remarks>Enabled by default.</remarks>
    public bool CacheResponseHeaders { internal get; set; } = true;

    /// <summary>
    ///     Gets or sets whether the response content (body) should be cached.
    /// </summary>
    /// <remarks>Enabled by default.</remarks>
    public bool CacheResponseContent { internal get; set; } = true;
}