using FastEndpoints;

using Nefarius.HttpClient.LiteDbCache;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

builder.Services.AddFastEndpoints();

builder.Services.AddHttpClient("ifconfig", cfg =>
{
    cfg.BaseAddress = new Uri("https://ifconfig.me");
}).AddLiteDbCache(options =>
{
    options.ConnectionString = @"C:\Temp\MyData.db";
    options.CollectionName = "cache";
});

WebApplication app = builder.Build();

app.UseFastEndpoints();

app.Run();