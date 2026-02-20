var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();


// Load environment variables from .env file
DotNetEnv.Env.Load(); 
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var apiKey = Environment.GetEnvironmentVariable("API_KEY");

// Output the loaded environment variables to verify they are set correctly
Console.WriteLine("Environment Variables have been loaded:");
Console.WriteLine($"   Environment: {environment}");
Console.WriteLine($"   API Key:     {apiKey}");


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();