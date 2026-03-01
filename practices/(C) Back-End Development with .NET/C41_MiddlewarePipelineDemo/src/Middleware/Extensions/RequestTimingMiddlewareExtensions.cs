namespace MiddlewarePipelineDemo.Middleware.Extensions;

public static class RequestTimingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestTiming(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            var startTime = DateTime.UtcNow;

            await next();

            var elapsed = DateTime.UtcNow - startTime;
            Console.WriteLine($"Request processed in {elapsed.TotalMilliseconds:F2} ms");
        });
    }
}