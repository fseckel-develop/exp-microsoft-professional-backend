using JwtAuthenticationDemo.Infrastructure;
using JwtAuthenticationDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddJwtOptions(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
builder.Services.AddSingleton<IUserStorage, InMemoryUserStore>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();