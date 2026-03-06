using Microsoft.AspNetCore.Identity;

namespace IdentityAuthNAuthZDemo.Infrastructure;

public class IdentityBootstrapHostedService : IHostedService
{
    private readonly IServiceProvider _services;

    public IdentityBootstrapHostedService(IServiceProvider services)
    {
        _services = services;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _services.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // Ensure Admin role
        if (!await roleManager.RoleExistsAsync(AccessRules.Roles.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole(AccessRules.Roles.Admin));
        }

        // Ensure admin user
        var email = AccessRules.DemoUsers.AdminEmail;
        var admin = await userManager.FindByEmailAsync(email);

        if (admin is null)
        {
            admin = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var create = await userManager.CreateAsync(admin, AccessRules.DemoUsers.AdminPassword);
            if (!create.Succeeded)
                throw new InvalidOperationException(string.Join("; ", create.Errors.Select(e => e.Description)));
        }

        // Put admin in role
        if (!await userManager.IsInRoleAsync(admin, AccessRules.Roles.Admin))
        {
            await userManager.AddToRoleAsync(admin, AccessRules.Roles.Admin);
        }

        // (Optional) Give admin IT claim so you can test claim policy quickly
        var claims = await userManager.GetClaimsAsync(admin);
        if (!claims.Any(c => c.Type == AccessRules.Claims.Department && c.Value == AccessRules.Claims.IT))
        {
            await userManager.AddClaimAsync(admin,
                new System.Security.Claims.Claim(AccessRules.Claims.Department, AccessRules.Claims.IT));
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}