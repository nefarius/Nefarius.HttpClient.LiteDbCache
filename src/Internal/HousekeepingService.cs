using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Microsoft.Extensions.Hosting;

namespace Nefarius.HttpClient.LiteDbCache.Internal;

internal sealed class HousekeepingService : BackgroundService
{
    private readonly CacheDatabaseInstances _instances;
    private readonly IHostApplicationLifetime _lifetime;

    public HousekeepingService(CacheDatabaseInstances instances, IHostApplicationLifetime lifetime)
    {
        _instances = instances;
        _lifetime = lifetime;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _lifetime.ApplicationStopping.Register(() =>
        {
            // dispose instances properly
            foreach ((string _, LiteDatabase db) in _instances)
            {
                db.Dispose();
            }
        });

        return Task.CompletedTask;
    }
}