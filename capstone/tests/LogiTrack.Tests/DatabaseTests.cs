using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using LogiTrack.Api.Entities;

namespace LogiTrack.Tests;

public class DatabaseTests
{
    private DbContextOptions<LogiTrackContext> CreateInMemoryOptions(SqliteConnection connection)
    {
        // Use the provided open connection so the database stays alive across contexts
        var builder = new DbContextOptionsBuilder<LogiTrackContext>();
        builder.UseSqlite(connection);
        return builder.Options;
    }

    [Fact]
    public void SeedingBlock_ShouldAddInventoryItemWhenNoneExist()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = CreateInMemoryOptions(connection);

        // Initial context creates schema and inserts seed
        using (var context = new LogiTrackContext(options))
        {
            context.Database.EnsureCreated();

            if (!context.InventoryItems.Any())
            {
                context.InventoryItems.Add(new InventoryItem
                {
                    Name = "Pallet Jack",
                    QuantityInStock = 12,
                    Location = "Warehouse A"
                });
                context.SaveChanges();
            }
        }

        // Second context reads back
        using (var context = new LogiTrackContext(options))
        {
            var items = context.InventoryItems.ToList();
            items.Should().ContainSingle();
            items[0].Name.Should().Be("Pallet Jack");
            items[0].QuantityInStock.Should().Be(12);
            items[0].Location.Should().Be("Warehouse A");

            var writer = new StringWriter();
            Console.SetOut(writer);
            items[0].DisplayInfo();
            var output = writer.ToString();
            output.Should().Contain("Item: Pallet Jack");
        }
    }
}