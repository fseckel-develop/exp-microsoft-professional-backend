using System.Text.Json;
using JsonPackageDemo.Models;

namespace JsonPackageDemo.Services;

public sealed class BookJsonService
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public string Serialize(Book book)
    {
        return JsonSerializer.Serialize(book, _options);
    }

    public Book Deserialize(string json)
    {
        var book = JsonSerializer.Deserialize<Book>(json, _options);

        if (book is null)
            throw new InvalidOperationException("Failed to deserialize book JSON.");

        return book;
    }
}