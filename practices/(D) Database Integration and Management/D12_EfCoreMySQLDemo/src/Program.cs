using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EfCoreMySQLDemo.Data;
using EfCoreMySQLDemo.Services;

namespace EfCoreMySQLDemo;

internal static class Program
{
    private static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connection = configuration.GetConnectionString("MySQL")
            ?? throw new InvalidOperationException("Missing ConnectionStrings:MySQL");

        var options = new DbContextOptionsBuilder<CoffeeShopDbContext>()
            .UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 26)))
            .Options;

        using var db = new CoffeeShopDbContext(options);
        db.Database.Migrate();
        var drinks = new DrinkService(db);

        // CREATE
        var created = drinks.AddDrink(name: "Oat Latte", priceEur: 4.50m);
        Console.WriteLine($"Created: {created.DrinkId} - {created.Name} (€{created.PriceEuro})");

        // READ (all)
        Console.WriteLine("\nMenu:");
        foreach (var d in drinks.GetAll())
            Console.WriteLine($"{d.DrinkId}: {d.Name} (€{d.PriceEuro})");

        // READ (single) - pick the item we just created
        var one = drinks.GetById(created.DrinkId);
        Console.WriteLine($"\nFound: {one.DrinkId} - {one.Name} (€{one.PriceEuro})");

        // UPDATE
        drinks.ChangePrice(one.DrinkId, 4.20m);
        var updated = drinks.GetById(one.DrinkId);
        Console.WriteLine($"\nUpdated: {updated.DrinkId} - {updated.Name} (€{updated.PriceEuro})");

        // DELETE
        drinks.Remove(one.DrinkId);
        Console.WriteLine("\nDeleted item. Current menu:");
        foreach (var d in drinks.GetAll())
            Console.WriteLine($"{d.DrinkId}: {d.Name} (€{d.PriceEuro})");
    }
}