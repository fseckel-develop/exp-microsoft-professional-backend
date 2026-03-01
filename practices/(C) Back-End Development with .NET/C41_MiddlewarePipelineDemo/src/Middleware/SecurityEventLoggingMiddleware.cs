namespace MiddlewarePipelineDemo.Middleware;

public sealed class SecurityEventLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityEventLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode >= 400)
        {
            Console.WriteLine(
                $"Security Event: {context.Request.Path} - Status Code: {context.Response.StatusCode}");
        }
    }
}