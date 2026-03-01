using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.Contracts.Orders;

public class CreateOrderItemDto
{
    [Required]
    public required int InventoryItemId { get; set; }

    [Required]
    public required int QuantityOrdered { get; set; }
}