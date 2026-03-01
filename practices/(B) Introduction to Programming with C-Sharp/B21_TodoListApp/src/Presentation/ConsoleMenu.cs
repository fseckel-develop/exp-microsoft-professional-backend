using ToDoListApp.Services;

namespace ToDoListApp.Presentation;

public sealed class ConsoleMenu
{
    private readonly ToDoService _service;
    private readonly ConsoleWriter _writer;

    public ConsoleMenu(ToDoService service, ConsoleWriter writer)
    {
        _service = service;
        _writer = writer;
    }

    public void Run()
    {
        bool running = true;

        while (running)
        {
            _writer.WriteMenu();

            string choice = (Console.ReadLine() ?? string.Empty).Trim();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    CompleteTask();
                    break;
                case "4":
                    running = false;
                    _writer.WriteMessage("Exiting application. Goodbye!");
                    break;
                default:
                    _writer.WriteMessage("Invalid input.");
                    break;
            }
        }
    }

    private void AddTask()
    {
        Console.Write("Enter the task to add: ");
        string title = Console.ReadLine() ?? string.Empty;

        try
        {
            var task = _service.AddTask(title);
            _writer.WriteMessage($"Task added successfully (ID: {task.Id}).");
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            _writer.WriteMessage(ex.Message);
        }
    }

    private void ViewTasks()
    {
        var tasks = _service.GetAllTasks();

        if (tasks.Count == 0)
        {
            _writer.WriteMessage("No tasks registered yet.");
            return;
        }

        _writer.WriteTasks(tasks);
    }

    private void CompleteTask()
    {
        Console.Write("Which task ID did you complete? ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            _writer.WriteMessage("Invalid input.");
            return;
        }

        bool completed = _service.CompleteTask(id);

        if (!completed)
        {
            _writer.WriteMessage("Task not found.");
            return;
        }

        _writer.WriteMessage("Task marked as completed.");
    }
}