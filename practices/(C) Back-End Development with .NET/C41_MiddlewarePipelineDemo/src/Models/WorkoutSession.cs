namespace MiddlewarePipelineDemo.Models;

public sealed class WorkoutSession
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}