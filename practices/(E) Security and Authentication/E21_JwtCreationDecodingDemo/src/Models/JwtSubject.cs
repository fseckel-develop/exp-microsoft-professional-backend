namespace JwtCreationDecodingDemo.Models;

public sealed record JwtSubject(
    string UserId,
    IReadOnlyList<string> Roles
);
