using FluentAssertions;
using LogiTrack.Api.Models;

namespace LogiTrack.Api.Tests.Models;

public class OrderTests
{
    [Fact]
    public void UpdateCustomerName_ShouldSetCustomerName()
    {
        var order = new Order();

        order.UpdateCustomerName("Bob");

        order.CustomerName.Should().Be("Bob");
    }

    [Fact]
    public void UpdateCustomerName_WithInvalidName_ShouldThrow()
    {
        var order = new Order();

        Action act = () => order.UpdateCustomerName("");

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Customer name cannot be empty*");
    }

    [Fact]
    public void AddItem_ShouldAddItemToOrder()
    {
        var order = new Order();
        order.UpdateCustomerName("Bob");

        var item = new OrderItem
        {
            InventoryItemId = 1
        };
        item.SetValues(2, 10m);

        order.AddItem(item);

        order.OrderItems.Should().ContainSingle();
        order.OrderItems[0].InventoryItemId.Should().Be(1);
        order.OrderItems[0].QuantityOrdered.Should().Be(2);
    }

    [Fact]
    public void AddItem_WithSameInventoryItem_ShouldMergeQuantities()
    {
        var order = new Order();
        order.UpdateCustomerName("Henry");

        var first = new OrderItem { InventoryItemId = 5 };
        first.SetValues(1, 1m);

        var second = new OrderItem { InventoryItemId = 5 };
        second.SetValues(3, 1m);

        order.AddItem(first);
        order.AddItem(second);

        order.OrderItems.Should().HaveCount(1);
        order.OrderItems[0].QuantityOrdered.Should().Be(4);
    }

    [Fact]
    public void AddItem_WithNull_ShouldThrow()
    {
        var order = new Order();
        order.UpdateCustomerName("Test");

        Action act = () => order.AddItem(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void RemoveItem_WhenPresent_ShouldReturnTrueAndRemoveIt()
    {
        var order = new Order();
        order.UpdateCustomerName("Carol");

        var item = new OrderItem
        {
            OrderItemId = 42,
            InventoryItemId = 1
        };
        item.SetValues(1, 1m);

        order.AddItem(item);

        var result = order.RemoveItem(42);

        result.Should().BeTrue();
        order.OrderItems.Should().BeEmpty();
    }

    [Fact]
    public void RemoveItem_WhenMissing_ShouldReturnFalse()
    {
        var order = new Order();
        order.UpdateCustomerName("Dave");

        var result = order.RemoveItem(999);

        result.Should().BeFalse();
    }

    [Fact]
    public void TotalAmount_ShouldSumSubtotals()
    {
        var order = new Order();
        order.UpdateCustomerName("Frank");

        var first = new OrderItem { InventoryItemId = 1 };
        first.SetValues(2, 5m);

        var second = new OrderItem { InventoryItemId = 2 };
        second.SetValues(1, 3m);

        order.AddItem(first);
        order.AddItem(second);

        order.TotalAmount.Should().Be(13m);
    }
}