namespace JwtCreationDecodingDemo.Models;

public sealed record JwtOptions(
    string Issuer,
    string Audience,
    string Secret,
    TimeSpan Lifetime
)
{
    public static JwtOptions ForDemo() => new JwtOptions(
        Issuer: "JwtCreationDecodingDemo",
        Audience: "JwtCreationDecodingDemo",
        Secret: "MySuperSecretKeyForThisDemoApp123456789",
        Lifetime: TimeSpan.FromMinutes(5)
    );
}