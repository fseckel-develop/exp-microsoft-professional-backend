using EncryptDecryptDemo.Infrastructure;
using EncryptDecryptDemo.Routing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCrypto(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => Results.Ok("Crypto API is running."));
app.MapCryptoEndpoints();

app.Run();