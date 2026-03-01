using DesignPatternsDemo.Adapter;
using DesignPatternsDemo.Observer;

namespace DesignPatternsDemo.Models;

public sealed class MonitoringHub : IStationSource
{
    private static MonitoringHub? _instance;
    private static readonly object _lock = new();

    private readonly List<IObserver> _observers = [];
    private float _temperatureCelsius;

    private MonitoringHub()
    {
        _temperatureCelsius = 20.0f;
        Console.WriteLine("Monitoring hub initialized.");
    }

    public static MonitoringHub GetInstance()
    {
        if (_instance is null)
        {
            lock (_lock)
            {
                _instance ??= new MonitoringHub();
            }
        }

        return _instance;
    }

    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void SetTemperature(float temperatureCelsius)
    {
        _temperatureCelsius = temperatureCelsius;
        NotifyObservers();
    }

    private void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_temperatureCelsius);
        }
    }

    public void OutputStationReading()
    {
        Console.WriteLine($"Monitoring Hub: Current temperature is {_temperatureCelsius} °C.");
    }
}