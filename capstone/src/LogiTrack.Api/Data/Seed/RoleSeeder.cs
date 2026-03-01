using LogiTrack.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace LogiTrack.Api.Data.Seed;

public static class RoleSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = ["Admin", "WarehouseStaff", "SalesStaff"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        await EnsureDemoUserAsync(
            userManager,
            email: "admin@example.com",
            password: "Admin123!",
            role: "Admin");

        await EnsureDemoUserAsync(
            userManager,
            email: "warehouse@example.com",
            password: "Warehouse123!",
            role: "WarehouseStaff");

        await EnsureDemoUserAsync(
            userManager,
            email: "sales@example.com",
            password: "Sales123!",
            role: "SalesStaff");
    }

    private static async Task EnsureDemoUserAsync(
        UserManager<ApplicationUser> userManager,
        string email,
        string password,
        string role)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null)
            return;

        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return;

        await userManager.AddToRoleAsync(user, role);
    }
}