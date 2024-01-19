#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Nefarius.HttpClient.LiteDbCache.Internal;
using Nefarius.HttpClient.LiteDbCache.Options;

namespace Nefarius.HttpClient.LiteDbCache;

/// <summary>
///     Extensions for <see cref="IHttpClientBuilder" />.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class HttpClientBuilderExtensions
{
    /// <summary>
    ///     Configures a named <see cref="LiteDB"/> cache instance for a given <see cref="IHttpClientBuilder"/>. 
    /// </summary>
    public static IHttpClientBuilder AddLiteDbCache(this IHttpClientBuilder builder,
        Action<CacheDatabaseOptions> configuration)
    {
        CacheDatabaseOptions dbOptions = new();

        configuration.Invoke(dbOptions);

        // link the name of the client to the database instance to use
        string name = builder.Name;

        // store client name to database instance association
        builder.Services.Configure<DatabaseInstanceOptions>(name, options =>
        {
            options.ClientName = name;
            options.ConnectionString = dbOptions.ConnectionString;
            options.CollectionName = dbOptions.CollectionName;
        });

        // registers message handler
        builder.Services.TryAddTransient<LiteDbCacheHandler>(sp =>
            ActivatorUtilities.CreateInstance<LiteDbCacheHandler>(sp,
                sp.GetRequiredService<IOptionsSnapshot<DatabaseInstanceOptions>>(), name));

        // adds message handler to HTTP client
        return builder.AddHttpMessageHandler<LiteDbCacheHandler>();
    }
}