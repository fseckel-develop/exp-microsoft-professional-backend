using EfCoreMySQLDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreMySQLDemo.Data;

/*
 * Run for the first time to create the database and table:
 *  dotnet ef migrations add InitialCreate \
 *      --context CoffeeShopDbContext \
 *      --output-dir Data/Migrations
 *  dotnet ef database update --context CoffeeShopDbContext
 */

public class CoffeeShopDbContext : DbContext
{
    public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options) : base(options) { }

    public DbSet<Drink> Drinks => Set<Drink>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Drink>(e =>
        {
            e.HasKey(x => x.DrinkId);
            e.Property(x => x.Name).IsRequired().HasMaxLength(80);
            e.Property(x => x.PriceEuro).HasPrecision(10, 2);
        });
    }
}