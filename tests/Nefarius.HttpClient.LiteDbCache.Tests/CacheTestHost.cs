using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Nefarius.HttpClient.LiteDbCache.Options;

namespace Nefarius.HttpClient.LiteDbCache.Tests;

internal sealed class CacheTestHost : IAsyncDisposable
{
    private readonly string _cacheDirectory;

    private CacheTestHost(IHost host, StubHttpHandler stub, global::System.Net.Http.HttpClient client,
        string cacheDirectory, ILiteDbCacheDatabaseInstances databases)
    {
        Host = host;
        Stub = stub;
        Client = client;
        _cacheDirectory = cacheDirectory;
        Databases = databases;
    }

    public IHost Host { get; }

    public StubHttpHandler Stub { get; }

    public global::System.Net.Http.HttpClient Client { get; }

    public ILiteDbCacheDatabaseInstances Databases { get; }

    public const string ClientName = "cached";

    public static CacheTestHost Create(Action<LiteDbCacheEntryOptions>? configureEntry = null,
        Func<HttpRequestMessage, int, HttpResponseMessage>? responder = null)
    {
        StubHttpHandler stub = new(responder ?? ((_, _) => StubHttpHandler.Ok()));
        string cacheDirectory = Path.Combine(Path.GetTempPath(), "LiteDbCacheTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(cacheDirectory);
        string dbPath = Path.Combine(cacheDirectory, "cache.db");

        HostApplicationBuilder builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder();
        builder.Services.AddHttpClient(ClientName, client =>
            {
                client.BaseAddress = new Uri("https://cache.test");
            })
            .AddLiteDbCache(options =>
            {
                options.ConnectionString = dbPath;
                options.CollectionName = "cache";
                configureEntry?.Invoke(options.EntryOptions);
            })
            .ConfigurePrimaryHttpMessageHandler(() => stub);

        IHost host = builder.Build();
        global::System.Net.Http.HttpClient client =
            host.Services.GetRequiredService<IHttpClientFactory>().CreateClient(ClientName);
        ILiteDbCacheDatabaseInstances databases = host.Services.GetRequiredService<ILiteDbCacheDatabaseInstances>();

        return new CacheTestHost(host, stub, client, cacheDirectory, databases);
    }

    public async ValueTask DisposeAsync()
    {
        Client.Dispose();
        await Host.StopAsync();
        Host.Dispose();

        try
        {
            if (Directory.Exists(_cacheDirectory))
            {
                Directory.Delete(_cacheDirectory, true);
            }
        }
        catch (IOException)
        {
            // LiteDB may keep a handle briefly on some platforms.
        }
    }
}
