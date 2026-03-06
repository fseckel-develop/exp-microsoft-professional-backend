namespace AsyncProcessingDemo.Models;

public sealed class Order
{
    public Guid Id { get; init; }
    public required string CustomerName { get; init; }
    public required string ProductName { get; init; }
    public int Quantity { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public OrderStatus Status { get; set; }
    public DateTime? ProcessingStartedAtUtc { get; set; }
    public DateTime? CompletedAtUtc { get; set; }
}