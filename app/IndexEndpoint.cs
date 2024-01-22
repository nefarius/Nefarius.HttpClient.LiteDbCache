using FastEndpoints;

namespace TestWebApp;

public sealed class IndexEndpoint : EndpointWithoutRequest
{
    private readonly IHttpClientFactory _clientFactory;

    public IndexEndpoint(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(ct);
    }
}