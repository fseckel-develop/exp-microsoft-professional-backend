using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.Entities;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    public string CustomerName { get; set; } = string.Empty;

    public DateTime DatePlaced { get; set; } = DateTime.UtcNow;

    public List<OrderItem> OrderItems { get; set; } = new();

    public decimal TotalAmount => OrderItems.Sum(i => i.SubTotal);


#region Behavior

    public void AddItem(OrderItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        var existing = OrderItems.FirstOrDefault(o => o.InventoryItemId == item.InventoryItemId);
        if (existing != null)
        {
            existing.QuantityOrdered += item.QuantityOrdered;
        }
        else
        {
            OrderItems.Add(item);
        }
    }

    public bool RemoveItem(int orderItemId)
    {
        var item = OrderItems.FirstOrDefault(i => i.OrderItemId == orderItemId);
        if (item != null)
        {
            OrderItems.Remove(item);
            return true;
        }

        return false;
    }

    public string GetOrderSummary()
    {
        return $"Order #{OrderId} for {CustomerName} | Items: {OrderItems.Count} | Placed: {DatePlaced:dd/MM/yyyy}";
    }

    public override string ToString() => GetOrderSummary();

#endregion
}