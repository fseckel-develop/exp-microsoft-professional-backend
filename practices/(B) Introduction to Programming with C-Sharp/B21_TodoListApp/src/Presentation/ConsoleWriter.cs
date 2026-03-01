using ToDoListApp.Models;

namespace ToDoListApp.Presentation;

public sealed class ConsoleWriter
{
    public void WriteMenu()
    {
        Console.WriteLine("====== To-Do List ======");
        Console.WriteLine("  (1) Add new task");
        Console.WriteLine("  (2) View tasks");
        Console.WriteLine("  (3) Mark task as completed");
        Console.WriteLine("  (4) Exit");
        Console.Write("Choose an action: ");
    }

    public void WriteMessage(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine();
    }

    public void WriteTasks(IEnumerable<TaskItem> tasks)
    {
        Console.WriteLine($"{"ID",4} {"Status",-10} Title");
        Console.WriteLine(new string('-', 40));

        foreach (var task in tasks)
        {
            Console.WriteLine($"{task.Id,4} {task.Status,-10} {task.Title}");
        }

        Console.WriteLine();
    }
}