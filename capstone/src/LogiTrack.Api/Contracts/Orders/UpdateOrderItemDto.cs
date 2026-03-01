using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.Contracts.Orders;

public class UpdateOrderItemDto
{
    [Required]
    public required int InventoryItemId { get; set; }

    [Required]
    public required int QuantityOrdered { get; set; }
}