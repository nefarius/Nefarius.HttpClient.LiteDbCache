using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;

using LiteDB;

namespace Nefarius.HttpClient.LiteDbCache.Internal;

/// <summary>
///     Describes a serializable <see cref="HttpResponseMessage"/> cache entry.
/// </summary>
internal sealed class CachedHttpResponseMessage
{
    /// <summary>
    ///     Database primary key.
    /// </summary>
    [BsonId]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public ObjectId Id { get; set; }

    /// <summary>
    ///     Request URI to match.
    /// </summary>
    public Uri Uri { get; set; }

    /// <summary>
    ///     Response HTTP headers.
    /// </summary>
    public Dictionary<string, List<string>> Headers { get; set; } = new();
    
    /// <summary>
    ///     Response status code.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    ///     Binary blob of response content.
    /// </summary>
    public byte[] Content { get; set; }

    /// <summary>
    ///     Timestamp of entry creation.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    ///     Timestamp of last access.
    /// </summary>
    public DateTimeOffset? LastAccessedAt { get; set; } = null;
}