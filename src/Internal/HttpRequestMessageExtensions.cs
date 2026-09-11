using System;
using System.Collections.Generic;
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

        using IncrementalHash hasher = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        byte[] separator = { 0 };

        hasher.AppendData(Encoding.UTF8.GetBytes(request.Method.Method));
        hasher.AppendData(separator);
        hasher.AppendData(Encoding.UTF8.GetBytes(request.RequestUri.ToString()));

        if (request.Method == HttpMethod.Get ||
            request.Method == HttpMethod.Head ||
            request.Method == HttpMethod.Delete)
        {
            return hasher.GetHashAndReset().ToHexString();
        }

        if (request.Method == HttpMethod.Post ||
            request.Method == HttpMethod.Patch ||
            request.Method == HttpMethod.Put)
        {
            byte[] contentBytes;

            if (request.Content is null)
            {
                contentBytes = Array.Empty<byte>();
            }
            else
            {
                HttpContent originalContent = request.Content;
                contentBytes = await originalContent.ReadAsByteArrayAsync(ct);
                ByteArrayContent buffered = new(contentBytes);

                foreach (KeyValuePair<string, IEnumerable<string>> header in originalContent.Headers)
                {
                    buffered.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }

                request.Content = buffered;
            }

            hasher.AppendData(separator);
            hasher.AppendData(contentBytes);

            return hasher.GetHashAndReset().ToHexString();
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
