using System;
using System.Collections.Generic;

using LiteDB;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Nefarius.HttpClient.LiteDbCache.Internal;

/// <summary>
///     Holds the association if HTTP client name to database instance.
/// </summary>
internal sealed class CacheDatabaseInstances : Dictionary<string, LiteDatabase>
{
    private readonly IServiceProvider _sp;

    public CacheDatabaseInstances(IServiceProvider sp)
    {
        _sp = sp;
    }

    /// <summary>
    ///     Gets (or creates) a <see cref="LiteDatabase" /> instance for a given name.
    /// </summary>
    /// <param name="name">The client/instance name.</param>
    /// <returns>The <see cref="LiteDatabase" /> object.</returns>
    public LiteDatabase GetDatabase(string name)
    {
        if (TryGetValue(name, out LiteDatabase db))
        {
            return db;
        }

        // an IOptionsSnapshot is scoped
        using IServiceScope scope = _sp.CreateScope();

        IOptionsSnapshot<DatabaseInstanceOptions> options =
            scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<DatabaseInstanceOptions>>();

        DatabaseInstanceOptions instance = options.Get(name);

        db = new LiteDatabase(instance.ConnectionString);

        Add(name, db);

        return db;
    }
}