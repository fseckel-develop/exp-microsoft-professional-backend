using DesignPatternsDemo.Adapter;
using DesignPatternsDemo.Factory;
using DesignPatternsDemo.Models;

namespace DesignPatternsMonitoringDemo;

internal static class Program
{
    private static void Main()
    {
        // SINGLETON PATTERN USAGE
        var monitoringHub = MonitoringHub.GetInstance();

        // FACTORY PATTERN USAGE
        var phoneDisplay = DisplayFactory.Create("phone");
        var desktopDisplay = DisplayFactory.Create("desktop");
        var controlRoomDisplay = DisplayFactory.Create("control-room");

        // OBSERVER PATTERN USAGE
        monitoringHub.RegisterObserver(phoneDisplay);
        monitoringHub.RegisterObserver(desktopDisplay);
        monitoringHub.RegisterObserver(controlRoomDisplay);

        Console.WriteLine();
        Console.WriteLine("Updating monitoring hub temperature...");
        monitoringHub.SetTemperature(30.0f);

        Console.WriteLine();
        var damStation = new DamStation();

        // ADAPTER PATTERN USAGE
        List<IMonitorTarget> stations =
        [
            new StationAdapter(monitoringHub),
            new StationAdapter(damStation)
        ];

        Console.WriteLine();
        Console.WriteLine("Requesting unified station status output...");
        foreach (var station in stations)
        {
            station.ShowStatus();
        }
    }
}