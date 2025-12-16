using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

// ----------------------
// ErrorHandlingMiddleware
// Catches unhandled exceptions from downstream middleware/controllers
// and converts them into a consistent JSON 500 response used by the exercises.
// ----------------------
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;

    // InvokeAsync - wraps _next in a try/catch, writes JSON error on exceptions
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = "Internal server error." });
        }
    }
}
