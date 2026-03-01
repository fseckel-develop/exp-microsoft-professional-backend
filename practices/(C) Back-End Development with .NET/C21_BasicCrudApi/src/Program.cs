using BasicCrudApi.Contracts;
using BasicCrudApi.Middleware;
using BasicCrudApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register built-in HTTP request/response logging.
builder.Services.AddHttpLogging();

var app = builder.Build();

// In-memory demo data for the CRUD endpoints.
// This keeps the project simple and focused on API fundamentals.
var tasks = new List<TaskItem>
{
    new()
    {
        Id = 1,
        Title = "Buy groceries",
        Description = "Milk, Eggs, Bread",
        IsCompleted = false
    },
    new()
    {
        Id = 2,
        Title = "Finish report",
        Description = "Complete the Q1 financial report",
        IsCompleted = true
    }
};

var nextTaskId = tasks.Max(t => t.Id) + 1;

//
// Middleware pipeline
//
// Middleware runs in the order it is registered.
// Each middleware can do work before and after the next step in the pipeline.
//

app.UseHttpLogging();

// Custom middleware extracted into an extension method.
// Demonstrates how middleware can be modularized.
app.UseRequestConsoleLogging();

// Inline middleware example to show pipeline flow.
app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware 2 - before next");

    await next();

    Console.WriteLine("Middleware 2 - after next");
});


//
// General demo endpoints
//

app.MapGet("/", () => "Welcome to the Basic API Playground!");

// Route parameters with constraints:
// - id must be an integer
// - id must be >= 0
// - slug is optional
app.MapGet("/users/{id:int:min(0)}/posts/{slug?}", (int id, string? slug) =>
{
    return $"User ID: {id}, Post Slug: {slug ?? "No slug provided"}";
});

// Catch-all route parameter:
// captures the remaining path as one string.
app.MapGet("/files/{*filepath}", (string filepath) =>
{
    return $"File path: {filepath}";
});

// Query string parameters:
// /search?query=api&page=2
app.MapGet("/search", (string? query, int page = 1) =>
{
    return $"Searching for \"{query ?? "nothing"}\" on page {page}.";
});


//
// Task endpoints
//
// Route grouping keeps related endpoints together.
// This section demonstrates the CRUD concept:
//
//   C - Create   -> POST
//   R - Read     -> GET
//   U - Update   -> PUT / PATCH
//   D - Delete   -> DELETE
//

var taskRoutes = app.MapGroup("/tasks");

//
// READ
//

// Get all tasks
taskRoutes.MapGet("/", () =>
{
    return Results.Ok(tasks);
});

// Read only completed tasks
taskRoutes.MapGet("/completed", () =>
{
    return Results.Ok(tasks.Where(t => t.IsCompleted));
});

// Read one task by ID
taskRoutes.MapGet("/{id:int}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    return task is null
        ? Results.NotFound("Task not found.")
        : Results.Ok(task);
})
.WithName("GetTaskById");

//
// CREATE
//

taskRoutes.MapPost("/", (CreateTaskRequestDto dto) =>
{
    // Basic validation:
    // Title is required before creating a task.
    if (string.IsNullOrWhiteSpace(dto.Title))
        return Results.BadRequest("Title is required.");

    var task = new TaskItem
    {
        Id = nextTaskId++,
        Title = dto.Title.Trim(),
        Description = dto.Description?.Trim() ?? string.Empty,
        IsCompleted = false
    };

    tasks.Add(task);

    // CreatedAtRoute returns HTTP 201 and a location header
    // pointing to the new resource.
    return Results.CreatedAtRoute("GetTaskById", new { id = task.Id }, task);
});

//
// UPDATE
//

// Full update of a task resource
taskRoutes.MapPut("/{id:int}", (int id, UpdateTaskRequestDto dto) =>
{
    var existingTask = tasks.FirstOrDefault(t => t.Id == id);

    if (existingTask is null)
        return Results.NotFound("Task not found.");

    // Basic validation:
    // Updates must still provide a valid title.
    if (string.IsNullOrWhiteSpace(dto.Title))
        return Results.BadRequest("Title is required.");

    existingTask.Title = dto.Title.Trim();
    existingTask.Description = dto.Description?.Trim() ?? string.Empty;
    existingTask.IsCompleted = dto.IsCompleted;

    return Results.Ok(existingTask);
});

// Partial update:
// only changes the completion state.
taskRoutes.MapPatch("/{id:int}/complete", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task is null)
        return Results.NotFound("Task not found.");

    task.IsCompleted = true;
    return Results.Ok(task);
});

//
// DELETE
//

taskRoutes.MapDelete("/{id:int}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task is null)
        return Results.NotFound("Task not found.");

    tasks.Remove(task);
    return Results.Ok(task);
});

app.Run();