using IdentityAuthNAuthZDemo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityAuthNAuthZDemo.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityStorage(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("AuthDemoDatabase"));

        return services;
    }

    public static IServiceCollection AddIdentityCoreServices(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection UseApiFriendlyCookies(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/Login";
            options.SlidingExpiration = true;

            options.Events.OnRedirectToLogin = context =>
            {
                if (context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                }
                context.Response.Redirect(context.RedirectUri);
                return Task.CompletedTask;
            };

            options.Events.OnRedirectToAccessDenied = context =>
            {
                if (context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                }
                context.Response.Redirect(context.RedirectUri);
                return Task.CompletedTask;
            };
        });

        return services;
    }

    public static IServiceCollection AddAuthPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // Admin policy (role-based)
            options.AddPolicy(AccessRules.Policies.AdminArea, policy =>
                policy.RequireRole(AccessRules.Roles.Admin));

            // Claim-based policy
            options.AddPolicy(AccessRules.Policies.ITOnly, policy =>
                policy.RequireClaim(AccessRules.Claims.Department, AccessRules.Claims.IT));
        });

        return services;
    }

    public static IServiceCollection AddIdentityBootstrapper(this IServiceCollection services)
    {
        // Runs once on startup (dev-friendly)
        services.AddHostedService<IdentityBootstrapHostedService>();
        return services;
    }
}