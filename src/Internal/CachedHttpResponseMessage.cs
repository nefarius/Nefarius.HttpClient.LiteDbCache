#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;

using LiteDB;

namespace Nefarius.HttpClient.LiteDbCache.Internal;

/// <summary>
///     Describes a serializable <see cref="HttpResponseMessage" /> cache entry.
/// </summary>
[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
internal sealed class CachedHttpResponseMessage
{
    /// <summary>
    ///     The current schema version. Increment when <see cref="CachedHttpResponseMessage" /> changes.
    /// </summary>
    /// <remarks>Increment whenever <see cref="CachedHttpResponseMessage"/> changes in an API-breaking fashion.</remarks>
    public const int CurrentSchemaVersion = 2;

    /// <summary>
    ///     Database primary key.
    /// </summary>
    [BsonId]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public ObjectId Id { get; set; } = ObjectId.NewObjectId();

    /// <summary>
    ///     The key (SHA256 hash) to match against.
    /// </summary>
    public string Key { get; set; } = null!;

    /// <summary>
    ///     The version of the <see cref="CachedHttpResponseMessage" /> schema.
    /// </summary>
    public int SchemaVersion { get; set; } = 0;

    /// <summary>
    ///     Response HTTP headers.
    /// </summary>
    public Dictionary<string, List<string>> Headers { get; set; } = new();

    /// <summary>
    ///     Response status code.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    ///     ID of the content file in FileStore, if any.
    /// </summary>
    public string? ContentFileId { get; set; }

    /// <summary>
    ///     Timestamp of entry creation.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    ///     Timestamp of last access.
    /// </summary>
    public DateTimeOffset? LastAccessedAt { get; set; }

    /// <summary>
    ///     The response content type.
    /// </summary>
    public string? ContentType { get; set; }

    public override string ToString()
    {
        return $"{Key} (ID: {Id})";
    }
}