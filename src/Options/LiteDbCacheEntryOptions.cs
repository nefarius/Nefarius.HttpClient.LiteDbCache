#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Nefarius.HttpClient.LiteDbCache.Options;

/// <summary>
///     Provides the cache options for an entry in a LiteDb cache instance.
/// </summary>
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
[SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
public sealed class LiteDbCacheEntryOptions
{
    private TimeSpan? _absoluteExpirationRelativeToNow;
    private TimeSpan? _slidingExpiration;

    /// <summary>
    ///     Initializes a new instance of <see cref="LiteDbCacheEntryOptions" />.
    /// </summary>
    public LiteDbCacheEntryOptions() { }

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
    ///     Collection of content types that should never be pulled from cache (e.g. application/octet-stream).
    /// </summary>
    public List<string> ExcludedContentTypes { get; internal init; } = new();

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

    /// <summary>
    ///     Gets or sets whether response <c>Cache-Control</c> and <c>Expires</c> headers should influence cache storage and
    ///     lifetime.
    /// </summary>
    /// <remarks>
    ///     Disabled by default. When enabled, <c>no-store</c> and <c>no-cache</c> prevent storage,
    ///     <c>max-age</c> (accounting for <c>Date</c>/<c>Age</c>) bounds entry lifetime, and <c>Expires</c> is used only when
    ///     <c>max-age</c> is absent. Local expiration options still apply; the earliest expiry wins.
    /// </remarks>
    public bool HonorCacheControl { internal get; set; } = false;

    /// <summary>
    ///     Gets or sets whether an expired cache entry should be returned when refreshing the remote resource fails.
    /// </summary>
    /// <remarks>
    ///     Disabled by default. When enabled, expired entries are kept until a successful refresh. A refresh failure is a
    ///     transport error (timeout, connection failure) or a non-success status code when <see cref="CacheErrors" /> is
    ///     <see langword="false" />. Caller cancellation is not treated as a refresh failure.
    /// </remarks>
    public bool ServeStaleOnError { internal get; set; } = false;
}