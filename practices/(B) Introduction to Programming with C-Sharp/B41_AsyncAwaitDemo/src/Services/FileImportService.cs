using AsyncAwaitDemo.Presentation;

namespace AsyncAwaitDemo.Services;

public sealed class FileImportService
{
    private readonly ConsoleWriter _writer;

    public FileImportService(ConsoleWriter writer)
    {
        _writer = writer;
    }

    public async Task<string> ImportFileAsync(string fileName)
    {
        _writer.WriteMessage($"Starting import of {fileName}...");
        await Task.Delay(3000);
        _writer.WriteMessage($"Completed import of {fileName}.");

        return $"{fileName} content";
    }

    public async Task ImportFilesAsync()
    {
        var importTask1 = ImportFileAsync("products.csv");
        var importTask2 = ImportFileAsync("reviews.csv");

        await Task.WhenAll(importTask1, importTask2);

        _writer.WriteMessage("All file imports completed.");
        Console.WriteLine();
    }
}