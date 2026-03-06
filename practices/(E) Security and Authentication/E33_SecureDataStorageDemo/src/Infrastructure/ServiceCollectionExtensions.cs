using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SecureDataStorageDemo.Data;
using SecureDataStorageDemo.Services;

namespace SecureDataStorageDemo.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppData(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseInMemoryDatabase("SecureDataDb"));

        return services;
    }

    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<JwtOptions>()
            .Bind(config.GetSection(JwtOptions.SectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.Issuer), "Jwt:Issuer missing")
            .Validate(o => !string.IsNullOrWhiteSpace(o.Audience), "Jwt:Audience missing")
            .Validate(o => !string.IsNullOrWhiteSpace(o.SigningKeyBase64), "Jwt:SigningKeyBase64 missing")
            .ValidateOnStart();

        var jwt = config.GetSection(JwtOptions.SectionName);
        var keyBytes = Convert.FromBase64String(jwt["SigningKeyBase64"]!);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwt["Issuer"],
                    ValidAudience = jwt["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

                    ClockSkew = TimeSpan.Zero
                };

                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        var raw = ctx.Request.Headers.Authorization.ToString();
                        Console.WriteLine("AUTH HEADER RAW: [" + raw + "]");
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = ctx =>
                    {
                        Console.WriteLine("JWT failed: " + ctx.Exception.GetType().Name + " - " + ctx.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();
        return services;
    }

    public static IServiceCollection AddMessageCrypto(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<EncryptionOptions>()
            .Bind(config.GetSection(EncryptionOptions.SectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.KeyBase64), "Encryption:KeyBase64 missing")
            .ValidateOnStart();

        services.AddSingleton<IMessageCryptoService, AesMessageCryptoService>();
        return services;
    }
}