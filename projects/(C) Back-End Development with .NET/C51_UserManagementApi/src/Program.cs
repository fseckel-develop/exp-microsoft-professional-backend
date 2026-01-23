/*
Microsoft Copilot helps speed up development by generating boilerplate code for the ASP.NET Core minimal API setup.
This project demonstrates a small user management API used for learning:
- Middleware (error handling, auth, logging)
- A thread-safe in-memory `UserService`
- `UsersController` exposing CRUD endpoints

Keep changes consistent with the project's lightweight teaching goals (no external infra).
*/

var builder = WebApplication.CreateBuilder(args);
// ----------------------
// Service registration
// Register logging, the singleton in-memory UserService and MVC controllers.
// ----------------------
builder.Services.AddLogging();
builder.Services.AddSingleton<UserService>();
builder.Services.AddControllers();
var app = builder.Build();

// ----------------------
// Middleware pipeline
// Order: global error handling -> authentication -> request logging
// Error handling should run first (closest to the request entry) so it can catch
// exceptions from later middleware or controllers.
// ----------------------
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

// ----------------------
// Routing
// Controllers handle route mappings (UsersController) and are mapped here.
// ----------------------
app.MapControllers();

app.Run();
