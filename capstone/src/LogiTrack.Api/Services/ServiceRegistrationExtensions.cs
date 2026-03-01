using System.Text;
using LogiTrack.Api.Data;
using LogiTrack.Api.Models;
using LogiTrack.Api.Services.Auth;
using LogiTrack.Api.Services.Email;
using LogiTrack.Api.Services.Inventory;
using LogiTrack.Api.Services.Orders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace LogiTrack.Api.Services;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddLogiTrackDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LogiTrackDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

    public static IServiceCollection AddLogiTrackCaching(this IServiceCollection services)
    {
        services.AddMemoryCache();
        return services;
    }

    public static IServiceCollection AddLogiTrackIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<LogiTrackDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        });

        return services;
    }

    public static IServiceCollection AddLogiTrackAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtKey = configuration["Jwt:Key"]
                    ?? throw new InvalidOperationException("JWT Key is not configured.");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userManager = context.HttpContext.RequestServices
                            .GetRequiredService<UserManager<ApplicationUser>>();

                        var userId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
                        var tokenSecurityStamp = context.Principal?.FindFirst("security_stamp")?.Value;

                        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(tokenSecurityStamp))
                        {
                            context.Fail("Invalid token claims.");
                            return;
                        }

                        var user = await userManager.FindByIdAsync(userId);
                        if (user is null)
                        {
                            context.Fail("User no longer exists.");
                            return;
                        }

                        var currentSecurityStamp = await userManager.GetSecurityStampAsync(user);

                        if (!string.Equals(tokenSecurityStamp, currentSecurityStamp, StringComparison.Ordinal))
                        {
                            context.Fail("Token has been revoked.");
                        }
                    }
                };
            });

        return services;
    }

    public static IServiceCollection AddLogiTrackAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("InventoryWrite",
                policy => policy.RequireRole("Admin", "WarehouseStaff"));

            options.AddPolicy("OrderWrite",
                policy => policy.RequireRole("Admin", "SalesStaff"));

            options.AddPolicy("AdminOnly",
                policy => policy.RequireRole("Admin"));
        });

        return services;
    }

    public static IServiceCollection AddLogiTrackApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IInventoryService, InventoryService>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddTransient<IEmailSender, MockEmailSender>();
        // services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }

    public static IServiceCollection AddLogiTrackSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "LogiTrack API",
                Version = "v1",
                Description = "A logistics and inventory management API with JWT authentication, role-based authorization, inventory management, and order processing."
            });
            
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter: Bearer {your JWT token}",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                [securityScheme] = Array.Empty<string>()
            });
        });

        return services;
    }
}