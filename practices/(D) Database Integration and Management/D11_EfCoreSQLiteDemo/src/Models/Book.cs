using System;

namespace EfCoreSQLiteDemo.Models;

public class Book
{
    public int BookId { get; set; }              // PK
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public DateTime AddedAt { get; set; }

    public int CategoryId { get; set; }          // FK
    public Category? Category { get; set; }      // Navigation
}
