using System;

namespace Nefarius.HttpClient.LiteDbCache.Options;

/// <summary>
///     Provides the cache options for an entry in a LiteDb cache instance.
/// </summary>
public sealed class LiteDbCacheEntryOptions
{
    private TimeSpan? _absoluteExpirationRelativeToNow;
    private TimeSpan? _slidingExpiration;

    /// <summary>
    ///     Gets or sets an absolute expiration date for the cache entry.
    /// </summary>
    public DateTimeOffset? AbsoluteExpiration { get; set; }

    /// <summary>
    ///     Gets or sets an absolute expiration time, relative to now.
    /// </summary>
    public TimeSpan? AbsoluteExpirationRelativeToNow
    {
        get => _absoluteExpirationRelativeToNow;
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
        get => _slidingExpiration;
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
    public bool CacheErrors { get; set; } = false;
}