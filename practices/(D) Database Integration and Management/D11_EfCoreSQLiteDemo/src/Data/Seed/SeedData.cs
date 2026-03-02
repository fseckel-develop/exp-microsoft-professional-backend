using Microsoft.EntityFrameworkCore;
using EfCoreSQLiteDemo.Models;

namespace EfCoreSQLiteDemo.Data.Seed;

public static class SeedData
{
    // A fixed timestamp keeps seeding deterministic
    private static readonly DateTime SeedTime = new DateTime(2025, 01, 01, 12, 00, 00, DateTimeKind.Utc);

    public static void Apply(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Science Fiction" },
            new Category { CategoryId = 2, Name = "Programming" }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                BookId = 1,
                Title = "Dune",
                Author = "Frank Herbert",
                AddedAt = SeedTime,
                CategoryId = 1
            },
            new Book
            {
                BookId = 2,
                Title = "Clean Code",
                Author = "Robert C. Martin",
                AddedAt = SeedTime,
                CategoryId = 2
            }
        );
    }
}