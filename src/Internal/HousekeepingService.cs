using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Microsoft.Extensions.Hosting;

namespace Nefarius.HttpClient.LiteDbCache.Internal;

internal sealed class HousekeepingService(LiteDbCacheDatabaseInstances instances, IHostApplicationLifetime lifetime)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        lifetime.ApplicationStopping.Register(() =>
        {
            // dispose instances properly
            foreach ((string _, LiteDatabase db) in instances)
            {
                db.Dispose();
            }
        });

        return Task.CompletedTask;
    }
}