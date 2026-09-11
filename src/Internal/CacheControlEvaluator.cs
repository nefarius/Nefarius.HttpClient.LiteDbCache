#nullable enable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Nefarius.HttpClient.LiteDbCache.Internal;

/// <summary>
///     Pragmatic evaluation of response cache directives for a private HTTP client cache.
/// </summary>
internal static class CacheControlEvaluator
{
    /// <summary>
    ///     Determines whether <paramref name="response" /> may be stored and, if so, when upstream freshness ends.
    /// </summary>
    public static CacheabilityDecision Evaluate(HttpResponseMessage response, DateTimeOffset now)
    {
        ArgumentNullException.ThrowIfNull(response);

        CacheControlHeaderValue? cacheControl = response.Headers.CacheControl
                                                ?? GetContentCacheControl(response.Content.Headers);

        if (cacheControl is not null)
        {
            if (cacheControl.NoStore || cacheControl.NoCache)
            {
                return CacheabilityDecision.DoNotCache;
            }

            if (cacheControl.MaxAge is { } maxAge)
            {
                TimeSpan remaining = maxAge - GetCurrentAge(response, now);
                if (remaining <= TimeSpan.Zero)
                {
                    return CacheabilityDecision.DoNotCache;
                }

                return CacheabilityDecision.CacheUntil(now.Add(remaining));
            }
        }

        DateTimeOffset? expires = GetExpires(response);
        if (expires is null)
        {
            return CacheabilityDecision.CacheWithoutUpstreamExpiry;
        }

        return expires.Value <= now
            ? CacheabilityDecision.DoNotCache
            : CacheabilityDecision.CacheUntil(expires.Value);
    }

    private static CacheControlHeaderValue? GetContentCacheControl(HttpContentHeaders headers)
    {
        if (!headers.TryGetValues("Cache-Control", out IEnumerable<string>? values))
        {
            return null;
        }

        return CacheControlHeaderValue.TryParse(string.Join(",", values), out CacheControlHeaderValue? parsed)
            ? parsed
            : null;
    }

    internal static TimeSpan GetCurrentAge(HttpResponseMessage response, DateTimeOffset now)
    {
        TimeSpan ageHeader = response.Headers.Age.GetValueOrDefault();
        TimeSpan apparentAge = TimeSpan.Zero;

        if (response.Headers.Date is { } date)
        {
            TimeSpan delta = now - date;
            if (delta > TimeSpan.Zero)
            {
                apparentAge = delta;
            }
        }

        return apparentAge > ageHeader ? apparentAge : ageHeader;
    }

    internal static DateTimeOffset? GetExpires(HttpResponseMessage response)
    {
        if (response.Content.Headers.Expires is { } expires)
        {
            return expires;
        }

        if (!response.Headers.TryGetValues("Expires", out IEnumerable<string>? values))
        {
            return null;
        }

        string? raw = values.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(raw))
        {
            return null;
        }

        if (raw == "0")
        {
            return DateTimeOffset.MinValue;
        }

        if (DateTimeOffset.TryParseExact(raw, "r", CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out DateTimeOffset parsed) ||
            DateTimeOffset.TryParse(raw, CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out parsed))
        {
            return parsed;
        }

        return DateTimeOffset.MinValue;
    }
}

/// <summary>
///     Result of evaluating response cache headers.
/// </summary>
/// <param name="CanCache">Whether the response may be stored.</param>
/// <param name="ExpiresAt">Absolute upstream expiry, if one could be derived.</param>
internal readonly record struct CacheabilityDecision(bool CanCache, DateTimeOffset? ExpiresAt)
{
    public static CacheabilityDecision DoNotCache { get; } = new(false, null);

    public static CacheabilityDecision CacheWithoutUpstreamExpiry { get; } = new(true, null);

    public static CacheabilityDecision CacheUntil(DateTimeOffset expiresAt)
    {
        return new CacheabilityDecision(true, expiresAt);
    }
}
