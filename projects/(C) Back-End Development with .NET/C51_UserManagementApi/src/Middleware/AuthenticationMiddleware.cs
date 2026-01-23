using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

// ----------------------
// AuthenticationMiddleware
// Lightweight example auth used by the exercise: expects `Authorization: Bearer my-secret-token`.
// Keeps auth logic isolated and easy to replace with real authentication later.
// ----------------------
public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private const string ValidToken = "my-secret-token";

    public AuthenticationMiddleware(RequestDelegate next) => _next = next;

    // InvokeAsync - returns 401 JSON when the Authorization header is missing or invalid
    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var token) || token != $"Bearer {ValidToken}")
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { error = "Unauthorized" });
            return;
        }

        await _next(context);
    }
}
