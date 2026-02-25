using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.DTOs;

public class UpdateOrderItemDto
{
    public int? OrderItemId { get; set; }  // null = new item

    [Required]
    public required int InventoryItemId { get; set; }

    [Required]
    public required int QuantityOrdered { get; set; }

    [Required]
    public required decimal UnitPrice { get; set; }
}