using ApiDependencyInjectionDemo.Services;

namespace ApiDependencyInjectionDemo.Middleware;

public static class AuditMiddlewareExtensions
{
    public static IApplicationBuilder UseAuditMiddlewareA(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            var auditService = context.RequestServices.GetRequiredService<IRequestAuditService>();
            auditService.Record("Middleware A - before next");

            await next();

            auditService.Record("Middleware A - after next");
        });
    }

    public static IApplicationBuilder UseAuditMiddlewareB(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            var auditService = context.RequestServices.GetRequiredService<IRequestAuditService>();
            auditService.Record("Middleware B - before next");

            await next();

            auditService.Record("Middleware B - after next");
        });
    }
}