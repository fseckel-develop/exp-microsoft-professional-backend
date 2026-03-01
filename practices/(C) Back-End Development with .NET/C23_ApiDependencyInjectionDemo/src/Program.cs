using ApiDependencyInjectionDemo.Middleware;
using ApiDependencyInjectionDemo.Services;

var builder = WebApplication.CreateBuilder(args);

/*
---------------------------------
Dependency Injection Registration
---------------------------------

ASP.NET Core includes a built-in DI container.
Services are registered here so they can later be injected into:
    - Middleware
    - Endpoints
    - Controllers
    - Other services

The service lifetime determines how often an instance is created:
    Singleton: one instance for the entire application lifetime
    Scoped:    one instance per HTTP request
    Transient: new instance every time it is requested

Uncomment ONE registration below to observe the behavior.
*/

builder.Services.AddSingleton<IRequestAuditService, RequestAuditService>();
// builder.Services.AddScoped<IRequestAuditService, RequestAuditService>();
// builder.Services.AddTransient<IRequestAuditService, RequestAuditService>();

var app = builder.Build();

/*
-------------------
Middleware Pipeline
-------------------

These extension methods register custom middleware components that
resolve IRequestAuditService from the DI container for each request.

The middleware logs activity before and after the next pipeline step,
allowing the observation of how the service instance behaves across the
request lifecycle.
*/
app.UseAuditMiddlewareA();
app.UseAuditMiddlewareB();

/*
---------
Endpoints
---------

Services registered in the DI container can also be injected directly
into endpoint handlers. ASP.NET Core resolves them automatically.

Here we inject IRequestAuditService into the endpoint to record
activity and return the service instance information in the response.
*/
app.MapGet("/", (IRequestAuditService auditService) =>
{
    auditService.Record("Endpoint: GET /");

    return Results.Ok(new
    {
        message = "Inspect the response and console to observe DI lifetime behavior.",
        auditServiceInstanceId = auditService.InstanceId,
        records = auditService.GetRecords()
    });
});

app.MapGet("/details", (HttpContext context, IRequestAuditService auditService) =>
{
    auditService.Record("Endpoint: GET /details");

    return Results.Ok(new
    {
        path = context.Request.Path.ToString(),
        auditServiceInstanceId = auditService.InstanceId,
        records = auditService.GetRecords(),
        explanation =
            "Singleton keeps one instance for the whole app. " +
            "Scoped creates one per request. " +
            "Transient creates a new instance every resolution."
    });
});

app.Run();