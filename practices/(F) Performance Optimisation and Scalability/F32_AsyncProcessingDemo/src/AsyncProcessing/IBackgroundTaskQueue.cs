namespace AsyncProcessingDemo.AsyncProcessing;

public interface IBackgroundTaskQueue
{
    ValueTask EnqueueAsync(Func<CancellationToken, Task> workItem, CancellationToken ct = default);
    ValueTask<Func<CancellationToken, Task>> DequeueAsync(CancellationToken ct);
}