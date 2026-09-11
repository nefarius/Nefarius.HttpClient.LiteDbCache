using System.Net;
using System.Net.Http.Headers;

using Nefarius.HttpClient.LiteDbCache.Internal;

using Xunit;

namespace Nefarius.HttpClient.LiteDbCache.Tests;

public class CacheControlEvaluatorTests
{
    private static readonly DateTimeOffset Now = new(2026, 9, 11, 18, 0, 0, TimeSpan.Zero);

    [Fact]
    public void NoStore_IsNotCacheable()
    {
        HttpResponseMessage response = CreateResponse();
        response.Headers.CacheControl = new CacheControlHeaderValue { NoStore = true };

        CacheabilityDecision decision = CacheControlEvaluator.Evaluate(response, Now);

        Assert.False(decision.CanCache);
    }

    [Fact]
    public void NoCache_IsNotCacheable()
    {
        HttpResponseMessage response = CreateResponse();
        response.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true };

        CacheabilityDecision decision = CacheControlEvaluator.Evaluate(response, Now);

        Assert.False(decision.CanCache);
    }

    [Fact]
    public void MaxAge_ComputesRemainingLifetimeFromDateAndAge()
    {
        HttpResponseMessage response = CreateResponse();
        response.Headers.Date = Now.AddSeconds(-20);
        response.Headers.Age = TimeSpan.FromSeconds(5);
        response.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = TimeSpan.FromSeconds(60) };

        CacheabilityDecision decision = CacheControlEvaluator.Evaluate(response, Now);

        Assert.True(decision.CanCache);
        Assert.Equal(Now.AddSeconds(40), decision.ExpiresAt);
    }

    [Fact]
    public void MaxAge_AlreadyStale_IsNotCacheable()
    {
        HttpResponseMessage response = CreateResponse();
        response.Headers.Date = Now.AddSeconds(-90);
        response.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = TimeSpan.FromSeconds(60) };

        CacheabilityDecision decision = CacheControlEvaluator.Evaluate(response, Now);

        Assert.False(decision.CanCache);
    }

    [Fact]
    public void Expires_IsUsedWhenMaxAgeIsAbsent()
    {
        DateTimeOffset expires = Now.AddMinutes(10);
        HttpResponseMessage response = CreateResponse();
        response.Content.Headers.Expires = expires;

        CacheabilityDecision decision = CacheControlEvaluator.Evaluate(response, Now);

        Assert.True(decision.CanCache);
        Assert.Equal(expires, decision.ExpiresAt);
    }

    [Fact]
    public void Expires_InThePast_IsNotCacheable()
    {
        HttpResponseMessage response = CreateResponse();
        response.Content.Headers.Expires = Now.AddMinutes(-1);

        CacheabilityDecision decision = CacheControlEvaluator.Evaluate(response, Now);

        Assert.False(decision.CanCache);
    }

    [Fact]
    public void MaxAge_TakesPrecedenceOverExpires()
    {
        HttpResponseMessage response = CreateResponse();
        response.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = TimeSpan.FromSeconds(30) };
        response.Content.Headers.Expires = Now.AddHours(-1);

        CacheabilityDecision decision = CacheControlEvaluator.Evaluate(response, Now);

        Assert.True(decision.CanCache);
        Assert.Equal(Now.AddSeconds(30), decision.ExpiresAt);
    }

    [Fact]
    public void NoDirectives_AllowsCachingWithoutUpstreamExpiry()
    {
        CacheabilityDecision decision = CacheControlEvaluator.Evaluate(CreateResponse(), Now);

        Assert.True(decision.CanCache);
        Assert.Null(decision.ExpiresAt);
    }

    private static HttpResponseMessage CreateResponse()
    {
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("ok")
        };
    }
}
