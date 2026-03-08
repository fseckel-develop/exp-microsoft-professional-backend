using KnapsackMemoizationDemo.Models;

namespace KnapsackMemoizationDemo.Data;

public static class SupplyItemFactory
{
    public static IReadOnlyList<SupplyItem> CreateExpeditionSupplies()
    {
        return
        [
            new SupplyItem("Water Filter", 10, 60),
            new SupplyItem("First Aid Kit", 20, 100),
            new SupplyItem("Climbing Rope", 30, 120),
            new SupplyItem("Tent", 40, 200),
            new SupplyItem("Cooking Set", 25, 150),
            new SupplyItem("Solar Charger", 35, 180),
            new SupplyItem("Thermal Blanket", 15, 75),
            new SupplyItem("Flashlight", 22, 90),
            new SupplyItem("Map Pack", 28, 110),
            new SupplyItem("Tool Kit", 18, 95),
            new SupplyItem("Navigation Device", 33, 130),
            new SupplyItem("Portable Stove", 27, 170),
            new SupplyItem("Emergency Beacon", 12, 85),
            new SupplyItem("Sleeping Bag", 24, 140),
            new SupplyItem("Rain Shelter", 31, 160),
            new SupplyItem("Portable Battery", 45, 210),
            new SupplyItem("Compass", 8, 55),
            new SupplyItem("Hiking Boots", 26, 125),
            new SupplyItem("Dry Food Pack", 29, 145),
            new SupplyItem("Insulated Jacket", 38, 190),
            new SupplyItem("Water Container", 21, 155),
            new SupplyItem("Multi-Tool", 34, 175),
            new SupplyItem("Fire Starter", 17, 115),
            new SupplyItem("Signal Mirror", 14, 80),
            new SupplyItem("Repair Kit", 23, 135),
            new SupplyItem("Portable Radio", 32, 165),
            new SupplyItem("Notebook", 19, 105),
            new SupplyItem("Field Binoculars", 36, 195)
        ];
    }
}