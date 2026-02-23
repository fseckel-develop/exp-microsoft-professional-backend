var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Let´s generate a CI/CD Pipeline with Copilot!");

app.Run();
