namespace DesignPatternsDemo.Observer;

public sealed class PhoneDisplay : Display
{
    public override string Name => "Phone Display";

    public override void Update(float temperature)
    {
        Console.WriteLine($"{Name}: Temperature updated to {temperature} °C.");
    }
}