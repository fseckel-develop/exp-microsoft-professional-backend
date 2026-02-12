namespace E1_roles_claims_controlled.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RolesController( RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Assign(string email, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null && await _roleManager.RoleExistsAsync(role))
        {
            await _userManager.AddToRoleAsync(user, role);
        }
        return RedirectToAction(nameof(Index));
    }
}