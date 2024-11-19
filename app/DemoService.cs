using LiteDB;

using Nefarius.HttpClient.LiteDbCache;

namespace TestWebApp;

internal sealed class DemoService : BackgroundService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ILiteDbCacheDatabaseInstances _instances;

    public DemoService(IHttpClientFactory clientFactory, ILiteDbCacheDatabaseInstances instances)
    {
        _clientFactory = clientFactory;
        _instances = instances;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: turn these into unit tests :P

        LiteDatabase? db = _instances.GetDatabase("httpbin");
        // null expected

        HttpResponseMessage? postRet = await _clientFactory
            .CreateClient("httpbin")
            .PostAsync("/post", new StringContent("key=value"), stoppingToken);

        string postBody = await postRet.Content.ReadAsStringAsync(stoppingToken);

        db = _instances.GetDatabase("httpbin");
        // not null expected

        HttpResponseMessage checkRet = await _clientFactory
            .CreateClient("InternetConnectivityCheck")
            .GetAsync("https://www.gstatic.com/generate_204", stoppingToken);

        HttpResponseMessage ipRet = await _clientFactory
            .CreateClient("ifconfig")
            .GetAsync("/", stoppingToken);

        string? ip = await ipRet.Content.ReadAsStringAsync(stoppingToken);

        if (ipRet.IsCached())
        {
            ObjectId? id = ipRet.GetCacheId();
            _instances.Delete("ifconfig", id!);
        }
    }
}