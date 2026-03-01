using FluentAssertions;
using LogiTrack.Api.Contracts.Inventory;
using LogiTrack.Api.Data;
using LogiTrack.Api.Models;
using LogiTrack.Api.Services.Inventory;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LogiTrack.Api.Tests.Services.Inventory;

public class InventoryServiceTests
{
    private static LogiTrackDbContext CreateContext(SqliteConnection connection)
    {
        var options = new DbContextOptionsBuilder<LogiTrackDbContext>()
            .UseSqlite(connection)
            .Options;

        return new LogiTrackDbContext(options);
    }

    private static IMemoryCache CreateCache() => new MemoryCache(new MemoryCacheOptions());

    [Fact]
    public async Task CreateAsync_ShouldCreateInventoryItem()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        using var context = CreateContext(connection);
        await context.Database.EnsureCreatedAsync();

        var service = new InventoryService(context, CreateCache());

        var dto = new CreateInventoryItemDto
        {
            Name = "Widget",
            QuantityInStock = 10,
            Location = "A1",
            Price = 5m
        };

        var result = await service.CreateAsync(dto);

        result.Name.Should().Be("Widget");
        result.QuantityInStock.Should().Be(10);
        result.Location.Should().Be("A1");
        result.Price.Should().Be(5m);

        context.InventoryItems.Should().ContainSingle();
    }

    [Fact]
    public async Task GetByIdAsync_WhenItemExists_ShouldReturnItem()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        using (var setup = CreateContext(connection))
        {
            await setup.Database.EnsureCreatedAsync();

            var item = new InventoryItem();
            item.UpdateDetails("Widget", 10, "A1", 5m);
            setup.InventoryItems.Add(item);

            await setup.SaveChangesAsync();
        }

        using var context = CreateContext(connection);
        var service = new InventoryService(context, CreateCache());

        var result = await service.GetByIdAsync(1);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Widget");
    }

    [Fact]
    public async Task UpdateAsync_WhenItemExists_ShouldUpdateAndReturnDto()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        using (var setup = CreateContext(connection))
        {
            await setup.Database.EnsureCreatedAsync();

            var item = new InventoryItem();
            item.UpdateDetails("Old Widget", 10, "A1", 5m);
            setup.InventoryItems.Add(item);

            await setup.SaveChangesAsync();
        }

        using var context = CreateContext(connection);
        var service = new InventoryService(context, CreateCache());

        var dto = new UpdateInventoryItemDto
        {
            Name = "New Widget",
            QuantityInStock = 25,
            Location = "B2",
            Price = 8m
        };

        var result = await service.UpdateAsync(1, dto);

        result.Should().NotBeNull();
        result!.Name.Should().Be("New Widget");
        result.QuantityInStock.Should().Be(25);
        result.Location.Should().Be("B2");
        result.Price.Should().Be(8m);
    }

    [Fact]
    public async Task UpdateAsync_WhenItemMissing_ShouldReturnNull()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        using var context = CreateContext(connection);
        await context.Database.EnsureCreatedAsync();

        var service = new InventoryService(context, CreateCache());

        var dto = new UpdateInventoryItemDto
        {
            Name = "Widget",
            QuantityInStock = 1,
            Location = "A1",
            Price = 1m
        };

        var result = await service.UpdateAsync(999, dto);

        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_WhenItemExists_ShouldReturnTrue()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        using (var setup = CreateContext(connection))
        {
            await setup.Database.EnsureCreatedAsync();

            var item = new InventoryItem();
            item.UpdateDetails("Widget", 10, "A1", 5m);
            setup.InventoryItems.Add(item);

            await setup.SaveChangesAsync();
        }

        using var context = CreateContext(connection);
        var service = new InventoryService(context, CreateCache());

        var deleted = await service.DeleteAsync(1);

        deleted.Should().BeTrue();
        context.InventoryItems.Should().BeEmpty();
    }
}