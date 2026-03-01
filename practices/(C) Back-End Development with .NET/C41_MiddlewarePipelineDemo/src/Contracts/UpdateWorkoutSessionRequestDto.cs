namespace MiddlewarePipelineDemo.Contracts;

public sealed class UpdateWorkoutSessionRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}