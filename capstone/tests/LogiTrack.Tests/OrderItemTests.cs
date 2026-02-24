using FluentAssertions;
using LogiTrack.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Tests;

public class OrderItemTests
{
    [Fact]
    public void SubTotal_ShouldMultiplyQuantityByUnitPrice()
    {
        var item = new OrderItem { QuantityOrdered = 3, UnitPrice = 2.5m };
        item.SubTotal.Should().Be(7.5m);
    }

    [Fact]
    public void UpdateQuantity_ShouldChangeQuantity()
    {
        var order = new Order { CustomerName = "Zack" };
        var item = new OrderItem
        {
            Order = order,
            QuantityOrdered = 1,
            UnitPrice = 10m
        };

        item.UpdateQuantity(5);

        item.QuantityOrdered.Should().Be(5);
    }

    [Fact]
    public void UpdateQuantity_Invalid_ShouldThrow()
    {
        var order = new Order { CustomerName = "Yara" };
        var item = new OrderItem { Order = order, QuantityOrdered = 2, UnitPrice = 1m };

        Action act = () => item.UpdateQuantity(0);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CannotSetInvalidQuantityOrPrice_ValidationFails()
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(new OrderItem { QuantityOrdered = 0, UnitPrice = 1m });
        bool valid = Validator.TryValidateObject(context.ObjectInstance, context, results, true);
        valid.Should().BeFalse();
        results.Should().Contain(r => r.MemberNames.Contains("QuantityOrdered"));

        results.Clear();
        context = new ValidationContext(new OrderItem { QuantityOrdered = 1, UnitPrice = 0m });
        valid = Validator.TryValidateObject(context.ObjectInstance, context, results, true);
        valid.Should().BeFalse();
        results.Should().Contain(r => r.MemberNames.Contains("UnitPrice"));
    }
}