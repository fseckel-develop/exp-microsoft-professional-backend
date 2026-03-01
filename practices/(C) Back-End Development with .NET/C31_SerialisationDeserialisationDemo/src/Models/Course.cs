namespace SerializationDeserializationDemo.Models;

public sealed class Course
{
    public required string Title { get; set; }
    public string? Instructor { get; set; }
    public int DurationHours { get; set; }

    public string ToDisplayString()
    {
        return $"Title: {Title}, Instructor: {Instructor}, DurationHours: {DurationHours}";
    }
}