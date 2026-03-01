using LogiTrack.Api.Contracts.Orders;

namespace LogiTrack.Api.Services.Orders;

public class OrderServiceResult
{
    public bool Success { get; init; }
    public OrderErrorCode ErrorCode { get; init; } = OrderErrorCode.None;
    public string? Error { get; init; }
    public OrderResponseDto? Order { get; init; }

    public static OrderServiceResult Successful(OrderResponseDto order) =>
        new()
        {
            Success = true,
            ErrorCode = OrderErrorCode.None,
            Order = order
        };

    public static OrderServiceResult Failure(OrderErrorCode errorCode, string error) =>
        new()
        {
            Success = false,
            ErrorCode = errorCode,
            Error = error
        };
}