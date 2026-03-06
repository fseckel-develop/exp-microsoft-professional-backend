using IdentityAuthNAuthZDemo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAuthNAuthZDemo.Controllers;

[Authorize(Policy = AccessRules.Policies.AdminArea)]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roles;
    private readonly UserManager<IdentityUser> _users;

    public RolesController(RoleManager<IdentityRole> roles, UserManager<IdentityUser> users)
    {
        _roles = roles;
        _users = users;
    }

    public IActionResult Index()
        => View(_roles.Roles.ToList());

    [HttpPost]
    public async Task<IActionResult> Create(string roleName)
    {
        roleName = (roleName ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(roleName))
            return RedirectToAction(nameof(Index));

        if (!await _roles.RoleExistsAsync(roleName))
            await _roles.CreateAsync(new IdentityRole(roleName));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Assign() => View();

    [HttpPost]
    public async Task<IActionResult> Assign(string email, string role)
    {
        email = (email ?? string.Empty).Trim();
        role = (role ?? string.Empty).Trim();

        var user = await _users.FindByEmailAsync(email);
        if (user is null) return RedirectToAction(nameof(Index));

        if (!await _roles.RoleExistsAsync(role))
            return RedirectToAction(nameof(Index));

        await _users.AddToRoleAsync(user, role);
        return RedirectToAction(nameof(Index));
    }
}