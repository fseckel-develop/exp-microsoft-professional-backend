using EfCoreMySQLDemo.Data;
using EfCoreMySQLDemo.Models;

namespace EfCoreMySQLDemo.Services;

public sealed class DrinkService
{
    private readonly CoffeeShopDbContext _db;

    public DrinkService(CoffeeShopDbContext db) => _db = db;

    public Drink AddDrink(string name, decimal priceEur)
    {
        var drink = new Drink { Name = name, PriceEuro = priceEur };
        _db.Drinks.Add(drink);
        _db.SaveChanges();
        return drink;
    }

    public List<Drink> GetAll() => _db.Drinks.ToList();

    public Drink GetById(int id)
        => _db.Drinks.Find(id) ?? throw new InvalidOperationException($"Drink {id} not found.");

    public void ChangePrice(int id, decimal newPriceEur)
    {
        var drink = GetById(id);
        drink.PriceEuro = newPriceEur;
        _db.SaveChanges();
    }

    public void Remove(int id)
    {
        var drink = GetById(id);
        _db.Drinks.Remove(drink);
        _db.SaveChanges();
    }
}