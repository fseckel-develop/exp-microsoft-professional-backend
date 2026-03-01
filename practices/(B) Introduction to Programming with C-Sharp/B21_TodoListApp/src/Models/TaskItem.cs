namespace ToDoListApp.Models;

public sealed class TaskItem
{
    public int Id { get; }
    public string Title { get; private set; }
    public TaskStatus Status { get; private set; }

    public TaskItem(int id, string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title is required.", nameof(title));

        Id = id;
        Title = title.Trim();
        Status = TaskStatus.Pending;
    }

    public void MarkCompleted()
    {
        Status = TaskStatus.Completed;
    }
}