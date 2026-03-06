using JwtBestPracticesDemo.Infrastructure;
using JwtBestPracticesDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Config + auth wiring
builder.Services.AddJwtOptions(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

// App services
builder.Services.AddSingleton<IUserDirectory, InMemoryUserDirectory>();
builder.Services.AddSingleton<IJwtIssuer, JwtIssuer>();
builder.Services.AddSingleton<IRefreshTokenStore, InMemoryRefreshTokenStore>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();