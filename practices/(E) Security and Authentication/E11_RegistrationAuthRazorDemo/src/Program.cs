
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();


// Add in-memory database for Identity Details
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseInMemoryDatabase("AuthDemoDatabase"));


// Add Identity
// - Register the RoleManager<IdentityRole> service in your project
// - Table for Users and Roles will be created in the in-memory database
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api") &&
            context.Response.StatusCode == 200)
        {
            context.Response.StatusCode = 401; // Unauthorized
            return Task.CompletedTask;
        }
        context.Response.Redirect(context.RedirectUri);
        context.Response.StatusCode = 401; // Unauthorized
        return Task.CompletedTask;
    };
});


builder.Services.AddAuthentication();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"))
    .AddPolicy("ITDepartment", policy => 
        policy.RequireClaim("Department", "IT"));



var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapGet("/api/admin-only", () => "Admin Access only!")
    .RequireAuthorization("AdminOnly");

app.MapGet("/api/user-claim-check", () => "Access granted to IT department.")
    .RequireAuthorization("ITDepartment");

app.MapGet("/blogs", () => "This is the blogs route (public).");


// Default login route when unauthorized
app.MapGet("/account/login", () => "This is the login route (default when unauthorized).");


// Creating roles endpoints
var roles = new[] { 
    "Admin", 
    "User" 
};
app.MapPost("/api/create-role", async (RoleManager<IdentityRole> roleManager) =>
{
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
    return Results.Ok("Roles created successfully.");
});


// Assign role to user endpoint
app.MapPost("/api/assign-role", async (UserManager<IdentityUser> userManager) =>
{
    var user = new IdentityUser
    {
        UserName = "testuser",
        Email = "testuser@example.com"
    };
    await userManager.CreateAsync(user, "Password@123");
    await userManager.AddToRoleAsync(user, "Admin");
    var isInRole = await userManager.IsInRoleAsync(user, "Admin");
    return isInRole 
        ? Results.Ok("Admin role assigned to test user successfully.")
        : Results.BadRequest("Failed to assign Admin role to test user.");
});


// Login endpoint
app.MapPost("/api/login", async (UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) =>
{
    var user = await userManager.FindByNameAsync("testuser");
    if (user == null)
    {
       return Results.NotFound("User not found.");
    }
    await signInManager.SignInAsync(user, isPersistent: false);
    return Results.Ok("Test user logged in successfully.");
});


// Login endpoint
app.MapPost("/api/add-claim", async (UserManager<IdentityUser> userManager) =>
{
    var user = await userManager.FindByNameAsync("testuser");
    if (user == null)
    {
       return Results.NotFound("User not found.");
    }
    await userManager.AddClaimAsync(user, new Claim("Department", "IT"));
    var hasITClaim = (await userManager.GetClaimsAsync(user))
        .Any(c => c.Type == "Department" && c.Value == "IT");
    return hasITClaim 
        ? Results.Ok("IT Department claim added to test user successfully.")
        : Results.BadRequest("Failed to add IT Department claim to test user.");
});


app.Run();
