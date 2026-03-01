using MiddlewarePipelineDemo.Policies;

namespace MiddlewarePipelineDemo.Middleware;

public sealed class SimulatedHttpsRequirementMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RequestPolicies _policies;

    public SimulatedHttpsRequirementMiddleware(RequestDelegate next, RequestPolicies policies)
    {
        _next = next;
        _policies = policies;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!_policies.RequiresSecureQuery(context))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Simulated HTTPS required.");
            return;
        }

        await _next(context);
    }
}