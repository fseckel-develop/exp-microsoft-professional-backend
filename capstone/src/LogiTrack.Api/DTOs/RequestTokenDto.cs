using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.DTOs;

public class RequestTokenDto
{
    [Required]
    public required string UserId { get; set; }

    [Required]
    public required string RefreshToken { get; set; }
}