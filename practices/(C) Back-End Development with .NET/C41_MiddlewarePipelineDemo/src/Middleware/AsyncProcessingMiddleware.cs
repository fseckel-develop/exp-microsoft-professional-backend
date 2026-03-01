namespace MiddlewarePipelineDemo.Middleware;

public sealed class AsyncProcessingMiddleware
{
    private readonly RequestDelegate _next;

    public AsyncProcessingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await Task.Delay(100);

        if (!context.Response.HasStarted)
        {
            await context.Response.WriteAsync("Processed asynchronously.\n");
        }

        await _next(context);
    }
}