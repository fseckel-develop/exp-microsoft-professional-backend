using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.DTOs;

public class UpdateOrderDto
{
    [Required]
    public required string CustomerName { get; set; } = string.Empty;

    public List<UpdateOrderItemDto> OrderItems { get; set; } = new();
}