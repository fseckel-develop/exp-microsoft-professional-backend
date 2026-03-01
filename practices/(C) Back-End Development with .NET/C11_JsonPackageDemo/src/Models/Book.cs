namespace JsonPackageDemo.Models;

public sealed class Book
{
    public string Title { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public int Pages { get; init; }
    public List<string> Genres { get; init; } = [];
}