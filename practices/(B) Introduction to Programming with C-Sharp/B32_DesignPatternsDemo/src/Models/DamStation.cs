using DesignPatternsDemo.Adapter;

namespace DesignPatternsDemo.Models;

public sealed class DamStation : IStationSource
{
    public float WaterLevelMeters { get; private set; }

    public DamStation(float waterLevelMeters = 12.5f)
    {
        WaterLevelMeters = waterLevelMeters;
        Console.WriteLine("Dam station initialized.");
    }

    public void OutputStationReading()
    {
        Console.WriteLine($"Dam Station: Current water level is {WaterLevelMeters} meters.");
    }
}