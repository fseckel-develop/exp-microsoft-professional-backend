namespace EfCoreMySQLDemo.Models;

public class Drink
{
    public int DrinkId { get; set; }
    public string Name { get; set; } = null!;
    public decimal PriceEuro { get; set; }
}