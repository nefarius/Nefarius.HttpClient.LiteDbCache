namespace TestWebApp;

internal sealed class DemoService : BackgroundService
{
    private readonly IHttpClientFactory _clientFactory;

    public DemoService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        HttpResponseMessage? postRet = await _clientFactory
            .CreateClient("httpbin")
            .PostAsync("/post", new StringContent("key=value"), stoppingToken);

        HttpResponseMessage checkRet = await _clientFactory
            .CreateClient("InternetConnectivityCheck")
            .GetAsync("https://www.gstatic.com/generate_204", stoppingToken);

        HttpResponseMessage ipRet = await _clientFactory
            .CreateClient("ifconfig")
            .GetAsync("/", stoppingToken);

        string? ip = await ipRet.Content.ReadAsStringAsync(stoppingToken);
    }
}