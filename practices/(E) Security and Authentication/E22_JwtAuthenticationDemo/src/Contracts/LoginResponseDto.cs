namespace JwtAuthenticationDemo.Models;

public sealed class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string TokenType { get; set; } = "Bearer";
}