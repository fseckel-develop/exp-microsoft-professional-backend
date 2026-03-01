using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogiTrack.Api.Models;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    [ForeignKey(nameof(Order))]
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    [ForeignKey(nameof(InventoryItem))]
    public int InventoryItemId { get; set; }
    public InventoryItem? InventoryItem { get; set; }

    [Required]
    public int QuantityOrdered { get; private set; }

    [Required]
    public decimal UnitPrice { get; private set; }

    public decimal SubTotal => QuantityOrdered * UnitPrice;


#region behaviour

    public void SetValues(int quantityOrdered, decimal unitPrice)
    {
        if (quantityOrdered < 1)
            throw new ArgumentException("Quantity must be at least 1.", nameof(quantityOrdered));

        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice));

        QuantityOrdered = quantityOrdered;
        UnitPrice = unitPrice;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity < 1)
            throw new ArgumentException("Quantity must be at least 1.", nameof(newQuantity));

        QuantityOrdered = newQuantity;
    }

    public void IncreaseQuantity(int amount)
    {
        if (amount < 1)
            throw new ArgumentException("Increase amount must be at least 1.", nameof(amount));

        QuantityOrdered += amount;
    }

#endregion behaviour
}