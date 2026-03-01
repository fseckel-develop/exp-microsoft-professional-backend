using JsonPackageDemo.Models;
using JsonPackageDemo.Presentation;
using JsonPackageDemo.Services;

namespace JsonPackageDemo;

internal static class Program
{
    private static void Main(string[] args)
    {
        var writer = new ConsoleWriter();
        var jsonService = new BookJsonService();

        var book = new Book
        {
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
            Pages = 180,
            Genres = ["Classic", "Novel"]
        };

        writer.WriteSection("Serialized JSON");
        string jsonOutput = jsonService.Serialize(book);
        writer.WriteJson(jsonOutput);

        string jsonInput =
            """{"Title":"1984","Author":"George Orwell","Pages":328,"Genres":["Dystopian","Science Fiction"]}""";

        writer.WriteSection("Deserialized Book");
        var deserializedBook = jsonService.Deserialize(jsonInput);
        writer.WriteBook(deserializedBook);
    }
}