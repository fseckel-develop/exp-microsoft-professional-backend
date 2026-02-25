namespace LogiTrack.Api.DTOs;

public class ResponseOrderDto
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime DatePlaced { get; set; }
    public decimal TotalAmount { get; set; }
    public List<ResponseOrderItemDto> OrderItems { get; set; } = new();
}