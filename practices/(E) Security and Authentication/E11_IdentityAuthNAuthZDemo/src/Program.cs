using IdentityAuthNAuthZDemo.Infrastructure;
using IdentityAuthNAuthZDemo.Models;

var builder = WebApplication.CreateBuilder(args);


// ------------------------------------
// MVC
// ------------------------------------
builder.Services.AddControllersWithViews();

// ------------------------------------
// Data + Identity + cookies
// ------------------------------------
builder.Services.AddIdentityStorage();
builder.Services.AddIdentityCoreServices();
builder.Services.UseApiFriendlyCookies();

// ------------------------------------
// Authorization policies
// ------------------------------------
builder.Services.AddAuthPolicies();

// ------------------------------------
// Seed default roles and admin user
// ------------------------------------
builder.Services.AddIdentityBootstrapper();

var app = builder.Build();

// ------------------------------------
// Middleware (ORDER MATTERS)
// ------------------------------------
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// ------------------------------------
// MVC routing
// ------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/api/whoami", (HttpContext ctx) =>
{
    var name = ctx.User?.Identity?.Name ?? "(none)";
    var authed = ctx.User?.Identity?.IsAuthenticated ?? false;
    return Results.Ok(new { authed, name });
});

app.MapControllers();

app.Run();