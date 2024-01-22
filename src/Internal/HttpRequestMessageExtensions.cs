using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nefarius.HttpClient.LiteDbCache.Internal;

internal static class HttpRequestMessageExtensions
{
    /// <summary>
    ///     Calculates a fingerprint (hash) key for a given <see cref="HttpRequestMessage" />.
    /// </summary>
    /// <param name="request">The request to hash.</param>
    /// <param name="ct">Optional cancellation token.</param>
    /// <returns>A hex-string representation of the request SHA256.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public static async Task<string> ToCacheKey(this HttpRequestMessage request, CancellationToken ct = default)
    {
        if (request.RequestUri is null)
        {
            throw new InvalidOperationException("Request URI can not be null");
        }

        using SHA256 alg = SHA256.Create();

        if (request.Method == HttpMethod.Get ||
            request.Method == HttpMethod.Head ||
            request.Method == HttpMethod.Delete)
        {
            byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(request.RequestUri.ToString()));

            return hash.ToHexString();
        }

        if (request.Method == HttpMethod.Post ||
            request.Method == HttpMethod.Patch ||
            request.Method == HttpMethod.Put)
        {
            byte[] uriBytes = Encoding.UTF8.GetBytes(request.RequestUri.ToString());
            byte[] contentBytes = await request.Content!.ReadAsByteArrayAsync(ct);

            request.Content = new ByteArrayContent(contentBytes);

            byte[] hash = alg.ComputeHash(uriBytes.Concat(contentBytes).ToArray());

            return hash.ToHexString();
        }

        throw new NotImplementedException($"Method {request.Method} not implemented");
    }

    private static string ToHexString(this IEnumerable<byte> bytes)
    {
        StringBuilder builder = new();
        foreach (byte value in bytes)
        {
            builder.Append(value.ToString("X2"));
        }

        return builder.ToString();
    }
}