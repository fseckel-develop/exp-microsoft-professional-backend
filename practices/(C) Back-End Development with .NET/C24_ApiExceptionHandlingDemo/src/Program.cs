using ApiExceptionHandlingDemo.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Global exception middleware catches exceptions from downstream middleware
// and controllers, then converts them into consistent HTTP responses.
app.UseGlobalExceptionHandling();

app.MapGet("/", () => Results.Ok(new
{
    message = "Exception handling demo available at /api/textanalysis/average-word-length"
}));

app.MapControllers();

app.Run();