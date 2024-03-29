﻿#nullable enable

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
    ///     Configures a named <see cref="LiteDB" /> cache instance for a given <see cref="IHttpClientBuilder" />.
    /// </summary>
    public static IHttpClientBuilder AddLiteDbCache(this IHttpClientBuilder builder,
        Action<LiteDbCacheDatabaseOptions> configuration)
    {
        LiteDbCacheDatabaseOptions dbOptions = new();

        configuration.Invoke(dbOptions);

        if (string.IsNullOrEmpty(dbOptions.ConnectionString))
        {
            throw new ArgumentException($"{nameof(LiteDbCacheDatabaseOptions.ConnectionString)} must not be empty");
        }

        if (string.IsNullOrEmpty(dbOptions.CollectionName))
        {
            throw new ArgumentException($"{nameof(LiteDbCacheDatabaseOptions.CollectionName)} must not be empty");
        }

        if (dbOptions.EntryOptions is null)
        {
            throw new ArgumentException($"{nameof(LiteDbCacheDatabaseOptions.EntryOptions)} must not be null");
        }

        // store client name to database instance association
        builder.Services.Configure<DatabaseInstanceOptions>(builder.Name, options =>
        {
            options.ClientName = builder.Name;
            options.ConnectionString = dbOptions.ConnectionString;
            options.CollectionName = dbOptions.CollectionName;
            options.EntryOptions = dbOptions.EntryOptions;
        });

        // stores name to database object association
        builder.Services.TryAddSingleton<LiteDbCacheDatabaseInstances>();
        // exposes a public interface to interact with the DB instance
        builder.Services.TryAddSingleton<ILiteDbCacheDatabaseInstances>(sp =>
            sp.GetRequiredService<LiteDbCacheDatabaseInstances>());

        // registers message handler
        builder.Services.AddTransient<LiteDbCacheHandler>(sp =>
            ActivatorUtilities.CreateInstance<LiteDbCacheHandler>(
                sp,
                sp.GetRequiredService<IOptionsSnapshot<DatabaseInstanceOptions>>(),
                builder.Name
            )
        );

        // housekeeping service takes care of clean DB shutdown etc.
        builder.Services.AddHostedService<HousekeepingService>();

        // adds message handler to HTTP client
        return builder.AddHttpMessageHandler(sp =>
            ActivatorUtilities.CreateInstance<LiteDbCacheHandler>(sp,
                sp.GetRequiredService<IOptionsSnapshot<DatabaseInstanceOptions>>(),
                builder.Name
            )
        );
    }
}