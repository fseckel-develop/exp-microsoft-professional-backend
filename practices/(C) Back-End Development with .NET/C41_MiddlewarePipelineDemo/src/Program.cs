using Microsoft.AspNetCore.HttpLogging;
using MiddlewarePipelineDemo.Contracts;
using MiddlewarePipelineDemo.Middleware.Extensions;
using MiddlewarePipelineDemo.Middleware;
using MiddlewarePipelineDemo.Models;
using MiddlewarePipelineDemo.Policies;

var sessions = new List<WorkoutSession>
{
    new()
    {
        Id = 1,
        Title = "Leg Day",
        Notes = "Squats, lunges, calf raises"
    },
    new()
    {
        Id = 2,
        Title = "Upper Body",
        Notes = "Bench press, pull-ups, shoulder press"
    }
};

var nextSessionId = sessions.Max(s => s.Id) + 1;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddSingleton<RequestPolicies>();

var app = builder.Build();

// Built-in middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpLogging();

// Global custom middleware
app.UseRequestTiming();
app.UseRequestConsoleLogging();

// Apply API key protection only to write operations under /sessions
app.UseWhen(
    context =>
        context.Request.Path.StartsWithSegments("/sessions") &&
        !HttpMethods.IsGet(context.Request.Method),
    branch =>
    {
        branch.UseApiKeyProtectionForWrites();
    });

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        message = "Middleware pipeline demo is running.",
        workoutApi = "/sessions",
        securityDemo = "/security-demo?secure=true&authenticated=true&input=Safe123"
    });
});

//
// Workout session API
//

var sessionRoutes = app.MapGroup("/sessions");

// Read all
sessionRoutes.MapGet("/", () =>
{
    return Results.Ok(sessions.OrderBy(s => s.Id));
});

// Read one
sessionRoutes.MapGet("/{id:int}", (int id) =>
{
    var session = sessions.FirstOrDefault(s => s.Id == id);

    return session is null
        ? Results.NotFound("Workout session not found.")
        : Results.Ok(session);
});

// Create
sessionRoutes.MapPost("/", (CreateWorkoutSessionRequestDto dto) =>
{
    if (string.IsNullOrWhiteSpace(dto.Title))
        return Results.BadRequest("Title is required.");

    var newSession = new WorkoutSession
    {
        Id = nextSessionId++,
        Title = dto.Title.Trim(),
        Notes = dto.Notes?.Trim() ?? string.Empty
    };

    sessions.Add(newSession);

    return Results.Created($"/sessions/{newSession.Id}", newSession);
});

// Update
sessionRoutes.MapPut("/{id:int}", (int id, UpdateWorkoutSessionRequestDto dto) =>
{
    var session = sessions.FirstOrDefault(s => s.Id == id);

    if (session is null)
        return Results.NotFound("Workout session not found.");

    if (string.IsNullOrWhiteSpace(dto.Title))
        return Results.BadRequest("Title is required.");

    session.Title = dto.Title.Trim();
    session.Notes = dto.Notes?.Trim() ?? string.Empty;

    return Results.Ok(session);
});

// Delete
sessionRoutes.MapDelete("/{id:int}", (int id) =>
{
    var session = sessions.FirstOrDefault(s => s.Id == id);

    if (session is null)
        return Results.NotFound("Workout session not found.");

    sessions.Remove(session);
    return Results.Ok(session);
});

//
// Security pipeline demo
//

app.Map("/security-demo", securityApp =>
{
    securityApp.UseMiddleware<SimulatedHttpsRequirementMiddleware>();
    securityApp.UseMiddleware<QueryInputValidationMiddleware>();
    securityApp.UseMiddleware<UnauthorizedPathBlockMiddleware>();
    securityApp.UseMiddleware<SimulatedAuthAndCookieMiddleware>();
    securityApp.UseMiddleware<AsyncProcessingMiddleware>();
    securityApp.UseMiddleware<FinalResponseMiddleware>();
    securityApp.UseMiddleware<SecurityEventLoggingMiddleware>();

    securityApp.Run(async context =>
    {
        if (!context.Response.HasStarted)
        {
            await context.Response.WriteAsync("Security demo completed.\n");
        }
    });
});

app.Run();