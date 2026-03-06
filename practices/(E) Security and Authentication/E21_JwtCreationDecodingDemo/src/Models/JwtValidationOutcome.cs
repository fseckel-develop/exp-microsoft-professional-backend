using System.Security.Claims;

namespace JwtCreationDecodingDemo.Models;

public sealed record JwtValidationResult(
    bool IsValid,
    ClaimsPrincipal? Principal,
    string? Error
)
{
    public static JwtValidationResult Success(ClaimsPrincipal principal)
        => new(true, principal, null);

    public static JwtValidationResult Fail(string error)
        => new(false, null, error);
}