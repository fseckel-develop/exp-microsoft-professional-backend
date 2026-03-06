using SecureDataStorageDemo.Routing;
using SecureDataStorageDemo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Data
builder.Services.AddAppData();

// Security (JWT + Crypto)
builder.Services.AddJwtAuth(builder.Configuration);
builder.Services.AddMessageCrypto(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => Results.Ok("Secure Messages API"));

// Debugging 
app.MapGet("/debug/bearer", (HttpContext ctx) =>
{
    var auth = ctx.Request.Headers.Authorization.ToString();
    return Results.Ok(new { authorizationHeader = auth });
});

app.MapAuthEndpoints();
app.MapMessageEndpoints();

app.Run();