using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.DTOs;

public class CreateOrderDto
{
    [Required]
    public required string CustomerName { get; set; } = string.Empty;

    public List<CreateOrderItemDto> OrderItems { get; set; } = new();
}