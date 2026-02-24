using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogiTrack.Api.Entities;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    [ForeignKey("InventoryItem")]
    public int InventoryItemId { get; set; }
    public InventoryItem? InventoryItem { get; set; }

    [Required]
    public int QuantityOrdered { get; set; }

    [Required]
    public decimal UnitPrice { get; set; }

    public decimal SubTotal => QuantityOrdered * UnitPrice;


#region Behavior

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity < 1)
            throw new ArgumentException("Quantity must be at least 1.", nameof(newQuantity));

        QuantityOrdered = newQuantity;
    }

#endregion
}