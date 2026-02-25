using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.DTOs;

public class UpdateInventoryItemDto
{
    [Required]
    public required string Name { get; set; } = string.Empty;

    [Required]
    public required int QuantityInStock { get; set; }

    [Required]
    public required string Location { get; set; } = string.Empty;

    [Required]
    public required decimal Price { get; set; }
}