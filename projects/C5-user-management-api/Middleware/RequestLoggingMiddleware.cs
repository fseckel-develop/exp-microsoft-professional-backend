using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

// ----------------------
// RequestLoggingMiddleware
// Logs incoming requests and outgoing responses using the registered ILogger.
// Useful for tracing example traffic during exercises and tests.
// ----------------------
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    // InvokeAsync - logs method/path before the next middleware and status after.
    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Incoming Request: {Method} {Path}", context.Request.Method, context.Request.Path);
        await _next(context);
        _logger.LogInformation("Outgoing Response: {StatusCode}", context.Response.StatusCode);
    }
}
