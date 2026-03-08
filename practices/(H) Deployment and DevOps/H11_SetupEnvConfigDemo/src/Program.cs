using SetupEnvConfigDemo.Configuration;

// Load .env variables into the process environment for local development.
// In production, these values would typically come from real environment variables,
// secret managers, containers, or hosting platform configuration.
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI
builder.Services.AddOpenApi();

// Bind configuration section into options.
// ASP.NET Core configuration merges multiple sources automatically.
builder.Services.Configure<DemoSettings>(
    builder.Configuration.GetSection("DemoSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Show the effective configuration values after provider precedence is applied.
app.MapGet("/", (
    IConfiguration config,
    Microsoft.Extensions.Options.IOptions<DemoSettings> settings) =>
{
    return Results.Ok(new
    {
        environment = app.Environment.EnvironmentName,
        appName = settings.Value.AppName,
        supportEmail = settings.Value.SupportEmail,
        apiKeyConfigured = !string.IsNullOrWhiteSpace(settings.Value.ApiKey),
        messageSource = settings.Value.MessageSource,
        providerDemo = new
        {
            appSettingsValue = config["DemoSettings:MessageSource"],
            envVarExample = "DemoSettings__ApiKey"
        }
    });
});

// Protected endpoint using runtime secret configuration.
app.MapGet("/protected", (
    HttpContext context,
    Microsoft.Extensions.Options.IOptions<DemoSettings> settings) =>
{
    if (!context.Request.Headers.TryGetValue("X-Api-Key", out var providedKey))
        return Results.Unauthorized();

    if (providedKey != settings.Value.ApiKey)

        return Results.Unauthorized();

    return Results.Ok(new
    {
        message = "Access granted to protected endpoint.",
        secretSourcePattern = "This API key was resolved through the ASP.NET Core configuration pipeline."
    });
});

// Explain the configuration scenarios this demo represents.
app.MapGet("/config-scenarios", (Microsoft.Extensions.Options.IOptions<DemoSettings> settings) =>
{
    return Results.Ok(new
    {
        scenarios = new[]
        {
            "appsettings.json provides default configuration values.",
            "appsettings.Development.json overrides values for the Development environment.",
            ".env provides local development secrets by populating environment variables.",
            "Real operating system environment variables can override everything else at runtime."
        },
        note = "Container secrets, cloud secret managers, and hosting platform settings often surface values through the same configuration pipeline."
    });
});

app.Run();