using IdentityAuthNAuthZDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAuthNAuthZDemo.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> users, SignInManager<IdentityUser> signIn)
    {
        _userManager = users;
        _signInManager = signIn;
    }

    // -----------------------
    // Helpers
    // -----------------------
    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }

    private void AddIdentityErrors(IdentityResult result)
    {
        foreach (var err in result.Errors)
            ModelState.AddModelError(string.Empty, err.Description);
    }

    private void AddLoginError()
        => ModelState.AddModelError(string.Empty, "Login failed. Please check your email and password.");

    // -----------------------
    // Register
    // -----------------------
    [HttpGet]
    public IActionResult Register(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new RegisterViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
            return View(model);

        var email = model.Email.Trim();

        var user = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        var create = await _userManager.CreateAsync(user, model.Password);

        if (!create.Succeeded)
        {
            AddIdentityErrors(create);
            return View(model);
        }

        // Keep your existing behavior: auto-login after register
        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToLocal(returnUrl);
    }

    // -----------------------
    // Login
    // -----------------------
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
            return View(model);

        var email = model.Email.Trim();

        // Because you set UserName = Email at registration, username==email is correct here.
        var signIn = await _signInManager.PasswordSignInAsync(
            userName: email,
            password: model.Password,
            isPersistent: model.RememberMe,
            lockoutOnFailure: false);

        if (signIn.Succeeded)
            return RedirectToLocal(returnUrl);

        AddLoginError();
        return View(model);
    }

    // -----------------------
    // Logout
    // -----------------------
    [HttpPost]
    // [ValidateAntiForgeryToken] // optional; keep for browser, but breaks raw .http calls
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }
}