namespace MiddlewarePipelineDemo.Middleware.Extensions;

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestConsoleLogging(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            Console.WriteLine($"Incoming request: {context.Request.Method} {context.Request.Path}");

            await next();

            Console.WriteLine($"Outgoing response: {context.Response.StatusCode}");
            Console.WriteLine();
        });
    }
}