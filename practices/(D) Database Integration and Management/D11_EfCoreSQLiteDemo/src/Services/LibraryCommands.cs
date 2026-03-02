using EfCoreSQLiteDemo.Models;
using EfCoreSQLiteDemo.Data;

namespace EfCoreSQLiteDemo.Services;

public static class LibraryCommands
{
    public static Book AddBook(LibraryDbContext db, string title, string author, int categoryId)
    {
        var book = new Book
        {
            Title = title,
            Author = author,
            AddedAt = DateTime.UtcNow,
            CategoryId = categoryId
        };

        db.Books.Add(book);
        db.SaveChanges();
        return book;
    }
}