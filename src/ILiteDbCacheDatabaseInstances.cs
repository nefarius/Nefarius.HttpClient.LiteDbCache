#nullable enable
using System.Diagnostics.CodeAnalysis;

using LiteDB;

namespace Nefarius.HttpClient.LiteDbCache;

/// <summary>
///     Grants access to the underlying cache database instances.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public interface ILiteDbCacheDatabaseInstances
{
    /// <summary>
    ///     Gets a <see cref="LiteDatabase" /> instance for a given name.
    /// </summary>
    /// <param name="name">The client/instance name.</param>
    /// <returns>The <see cref="LiteDatabase" /> object or null if not found.</returns>
    LiteDatabase? GetDatabase(string name);
}