using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtAuthenticationDemo.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtOptions(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<JwtOptions>()
            .Bind(config.GetSection(JwtOptions.SectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.Issuer), "JwtSettings:Issuer missing")
            .Validate(o => !string.IsNullOrWhiteSpace(o.Audience), "JwtSettings:Audience missing")
            .Validate(o => !string.IsNullOrWhiteSpace(o.Secret), "JwtSettings:Secret missing")
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        // Using IOptions<JwtOptions> inside the bearer config keeps it clean/testable
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = BuildTokenValidationParameters(config);
            });

        services.AddAuthorization();
        return services;
    }

    private static TokenValidationParameters BuildTokenValidationParameters(IConfiguration config)
    {
        var jwt = config.GetSection(JwtOptions.SectionName);
        var issuer = jwt["Issuer"]!;
        var audience = jwt["Audience"]!;
        var secret = jwt["Secret"]!;

        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = issuer,
            ValidAudience = audience,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),

            // Make expiry behavior deterministic in demos
            ClockSkew = TimeSpan.Zero
        };
    }
}