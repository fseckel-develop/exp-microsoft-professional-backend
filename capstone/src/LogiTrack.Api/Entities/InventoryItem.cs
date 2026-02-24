using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Api.Entities;

public class InventoryItem
{
    [Key]
    public int ItemId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int QuantityInStock { get; set; }

    [Required]
    public string Location { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();

    
#region Behavior

    public void DisplayInfo()
    {
        Console.WriteLine($"Item: {Name} | Quantity: {QuantityInStock} | Location: {Location}");
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

    public void UpdateLocation(string newLocation)
    {
        if (string.IsNullOrWhiteSpace(newLocation))
            throw new ArgumentException("Location cannot be empty.", nameof(newLocation));

        Location = newLocation;
    }

#endregion   
}