namespace DesignPatternsDemo.Observer;

public sealed class DesktopDisplay : Display
{
    public override string Name => "Desktop Display";

    public override void Update(float temperature)
    {
        Console.WriteLine($"{Name}: Temperature updated to {temperature} °C.");
    }
}