using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.Models;

public class InventoryItem
{
    [Key]
    public int ItemId { get; set; }

    [Required]
    public string Name { get; private set; } = string.Empty;

    [Required]
    public int QuantityInStock { get; private set; }

    [Required]
    public string Location { get; private set; } = string.Empty;

    [Required]
    public decimal Price { get; private set; }

    public List<OrderItem> OrderItems { get; set; } = new();


#region behaviour

    public void UpdateDetails(string name, int quantityInStock, string location, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        if (quantityInStock < 0)
            throw new ArgumentException("Quantity cannot be negative.", nameof(quantityInStock));

        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location cannot be empty.", nameof(location));

        if (price < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(price));

        Name = name;
        QuantityInStock = quantityInStock;
        Location = location;
        Price = price;
    }

    public void IncreaseStock(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount to increase must be positive.", nameof(amount));

        QuantityInStock += amount;
    }

    public bool DecreaseStock(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount to decrease must be positive.", nameof(amount));

        if (amount > QuantityInStock)
            return false;

        QuantityInStock -= amount;
        return true;
    }
    
#endregion behaviour
}