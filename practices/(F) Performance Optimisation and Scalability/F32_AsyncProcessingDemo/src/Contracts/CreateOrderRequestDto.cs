namespace AsyncProcessingDemo.Contracts;

public sealed record CreateOrderRequestDto(
    string CustomerName,
    string ProductName,
    int Quantity
);