class Program
{
    static void Main(string[] args)
    {
        var tasks = new List<Task>
        {
            new Task("Task A", 1, 4, 70),
            new Task("Task B", 4, 7, 70),
            new Task("Task C", 1, 7, 135),
            new Task("Task D", 7, 9, 50),
            new Task("Task E", 9, 11, 50),
            new Task("Task F", 11, 13, 50),
            new Task("Task G", 13, 15, 50)
        };

        Console.WriteLine("Available Tasks:");
        foreach (var task in tasks)
        {
            Console.WriteLine($"    {task.Name} (Timespan [{task.Start}, {task.End}], Value = {task.Value})");
        }

        var scheduledTasks = DynamicTaskScheduler.GetSchedule(tasks);

        Console.WriteLine("\nOptimal Schedule:");
        foreach (var task in scheduledTasks)
        {
            Console.WriteLine($"    Timespan [{task.Start}, {task.End}]: {task.Name} (Value = {task.Value})");
        }
        Console.WriteLine($"    Total Value: {scheduledTasks.Sum(t => t.Value)}");
    }
}