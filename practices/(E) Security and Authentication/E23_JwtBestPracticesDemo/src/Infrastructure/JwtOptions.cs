namespace JwtBestPracticesDemo.Infrastructure;

public sealed class JwtOptions
{
    public const string SectionName = "JwtSettings";

    public string Issuer { get; set; } = "JwtBestPracticesDemo";
    public string Audience { get; set; } = "JwtBestPracticesDemo";
    public string Key { get; set; } = "SuperSecretKeyForJwtTokenAuthorization123456789";

    public int AccessTokenMinutes { get; set; } = 2;
    public int RefreshTokenMinutes { get; set; } = 60;
}