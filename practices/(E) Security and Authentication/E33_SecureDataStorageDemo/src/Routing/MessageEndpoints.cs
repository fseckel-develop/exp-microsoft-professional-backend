using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using SecureDataStorageDemo.Data;
using SecureDataStorageDemo.Contracts;
using SecureDataStorageDemo.Services;
using SecureDataStorageDemo.Models;

namespace SecureDataStorageDemo.Routing;

public static class MessageEndpoints
{
    public static IEndpointRouteBuilder MapMessageEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/messages").RequireAuthorization();

        // List message metadata (don’t leak plaintext)
        group.MapGet("/", async (AppDbContext db) =>
        {
            var items = await db.Messages
                .OrderByDescending(m => m.Id)
                .Select(m => new MessageListItemDto
                {
                    Id = m.Id,
                    UserId = m.UserId,
                    CreatedAt = m.CreatedAt
                })
                .ToListAsync();

            return Results.Ok(items);
        });

        // Get decrypted message (owner-only)
        group.MapGet("/{id:int}", async (int id, ClaimsPrincipal user, AppDbContext db, IMessageCryptoService crypto) =>
        {
            var msg = await db.Messages.FindAsync(id);
            if (msg is null) return Results.NotFound("Message does not exist.");

            var userId = GetUserId(user);
            if (!string.Equals(msg.UserId, userId, StringComparison.OrdinalIgnoreCase))
                return Results.Forbid();

            try
            {
                var text = crypto.DecryptFromBase64(msg.CipherText);
                return Results.Ok(new { id = msg.Id, text, msg.CreatedAt });
            }
            catch
            {
                return Results.Problem("Failed to decrypt message.");
            }
        });

        // Create message (uses JWT user)
        group.MapPost("/", async (CreateMessageRequestDto dto, ClaimsPrincipal user, AppDbContext db, IMessageCryptoService crypto) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Text))
                return Results.BadRequest(new { error = "Text is required." });

            var userId = GetUserId(user);
            if (string.IsNullOrWhiteSpace(userId))
                return Results.Unauthorized();

            var msg = new Message
            {
                UserId = userId,
                CipherText = crypto.EncryptToBase64(dto.Text)
            };

            db.Messages.Add(msg);
            await db.SaveChangesAsync();

            return Results.Created($"/api/messages/{msg.Id}", new { id = msg.Id });
        });

        return app;
    }

    private static string GetUserId(ClaimsPrincipal user)
        => user.FindFirstValue(ClaimTypes.NameIdentifier)
           ?? user.FindFirstValue(JwtRegisteredClaimNames.Sub)
           ?? user.Identity?.Name
           ?? string.Empty;
}