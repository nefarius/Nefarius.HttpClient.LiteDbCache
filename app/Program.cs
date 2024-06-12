using System.Net;
using System.Net.Sockets;

using Nefarius.HttpClient.LiteDbCache;

using TestWebApp;

WebApplicationBuilder builder = WebApplication.CreateBuilder();


builder.Services.AddHttpClient("ifconfig", cfg =>
{
    cfg.BaseAddress = new Uri("https://ifconfig.me");
}).AddLiteDbCache(options =>
{
    string cacheDir = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath!)!, "caches");
    Directory.CreateDirectory(cacheDir);
    string dbPath = Path.Combine(cacheDir, "ifconfig.db");

    options.ConnectionString = dbPath;
    options.CollectionName = "cache";
    options.EntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
    
    options.EntryOptions.ExcludedContentTypes.Add("text/plain");
});

builder.Services.AddHttpClient("httpbin", cfg =>
{
    cfg.BaseAddress = new Uri("https://httpbin.org");
}).AddLiteDbCache(options =>
{
    string cacheDir = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath!)!, "caches");
    Directory.CreateDirectory(cacheDir);
    string dbPath = Path.Combine(cacheDir, "httpbin.db");

    options.ConnectionString = dbPath;
    options.CollectionName = "cache";
    options.EntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
});

builder.Services.AddHttpClient("InternetConnectivityCheck")
    .AddLiteDbCache(dbOpts =>
    {
        string cacheDir = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath!)!, "caches");
        Directory.CreateDirectory(cacheDir);
        string dbPath = Path.Combine(cacheDir, "internet-availability.db");

        dbOpts.ConnectionString = dbPath;
        dbOpts.CollectionName = "cache";
        dbOpts.EntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
    })
    .UseSocketsHttpHandler((handler, _) =>
    {
        handler.ConnectCallback = async (connectionContext, token) =>
        {
            // Use DNS to look up the IP addresses of the target host:
            // - IP v4: AddressFamily.InterNetwork
            // - IP v6: AddressFamily.InterNetworkV6
            // - IP v4 or IP v6: AddressFamily.Unspecified
            // note: this method throws a SocketException when there is no IP address for the host
            IPHostEntry entry = await Dns.GetHostEntryAsync(connectionContext.DnsEndPoint.Host,
                AddressFamily.InterNetwork, token);

            // Open the connection to the target host/port
            Socket socket = new(SocketType.Stream, ProtocolType.Tcp);

            // Turn off Nagle's algorithm since it degrades performance in most HttpClient scenarios.
            socket.NoDelay = true;

            try
            {
                await socket.ConnectAsync(entry.AddressList, connectionContext.DnsEndPoint.Port, token);

                // If you want to choose a specific IP address to connect to the server
                // await socket.ConnectAsync(
                //    entry.AddressList[Random.Shared.Next(0, entry.AddressList.Length)],
                //    context.DnsEndPoint.Port, cancellationToken);

                // Return the NetworkStream to the caller
                return new NetworkStream(socket, true);
            }
            catch
            {
                socket.Dispose();
                throw;
            }
        };
    });

builder.Services.AddHostedService<DemoService>();

WebApplication app = builder.Build();

app.Run();