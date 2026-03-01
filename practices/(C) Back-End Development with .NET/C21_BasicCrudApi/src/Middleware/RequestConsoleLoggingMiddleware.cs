namespace BasicCrudApi.Middleware;

public static class RequestConsoleLoggingMiddleware
{
    public static IApplicationBuilder UseRequestConsoleLogging(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            Console.WriteLine($"Incoming Request: {context.Request.Method} {context.Request.Path}");
            Console.WriteLine("Custom middleware - before next");

            await next();

            Console.WriteLine("Custom middleware - after next");
            Console.WriteLine($"Outgoing Response: {context.Response.StatusCode}");
            Console.WriteLine();
        });
    }
}