namespace JwtBestPracticesDemo.Contracts;

public sealed class RefreshRequestDto
{
    public string RefreshToken { get; set; } = string.Empty;
}