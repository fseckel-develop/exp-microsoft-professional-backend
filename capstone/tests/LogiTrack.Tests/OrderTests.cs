using FluentAssertions;
using LogiTrack.Api.Entities;

namespace LogiTrack.Tests;

public class OrderTests
{
    [Fact]
    public void AddItem_ShouldIncreaseCount()
    {
        var order = new Order { CustomerName = "Bob" };
        var item = new OrderItem
        {
            InventoryItem = new InventoryItem { Name = "Gadget", QuantityInStock = 5, Location = "A1" },
            InventoryItemId = 1,
            UnitPrice = 10.0m,
            QuantityOrdered = 2
        };

        order.AddItem(item);

        order.OrderItems.Should().Contain(item);
    }

    [Fact]
    public void AddItem_DuplicateShouldMergeQuantities()
    {
        var order = new Order { CustomerName = "Henry" };
        var first = new OrderItem { InventoryItem = new InventoryItem(), InventoryItemId = 5, UnitPrice = 1m, QuantityOrdered = 1 };
        var second = new OrderItem { InventoryItem = first.InventoryItem, InventoryItemId = 5, UnitPrice = 1m, QuantityOrdered = 3 };

        order.AddItem(first);
        order.AddItem(second);

        order.OrderItems.Should().HaveCount(1);
        order.OrderItems[0].QuantityOrdered.Should().Be(4);
    }

    [Fact]
    public void ToString_ShouldReturnSummary()
    {
        var order = new Order { CustomerName = "Isaac" };
        order.AddItem(new OrderItem { InventoryItem = new InventoryItem(), InventoryItemId = 1, UnitPrice = 2m, QuantityOrdered = 1 });

        order.ToString().Should().Be(order.GetOrderSummary());
    }

    [Fact]
    public void RemoveItem_WhenPresent_ShouldReturnTrueAndUpdate()
    {
        var order = new Order { CustomerName = "Carol" };
        var item = new OrderItem { OrderItemId = 42, InventoryItem = new InventoryItem(), InventoryItemId = 1, UnitPrice = 1, QuantityOrdered = 1 };
        order.AddItem(item);

        var result = order.RemoveItem(42);

        result.Should().BeTrue();
        order.OrderItems.Should().BeEmpty();
    }

    [Fact]
    public void RemoveItem_WhenMissing_ShouldReturnFalse()
    {
        var order = new Order { CustomerName = "Dave" };

        var result = order.RemoveItem(999);

        result.Should().BeFalse();
    }

    [Fact]
    public void TotalAmount_ShouldSumSubtotals()
    {
        var order = new Order { CustomerName = "Frank" };
        order.AddItem(new OrderItem { InventoryItem = new InventoryItem(), InventoryItemId = 1, UnitPrice = 5m, QuantityOrdered = 2 });
        order.AddItem(new OrderItem { InventoryItem = new InventoryItem(), InventoryItemId = 2, UnitPrice = 3m, QuantityOrdered = 1 });

        order.TotalAmount.Should().Be(13m);
    }

    [Fact]
    public void GetOrderSummary_ShouldContainRelevantInfo()
    {
        var order = new Order { CustomerName = "Grace" };
        order.AddItem(new OrderItem { InventoryItem = new InventoryItem(), InventoryItemId = 1, UnitPrice = 2m, QuantityOrdered = 1 });

        var summary = order.GetOrderSummary();
        summary.Should().Contain("Order #");
        summary.Should().Contain("Grace");
        summary.Should().Contain("Items: 1");
    }
}