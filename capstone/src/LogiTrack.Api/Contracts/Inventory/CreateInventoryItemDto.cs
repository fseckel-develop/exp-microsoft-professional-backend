using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.Contracts.Inventory;

public class CreateInventoryItemDto
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required int QuantityInStock { get; set; }

    [Required]
    public required string Location { get; set; }

    [Required]
    public required decimal Price { get; set; }
}