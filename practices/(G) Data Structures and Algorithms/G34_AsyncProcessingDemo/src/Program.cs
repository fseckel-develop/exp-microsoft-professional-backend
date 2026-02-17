class Program
{
    static async Task Main()
    {
        var service = new ApiService();

        Console.WriteLine("Starting API request...");
        var result = await service.FetchDataAsync("Single Request");
        Console.WriteLine(result);
        Console.WriteLine();

        Console.WriteLine("Starting multiple API requests...");
        await service.HandleMultipleRequestsAsync();
        Console.WriteLine();

        Console.WriteLine("Starting background task ...");
        await service.ProcessBackgroundTasksAsync();
        Console.WriteLine("All background tasks completed.");
    }
}