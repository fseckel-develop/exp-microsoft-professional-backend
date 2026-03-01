using JsonPackageDemo.Models;

namespace JsonPackageDemo.Presentation;

public sealed class ConsoleWriter
{
    public void WriteSection(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
    }

    public void WriteJson(string json)
    {
        Console.WriteLine(json);
        Console.WriteLine();
    }

    public void WriteBook(Book book)
    {
        Console.WriteLine($"Title:  {book.Title}");
        Console.WriteLine($"Author: {book.Author}");
        Console.WriteLine($"Pages:  {book.Pages}");
        Console.WriteLine($"Genres: {string.Join(", ", book.Genres)}");
        Console.WriteLine();
    }
}