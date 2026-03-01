using Microsoft.AspNetCore.Http;

namespace UserManagementApi.Middleware;

public sealed class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private const string ValidToken = "my-secret-token";

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var token) ||
            token != $"Bearer {ValidToken}")
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { error = "Unauthorized" });
            return;
        }

        await _next(context);
    }
}