namespace MiddlewarePipelineDemo.Middleware.Extensions;

public static class ApiKeyMiddlewareExtensions
{
    private const string DemoApiKey = "thisIsABadPassword";

    public static IApplicationBuilder UseApiKeyProtectionForWrites(this IApplicationBuilder app)
    {
        return app.UseWhen(
            context => !HttpMethods.IsGet(context.Request.Method),
            branch =>
            {
                branch.Use(async (context, next) =>
                {
                    var apiKey = context.Request.Headers["X-Api-Key"].ToString();

                    if (apiKey == DemoApiKey)
                    {
                        await next();
                        return;
                    }

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid API key.");
                });
            });
    }
}