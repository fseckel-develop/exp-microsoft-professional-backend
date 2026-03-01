using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    public string CustomerName { get; private set; } = string.Empty;

    public DateTime DatePlaced { get; set; } = DateTime.UtcNow;

    public List<OrderItem> OrderItems { get; set; } = new();

    public decimal TotalAmount => OrderItems.Sum(i => i.SubTotal);


#region behaviour

    public void UpdateCustomerName(string customerName)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name cannot be empty.", nameof(customerName));

        CustomerName = customerName;
    }

    public void AddItem(OrderItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        var existing = OrderItems.FirstOrDefault(o => o.InventoryItemId == item.InventoryItemId);
        if (existing != null)
        {
            existing.IncreaseQuantity(item.QuantityOrdered);
        }
        else
        {
            OrderItems.Add(item);
        }
    }

    public bool RemoveItem(int orderItemId)
    {
        var item = OrderItems.FirstOrDefault(i => i.OrderItemId == orderItemId);
        if (item is null)
            return false;

        OrderItems.Remove(item);
        return true;
    }
    
#endregion behaviour
}