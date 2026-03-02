using Microsoft.EntityFrameworkCore;
using EfCoreSQLiteDemo.Models;
using EfCoreSQLiteDemo.Data.Configurations;
using EfCoreSQLiteDemo.Data.Seed;

namespace EfCoreSQLiteDemo.Data;

public class LibraryDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite("Data Source=Data/LibraryApp.db")
            .EnableSensitiveDataLogging(false);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());

        SeedData.Apply(modelBuilder);
    }
}