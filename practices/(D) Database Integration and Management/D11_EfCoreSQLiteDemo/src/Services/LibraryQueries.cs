using Microsoft.EntityFrameworkCore;
using EfCoreSQLiteDemo.Models;
using EfCoreSQLiteDemo.Data;

namespace EfCoreSQLiteDemo.Services;

public static class LibraryQueries
{
    public static List<Book> GetAllBooks(LibraryDbContext db) =>
        db.Books
          .Include(b => b.Category)
          .AsNoTracking()
          .ToList();

    public static List<Book> GetBooksInCategory(LibraryDbContext db, string categoryName) =>
        db.Books
          .Include(b => b.Category)
          .Where(b => b.Category != null && b.Category.Name == categoryName)
          .AsNoTracking()
          .ToList();
}