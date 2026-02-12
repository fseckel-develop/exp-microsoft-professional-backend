using E1_roles_claims_controlled.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------
// MVC
// ------------------------------------
builder.Services.AddControllersWithViews();

// ------------------------------------
// Database (In-Memory for demo)
// ------------------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("AuthDemoDatabase"));

// ------------------------------------
// Identity (Users + Roles)
// ------------------------------------
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ------------------------------------
// Cookie configuration (MVC + API friendly)
// ------------------------------------
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
    options.SlidingExpiration = true;

    // Return 401 instead of redirect for API requests
    options.Events.OnRedirectToLogin = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = 401; // Unauthorized
            return Task.CompletedTask;
        }
        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = 403; // Forbidden
            return Task.CompletedTask;
        }
        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});

// ------------------------------------
// Authorization policies
// ------------------------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly",
        policy => policy.RequireRole("Admin"));

    options.AddPolicy("ITDepartment",
        policy => policy.RequireClaim("Department", "IT"));
});

var app = builder.Build();

// ------------------------------------
// Seed default roles and admin user
// ------------------------------------
using (var scope = app.Services.CreateScope())
{
    await IdentitySeed.SeedAsync(scope.ServiceProvider);
}

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

// ------------------------------------
// Optional: API fallback routing for ProtectedController
// ------------------------------------
app.MapControllers(); // Needed for [ApiController] routes

app.Run();