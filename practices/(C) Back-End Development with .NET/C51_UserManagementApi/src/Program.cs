using UserManagementApi.Middleware;
using UserManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Service registration
builder.Services.AddLogging();
builder.Services.AddSingleton<UserService>();
builder.Services.AddControllers();

var app = builder.Build();

// Middleware pipeline
app.UseErrorHandling();
app.UseAuthenticationMiddleware();
app.UseRequestLogging();

// Routing
app.MapControllers();

app.Run();