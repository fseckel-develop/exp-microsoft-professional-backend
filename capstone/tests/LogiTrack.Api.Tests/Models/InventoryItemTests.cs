using FluentAssertions;
using LogiTrack.Api.Models;

namespace LogiTrack.Api.Tests.Models;

public class InventoryItemTests
{
    [Fact]
    public void UpdateDetails_ShouldSetAllProperties()
    {
        var item = new InventoryItem();

        item.UpdateDetails("Widget", 100, "Aisle 3", 19.99m);

        item.Name.Should().Be("Widget");
        item.QuantityInStock.Should().Be(100);
        item.Location.Should().Be("Aisle 3");
        item.Price.Should().Be(19.99m);
    }

    [Fact]
    public void UpdateDetails_WithInvalidName_ShouldThrow()
    {
        var item = new InventoryItem();

        Action act = () => item.UpdateDetails("", 10, "A1", 5m);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Name cannot be empty*");
    }

    [Fact]
    public void UpdateDetails_WithNegativeQuantity_ShouldThrow()
    {
        var item = new InventoryItem();

        Action act = () => item.UpdateDetails("Widget", -1, "A1", 5m);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Quantity cannot be negative*");
    }

    [Fact]
    public void UpdateDetails_WithInvalidLocation_ShouldThrow()
    {
        var item = new InventoryItem();

        Action act = () => item.UpdateDetails("Widget", 10, "", 5m);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Location cannot be empty*");
    }

    [Fact]
    public void UpdateDetails_WithNegativePrice_ShouldThrow()
    {
        var item = new InventoryItem();

        Action act = () => item.UpdateDetails("Widget", 10, "A1", -1m);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Price cannot be negative*");
    }

    [Fact]
    public void IncreaseStock_ShouldIncreaseQuantity()
    {
        var item = new InventoryItem();
        item.UpdateDetails("Widget", 10, "A1", 5m);

        item.IncreaseStock(5);

        item.QuantityInStock.Should().Be(15);
    }

    [Fact]
    public void IncreaseStock_WithInvalidAmount_ShouldThrow()
    {
        var item = new InventoryItem();
        item.UpdateDetails("Widget", 10, "A1", 5m);

        Action act = () => item.IncreaseStock(0);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Amount to increase must be positive*");
    }

    [Fact]
    public void DecreaseStock_ShouldDecreaseAndReturnTrue()
    {
        var item = new InventoryItem();
        item.UpdateDetails("Widget", 10, "A1", 5m);

        var result = item.DecreaseStock(4);

        result.Should().BeTrue();
        item.QuantityInStock.Should().Be(6);
    }

    [Fact]
    public void DecreaseStock_WhenNotEnoughStock_ShouldReturnFalse()
    {
        var item = new InventoryItem();
        item.UpdateDetails("Widget", 3, "A1", 5m);

        var result = item.DecreaseStock(5);

        result.Should().BeFalse();
        item.QuantityInStock.Should().Be(3);
    }

    [Fact]
    public void DecreaseStock_WithInvalidAmount_ShouldThrow()
    {
        var item = new InventoryItem();
        item.UpdateDetails("Widget", 10, "A1", 5m);

        Action act = () => item.DecreaseStock(0);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Amount to decrease must be positive*");
    }
}