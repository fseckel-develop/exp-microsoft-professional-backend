using FluentAssertions;
using LogiTrack.Api.Data;
using LogiTrack.Api.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace LogiTrack.Api.Tests.Data;

public class LogiTrackDbContextTests
{
    private static DbContextOptions<LogiTrackDbContext> CreateOptions(SqliteConnection connection)
    {
        return new DbContextOptionsBuilder<LogiTrackDbContext>()
            .UseSqlite(connection)
            .Options;
    }

    [Fact]
    public void EnsureCreated_ShouldCreateSchemaSuccessfully()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = CreateOptions(connection);

        using var context = new LogiTrackDbContext(options);

        var created = context.Database.EnsureCreated();

        created.Should().BeTrue();
    }

    [Fact]
    public async Task CanPersistInventoryOrderAndOrderItems()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        var options = CreateOptions(connection);

        using (var context = new LogiTrackDbContext(options))
        {
            await context.Database.EnsureCreatedAsync();

            var inventory = new InventoryItem();
            inventory.UpdateDetails("Pallet Jack", 12, "Warehouse A", 199.99m);

            var order = new Order();
            order.UpdateCustomerName("Acme Corp");

            var orderItem = new OrderItem
            {
                InventoryItem = inventory
            };
            orderItem.SetValues(2, inventory.Price);

            order.AddItem(orderItem);

            context.InventoryItems.Add(inventory);
            context.Orders.Add(order);

            await context.SaveChangesAsync();
        }

        using (var context = new LogiTrackDbContext(options))
        {
            var savedOrder = await context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.InventoryItem)
                .SingleAsync();

            savedOrder.CustomerName.Should().Be("Acme Corp");
            savedOrder.OrderItems.Should().ContainSingle();
            savedOrder.OrderItems[0].InventoryItem.Should().NotBeNull();
            savedOrder.OrderItems[0].InventoryItem!.Name.Should().Be("Pallet Jack");
            savedOrder.TotalAmount.Should().Be(399.98m);
        }
    }
}