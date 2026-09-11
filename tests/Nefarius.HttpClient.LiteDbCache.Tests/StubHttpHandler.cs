using System.Net;

namespace Nefarius.HttpClient.LiteDbCache.Tests;

internal sealed class StubHttpHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, int, HttpResponseMessage> _responder;

    public StubHttpHandler(Func<HttpRequestMessage, HttpResponseMessage> responder)
        : this((request, _) => responder(request))
    {
    }

    public StubHttpHandler(Func<HttpRequestMessage, int, HttpResponseMessage> responder)
    {
        _responder = responder;
    }

    public int CallCount { get; private set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        CallCount++;
        return Task.FromResult(_responder(request, CallCount));
    }

    public static HttpResponseMessage Ok(string body = "cached-body", string contentType = "text/plain",
        Action<HttpResponseMessage>? configure = null)
    {
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent(body)
        };
        response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
        configure?.Invoke(response);
        return response;
    }
}
