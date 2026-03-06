using System.ComponentModel.DataAnnotations;

namespace SafeVault.Web.Contracts;

public sealed class RegisterRequestDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^(User|Admin)$")]
    public string Role { get; set; } = "User";
}