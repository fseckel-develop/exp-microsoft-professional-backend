using AsyncAwaitDemo.Presentation;

namespace AsyncAwaitDemo.Services;

public sealed class BackgroundJobService
{
    private readonly ConsoleWriter _writer;

    public BackgroundJobService(ConsoleWriter writer)
    {
        _writer = writer;
    }

    public async Task RunFailingJobAsync()
    {
        try
        {
            _writer.WriteMessage("Background job started...");
            await Task.Delay(3000);

            throw new InvalidOperationException("Simulated processing failure.");
        }
        catch (Exception ex)
        {
            _writer.WriteMessage($"An error occurred: {ex.Message}");
        }

        Console.WriteLine();
    }
}