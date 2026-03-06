using Microsoft.Extensions.Hosting;
using AsyncProcessingDemo.Data;
using AsyncProcessingDemo.Models;

namespace AsyncProcessingDemo.AsyncProcessing;

public sealed class QueuedOrderProcessor : BackgroundService
{
    private readonly IBackgroundTaskQueue _queue;
    private readonly ILogger<QueuedOrderProcessor> _logger;

    public QueuedOrderProcessor(
        IBackgroundTaskQueue queue,
        ILogger<QueuedOrderProcessor> logger)
    {
        _queue = queue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Queued order processor started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var workItem = await _queue.DequeueAsync(stoppingToken);
                await workItem(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Queued order processor is stopping.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing a queued job.");
            }
        }

        _logger.LogInformation("Queued order processor stopped.");
    }
}