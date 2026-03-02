using EfCoreSQLiteDemo.Data;
using EfCoreSQLiteDemo.Services;

namespace EfCoreSQLiteDemo;

public static class Program
{
    public static void Main()
    {
        using var db = new LibraryDbContext();

        // Ensure database exists (optional but handy for demos)
        db.Database.EnsureCreated();
        /*
         * In a real app, you would typically use EF Core Migrations to manage your database schema.
         * For simplicity, this demo uses EnsureCreated to create the database if it doesn't exist.
         * To use Migrations, you would:
         *   1. Install the EF Core Tools package (already included in the project file).
         *   2. Run 'dotnet ef migrations add InitialCreate --context LibraryDbContext 
         *      --output-dir Data/Migrations' to create a migration based on your model.
         *   3. Run 'dotnet ef database update --context LibraryDbContext' 
         *      to apply the migration and create/update the database schema.
        */

        // READ: all books with category
        var allBooks = LibraryQueries.GetAllBooks(db);
        Console.WriteLine("All books:");
        foreach (var b in allBooks)
            Console.WriteLine($"{b.Title} by {b.Author} — {b.Category?.Name ?? "Uncategorized"}");

        // READ: books in a specific category
        var sciFi = LibraryQueries.GetBooksInCategory(db, "Science Fiction");
        Console.WriteLine("\nScience Fiction books:");
        foreach (var b in sciFi)
            Console.WriteLine($"{b.Title}");

        // CREATE: add a new book
        var added = LibraryCommands.AddBook(db,
            title: "The Pragmatic Programmer",
            author: "Andrew Hunt & David Thomas",
            categoryId: 2 // e.g. Programming
        );

        Console.WriteLine($"\nAdded: {added.Title} (Id={added.BookId})");
    }
}