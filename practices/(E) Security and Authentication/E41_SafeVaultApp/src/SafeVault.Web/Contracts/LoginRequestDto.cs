using System.ComponentModel.DataAnnotations;

namespace SafeVault.Web.Contracts;

public sealed class LoginRequestDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}