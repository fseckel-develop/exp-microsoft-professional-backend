namespace DesignPatternsDemo.Observer;

public sealed class ControlRoomDisplay : Display
{
    public override string Name => "Control Room Display";

    public override void Update(float temperature)
    {
        Console.WriteLine($"{Name}: Central panel updated. Current temperature is {temperature} °C.");
    }
}