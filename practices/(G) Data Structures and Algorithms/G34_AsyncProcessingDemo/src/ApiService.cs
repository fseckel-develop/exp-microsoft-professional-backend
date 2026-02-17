public class ApiService
{
    public async Task<string> FetchDataAsync(string requestName)
    {
        try
        {
            Console.WriteLine($"{requestName} is processing...");
            await Task.Delay(2000);

            // Simulate occasional failure
            if (requestName.Contains("2"))
                throw new Exception("Simulated API failure.");

            return $"{requestName} completed.";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in {requestName}: {ex.Message}");
            throw; // Optional: rethrow if caller should handle it
        }
    }

    public async Task HandleMultipleRequestsAsync()
    {
        try
        {
            var task1 = FetchDataAsync("Request 1");
            var task2 = FetchDataAsync("Request 2");
            var task3 = FetchDataAsync("Request 3");

            var results = await Task.WhenAll(task1, task2, task3);

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }

            Console.WriteLine("Finished all API requests.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"One or more API requests failed: {ex.Message}");
        }
    }

    public async Task ProcessBackgroundTasksAsync()
    {
        var tasks = new List<Task>();

        for (int taskId = 1; taskId <= 5; taskId++)
        {
            tasks.Add(ExecuteTaskAsync(taskId));
        }

        try
        {
            await Task.WhenAll(tasks);
            Console.WriteLine("All background tasks processed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Background processing error: {ex.Message}");
        }
    }

    private async Task ExecuteTaskAsync(int taskId)
    {
        try
        {
            Console.WriteLine($"Task {taskId} started...");
            await Task.Delay(1000 * taskId);

            // Simulate occasional failure
            if (taskId == 3)
                throw new Exception("Simulated task failure.");

            Console.WriteLine($"Task {taskId} completed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in Task {taskId}: {ex.Message}");
            throw;
        }
    }
}