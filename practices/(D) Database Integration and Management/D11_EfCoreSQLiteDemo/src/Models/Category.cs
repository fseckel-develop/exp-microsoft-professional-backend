using System.Collections.Generic;

namespace EfCoreSQLiteDemo.Models;

public class Category
{
    public int CategoryId { get; set; }         // PK
    public string Name { get; set; } = null!;

    public List<Book> Books { get; set; } = new();
}