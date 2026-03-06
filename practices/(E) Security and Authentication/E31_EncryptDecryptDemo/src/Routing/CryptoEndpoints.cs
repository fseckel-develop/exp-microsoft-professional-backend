using EncryptDecryptDemo.Services;
using EncryptDecryptDemo.Models;
using System.Security.Cryptography;

namespace EncryptDecryptDemo.Routing;

public static class CryptoEndpoints
{
    public static IEndpointRouteBuilder MapCryptoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/crypto");

        group.MapPost("/encrypt", (EncryptRequestDto dto, IEncryptionService crypto) =>
        {
            if (string.IsNullOrWhiteSpace(dto.PlainText))
                return Results.BadRequest(new { error = "PlainText is required." });

            var encrypted = crypto.EncryptToBase64(dto.PlainText);
            return Results.Ok(new EncryptResponseDto(encrypted));
        });

        group.MapPost("/decrypt", (DecryptRequestDto dto, IEncryptionService crypto) =>
        {
            try
            {
                var decrypted = crypto.DecryptFromBase64(dto.CipherText);
                return Results.Ok(new DecryptResponseDto(decrypted));
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
            catch (CryptographicException)
            {
                // Wrong key/IV or tampered ciphertext
                return Results.BadRequest(new { error = "Decryption failed. Invalid ciphertext or key." });
            }
        });

        return app;
    }
}