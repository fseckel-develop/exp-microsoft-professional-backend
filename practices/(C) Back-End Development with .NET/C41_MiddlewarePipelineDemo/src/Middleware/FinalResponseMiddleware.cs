namespace MiddlewarePipelineDemo.Middleware;

public sealed class FinalResponseMiddleware
{
    private readonly RequestDelegate _next;

    public FinalResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Response.HasStarted)
        {
            await context.Response.WriteAsync("Final response from middleware pipeline.\n");
        }

        await _next(context);
    }
}