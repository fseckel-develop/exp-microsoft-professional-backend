using FluentAssertions;
using LogiTrack.Api.Contracts.Orders;
using LogiTrack.Api.Data;
using LogiTrack.Api.Models;
using LogiTrack.Api.Services.Orders;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LogiTrack.Api.Tests.Services;

public class OrderServiceTests
{
    private static LogiTrackDbContext CreateContext(SqliteConnection connection)
    {
        var options = new DbContextOptionsBuilder<LogiTrackDbContext>()
            .UseSqlite(connection)
            .Options;

        return new LogiTrackDbContext(options);
    }

    private static IMemoryCache CreateCache()
    {
        return new MemoryCache(new MemoryCacheOptions());
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateOrderAndDecreaseStock()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        using (var setupContext = CreateContext(connection))
        {
            await setupContext.Database.EnsureCreatedAsync();

            var item = new InventoryItem();
            item.UpdateDetails("Widget", 10, "A1", 5m);

            setupContext.InventoryItems.Add(item);
            await setupContext.SaveChangesAsync();
        }

        using (var context = CreateContext(connection))
        {
            var service = new OrderService(context, CreateCache());

            var dto = new CreateOrderDto
            {
                CustomerName = "Acme Corp",
                OrderItems =
                [
                    new CreateOrderItemDto
                    {
                        InventoryItemId = 1,
                        QuantityOrdered = 3
                    }
                ]
            };

            var result = await service.CreateAsync(dto);

            result.Success.Should().BeTrue();
            result.Order.Should().NotBeNull();
            result.Order!.CustomerName.Should().Be("Acme Corp");
            result.Order.OrderItems.Should().ContainSingle();
        }

        using (var verifyContext = CreateContext(connection))
        {
            var inventory = await verifyContext.InventoryItems.SingleAsync();
            inventory.QuantityInStock.Should().Be(7);
        }
    }

    [Fact]
    public async Task CreateAsync_WhenInventoryItemDoesNotExist_ShouldFail()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        using var context = CreateContext(connection);
        await context.Database.EnsureCreatedAsync();

        var service = new OrderService(context, CreateCache());

        var dto = new CreateOrderDto
        {
            CustomerName = "Acme Corp",
            OrderItems =
            [
                new CreateOrderItemDto
                {
                    InventoryItemId = 999,
                    QuantityOrdered = 2
                }
            ]
        };

        var result = await service.CreateAsync(dto);

        result.Success.Should().BeFalse();
        result.ErrorCode.Should().Be(OrderErrorCode.InventoryItemNotFound);
    }

    [Fact]
    public async Task CreateAsync_WhenStockIsInsufficient_ShouldFail()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        using (var setupContext = CreateContext(connection))
        {
            await setupContext.Database.EnsureCreatedAsync();

            var item = new InventoryItem();
            item.UpdateDetails("Widget", 2, "A1", 5m);

            setupContext.InventoryItems.Add(item);
            await setupContext.SaveChangesAsync();
        }

        using var context = CreateContext(connection);
        var service = new OrderService(context, CreateCache());

        var dto = new CreateOrderDto
        {
            CustomerName = "Acme Corp",
            OrderItems =
            [
                new CreateOrderItemDto
                {
                    InventoryItemId = 1,
                    QuantityOrdered = 5
                }
            ]
        };

        var result = await service.CreateAsync(dto);

        result.Success.Should().BeFalse();
        result.ErrorCode.Should().Be(OrderErrorCode.InsufficientStock);
    }

    [Fact]
    public async Task RemoveItemAsync_ShouldRestoreStock()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        using (var setupContext = CreateContext(connection))
        {
            await setupContext.Database.EnsureCreatedAsync();

            var inventory = new InventoryItem();
            inventory.UpdateDetails("Widget", 10, "A1", 5m);
            inventory.DecreaseStock(3);

            var order = new Order();
            order.UpdateCustomerName("Acme Corp");

            var orderItem = new OrderItem
            {
                InventoryItemId = 1,
                InventoryItem = inventory
            };
            orderItem.SetValues(3, 5m);

            order.AddItem(orderItem);

            setupContext.InventoryItems.Add(inventory);
            setupContext.Orders.Add(order);

            await setupContext.SaveChangesAsync();
        }

        using (var context = CreateContext(connection))
        {
            var service = new OrderService(context, CreateCache());

            var result = await service.RemoveItemAsync(1, 1);

            result.Success.Should().BeTrue();
        }

        using (var verifyContext = CreateContext(connection))
        {
            var inventory = await verifyContext.InventoryItems.SingleAsync();
            inventory.QuantityInStock.Should().Be(10);
        }
    }

    [Fact]
    public async Task AdjustItemQuantityAsync_Increase_ShouldReduceStock()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        using (var setupContext = CreateContext(connection))
        {
            await setupContext.Database.EnsureCreatedAsync();

            var inventory = new InventoryItem();
            inventory.UpdateDetails("Widget", 10, "A1", 5m);
            inventory.DecreaseStock(2);

            var order = new Order();
            order.UpdateCustomerName("Acme Corp");

            var orderItem = new OrderItem
            {
                InventoryItemId = 1,
                InventoryItem = inventory
            };
            orderItem.SetValues(2, 5m);

            order.AddItem(orderItem);

            setupContext.InventoryItems.Add(inventory);
            setupContext.Orders.Add(order);

            await setupContext.SaveChangesAsync();
        }

        using (var context = CreateContext(connection))
        {
            var service = new OrderService(context, CreateCache());

            var result = await service.AdjustItemQuantityAsync(1, 1, 3);

            result.Success.Should().BeTrue();
            result.Order!.OrderItems[0].QuantityOrdered.Should().Be(5);
        }

        using (var verifyContext = CreateContext(connection))
        {
            var inventory = await verifyContext.InventoryItems.SingleAsync();
            inventory.QuantityInStock.Should().Be(5);
        }
    }
}