using MiddlewarePipelineDemo.Policies;

namespace MiddlewarePipelineDemo.Middleware;

public sealed class UnauthorizedPathBlockMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RequestPolicies _policies;

    public UnauthorizedPathBlockMiddleware(RequestDelegate next, RequestPolicies policies)
    {
        _next = next;
        _policies = policies;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (_policies.IsUnauthorizedPath(context))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized access.");
            return;
        }

        await _next(context);
    }
}