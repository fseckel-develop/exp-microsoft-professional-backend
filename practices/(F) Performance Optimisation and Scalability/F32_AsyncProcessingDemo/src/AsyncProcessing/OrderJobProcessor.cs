using AsyncProcessingDemo.Data;
using AsyncProcessingDemo.Models;

namespace AsyncProcessingDemo.AsyncProcessing;

public sealed class OrderJobProcessor
{
    private readonly OrderStorage _store;
    private readonly ILogger<OrderJobProcessor> _logger;

    public OrderJobProcessor(OrderStorage store, ILogger<OrderJobProcessor> logger)
    {
        _store = store;
        _logger = logger;
    }

    public async Task ProcessAsync(Guid orderId, CancellationToken token)
    {
        _logger.LogInformation("Queued order {OrderId}. Waiting before processing starts...", orderId);

        // Intentional delay so Pending state is observable
        await Task.Delay(TimeSpan.FromSeconds(5), token);

        _store.TryUpdate(orderId, order =>
        {
            order.Status = OrderStatus.Processing;
            order.ProcessingStartedAtUtc = DateTime.UtcNow;
        });

        _logger.LogInformation("Order {OrderId} moved to Processing", orderId);

        // Simulate payment step
        await Task.Delay(TimeSpan.FromSeconds(4), token);
        _logger.LogInformation("Payment approved for order {OrderId}", orderId);

        // Simulate fulfillment/packing step
        await Task.Delay(TimeSpan.FromSeconds(4), token);
        _logger.LogInformation("Fulfillment completed for order {OrderId}", orderId);

        _store.TryUpdate(orderId, order =>
        {
            order.Status = OrderStatus.Completed;
            order.CompletedAtUtc = DateTime.UtcNow;
        });

        _logger.LogInformation("Order {OrderId} moved to Completed", orderId);
    }
}