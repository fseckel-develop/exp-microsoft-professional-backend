var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Let's trigger an CI/CD Pipeline Alert!");

// Uncomment the following line to simulate a failure
TriggerFailure();

app.Run();
