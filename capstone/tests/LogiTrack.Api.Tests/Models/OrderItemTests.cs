using FluentAssertions;
using LogiTrack.Api.Models;

namespace LogiTrack.Api.Tests.Models;

public class OrderItemTests
{
    [Fact]
    public void SetValues_ShouldSetQuantityAndUnitPrice()
    {
        var item = new OrderItem();

        item.SetValues(3, 2.5m);

        item.QuantityOrdered.Should().Be(3);
        item.UnitPrice.Should().Be(2.5m);
        item.SubTotal.Should().Be(7.5m);
    }

    [Fact]
    public void SetValues_WithInvalidQuantity_ShouldThrow()
    {
        var item = new OrderItem();

        Action act = () => item.SetValues(0, 2.5m);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Quantity must be at least 1*");
    }

    [Fact]
    public void SetValues_WithNegativeUnitPrice_ShouldThrow()
    {
        var item = new OrderItem();

        Action act = () => item.SetValues(1, -1m);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Unit price cannot be negative*");
    }

    [Fact]
    public void UpdateQuantity_ShouldChangeQuantity()
    {
        var item = new OrderItem();
        item.SetValues(1, 10m);

        item.UpdateQuantity(5);

        item.QuantityOrdered.Should().Be(5);
    }

    [Fact]
    public void UpdateQuantity_WithInvalidValue_ShouldThrow()
    {
        var item = new OrderItem();
        item.SetValues(2, 10m);

        Action act = () => item.UpdateQuantity(0);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Quantity must be at least 1*");
    }

    [Fact]
    public void IncreaseQuantity_ShouldIncreaseQuantityOrdered()
    {
        var item = new OrderItem();
        item.SetValues(2, 10m);

        item.IncreaseQuantity(3);

        item.QuantityOrdered.Should().Be(5);
    }

    [Fact]
    public void IncreaseQuantity_WithInvalidAmount_ShouldThrow()
    {
        var item = new OrderItem();
        item.SetValues(2, 10m);

        Action act = () => item.IncreaseQuantity(0);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Increase amount must be at least 1*");
    }
}