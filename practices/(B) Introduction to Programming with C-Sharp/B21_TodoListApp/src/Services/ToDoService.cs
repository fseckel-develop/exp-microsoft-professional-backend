using ToDoListApp.Models;

namespace ToDoListApp.Services;

public sealed class ToDoService
{
    private readonly List<TaskItem> _tasks = [];
    private int _nextId = 1;
    private readonly int _maxTasks;

    public ToDoService(int maxTasks = 10)
    {
        if (maxTasks <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxTasks), "Maximum task count must be greater than zero.");

        _maxTasks = maxTasks;
    }

    public bool IsFull => _tasks.Count >= _maxTasks;
    public int Count => _tasks.Count;

    public TaskItem AddTask(string title)
    {
        if (IsFull)
            throw new InvalidOperationException("Maximum number of tasks reached.");

        var task = new TaskItem(_nextId++, title);
        _tasks.Add(task);
        return task;
    }

    public IReadOnlyList<TaskItem> GetAllTasks()
    {
        return _tasks
            .OrderBy(t => t.Id)
            .ToList();
    }

    public TaskItem? FindById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public bool CompleteTask(int id)
    {
        var task = FindById(id);
        if (task is null)
            return false;

        task.MarkCompleted();
        return true;
    }
}