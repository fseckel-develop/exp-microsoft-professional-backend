using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.Contracts.Orders;

public class UpdateOrderDto
{
    [Required]
    public required string CustomerName { get; set; }
}