namespace SecureDataStorageDemo.Infrastructure;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;

    // Base64 encoded symmetric key
    public string SigningKeyBase64 { get; set; } = string.Empty;

    // dev-token lifetime
    public int DevTokenMinutes { get; set; } = 60;
}