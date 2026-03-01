using MiddlewarePipelineDemo.Policies;

namespace MiddlewarePipelineDemo.Middleware;

public sealed class SimulatedAuthAndCookieMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RequestPolicies _policies;

    public SimulatedAuthAndCookieMiddleware(RequestDelegate next, RequestPolicies policies)
    {
        _next = next;
        _policies = policies;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!_policies.IsAuthenticated(context))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Access denied.");
            return;
        }

        context.Response.Cookies.Append("SecureCookie", "SecureData", new CookieOptions
        {
            HttpOnly = true,
            Secure = true
        });

        await _next(context);
    }
}