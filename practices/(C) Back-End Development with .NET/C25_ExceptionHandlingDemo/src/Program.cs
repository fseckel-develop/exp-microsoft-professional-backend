using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(); // Enables controller support

// Configure logging (optional)
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure middleware for error handling
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Global exception caught: {ex.Message}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
    }
});


app.UseRouting();
app.MapControllers();
app.Run();
