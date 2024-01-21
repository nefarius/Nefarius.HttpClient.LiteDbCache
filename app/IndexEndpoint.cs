﻿using FastEndpoints;

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
        HttpClient client = _clientFactory.CreateClient("ifconfig");

        HttpResponseMessage result = await client.GetAsync("/", ct);

        if (!result.IsSuccessStatusCode)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        string? ip = await result.Content.ReadAsStringAsync(ct);

        await SendOkAsync(ip, ct);
    }
}