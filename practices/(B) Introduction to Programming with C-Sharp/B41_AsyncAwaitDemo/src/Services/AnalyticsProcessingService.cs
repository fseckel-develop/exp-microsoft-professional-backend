using AsyncAwaitDemo.Presentation;

namespace AsyncAwaitDemo.Services;

public sealed class AnalyticsProcessingService
{
    private readonly ConsoleWriter _writer;

    public AnalyticsProcessingService(ConsoleWriter writer)
    {
        _writer = writer;
    }

    public async Task ProcessChunkAsync(int chunkNumber)
    {
        _writer.WriteMessage($"Processing analytics chunk {chunkNumber}...");
        await Task.Delay(1000);
        _writer.WriteMessage($"Completed analytics chunk {chunkNumber}.");
    }

    public async Task ProcessLargeDatasetAsync(int numberOfChunks)
    {
        var tasks = new List<Task>();

        for (int i = 1; i <= numberOfChunks; i++)
        {
            tasks.Add(ProcessChunkAsync(i));
        }

        await Task.WhenAll(tasks);

        _writer.WriteMessage("All analytics chunks processed.");
        Console.WriteLine();
    }
}