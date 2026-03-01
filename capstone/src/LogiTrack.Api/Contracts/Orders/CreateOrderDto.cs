using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.Contracts.Orders;

public class CreateOrderDto
{
    [Required]
    public required string CustomerName { get; set; }

    public List<CreateOrderItemDto> OrderItems { get; set; } = new();
}