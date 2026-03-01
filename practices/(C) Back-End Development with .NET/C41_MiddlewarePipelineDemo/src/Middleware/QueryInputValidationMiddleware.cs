using MiddlewarePipelineDemo.Policies;

namespace MiddlewarePipelineDemo.Middleware;

public sealed class QueryInputValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RequestPolicies _policies;

    public QueryInputValidationMiddleware(RequestDelegate next, RequestPolicies policies)
    {
        _next = next;
        _policies = policies;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!_policies.IsValidInput(context))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Invalid input.");
            return;
        }

        await _next(context);
    }
}