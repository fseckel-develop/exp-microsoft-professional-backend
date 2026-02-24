using FluentAssertions;
using LogiTrack.Api.Entities;

namespace LogiTrack.Tests;

public class InventoryItemTests
{
    [Fact]
    public void InventoryItem_Creation_ShouldSetProperties()
    {
        string name = "Widget";
        int quantity = 100;
        string location = "Aisle 3";

        var item = new InventoryItem
        {
            Name = name,
            QuantityInStock = quantity,
            Location = location
        };

        item.Name.Should().Be(name);
        item.QuantityInStock.Should().Be(quantity);
        item.Location.Should().Be(location);
    }

    [Fact]
    public void IncreaseStock_ShouldIncreaseQuantity()
    {
        var item = new InventoryItem { QuantityInStock = 10 };

        item.IncreaseStock(5);

        item.QuantityInStock.Should().Be(15);
    }

    [Fact]
    public void IncreaseStock_InvalidAmount_ShouldThrow()
    {
        var item = new InventoryItem();
        Action act = () => item.IncreaseStock(0);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DecreaseStock_ShouldDecreaseAndReturnTrue()
    {
        var item = new InventoryItem { QuantityInStock = 10 };

        bool result = item.DecreaseStock(4);

        result.Should().BeTrue();
        item.QuantityInStock.Should().Be(6);
    }

    [Fact]
    public void DecreaseStock_NotEnoughStock_ShouldReturnFalse()
    {
        var item = new InventoryItem { QuantityInStock = 3 };
        bool result = item.DecreaseStock(5);
        result.Should().BeFalse();
        item.QuantityInStock.Should().Be(3);
    }

    [Fact]
    public void DecreaseStock_InvalidAmount_ShouldThrow()
    {
        var item = new InventoryItem { QuantityInStock = 10 };
        Action act = () => item.DecreaseStock(0);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UpdateLocation_ShouldChangeLocation()
    {
        var item = new InventoryItem { Location = "A1" };

        item.UpdateLocation("B2");

        item.Location.Should().Be("B2");
    }

    [Fact]
    public void UpdateLocation_Invalid_ShouldThrow()
    {
        var item = new InventoryItem { Location = "A1" };
        Action act = () => item.UpdateLocation("");
        act.Should().Throw<ArgumentException>();
    }
}