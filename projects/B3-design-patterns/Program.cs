public class Program
{
    public static void Main(string[] args)
    {
        var weatherStation = WeatherStation.GetInstance();

        var phoneDisplay = DisplayFactory.CreateDisplay("Phone");
        var desktopDisplay = DisplayFactory.CreateDisplay("Desktop");

        weatherStation.RegisterObserver(phoneDisplay);
        weatherStation.RegisterObserver(desktopDisplay);

        weatherStation.SetTemperature(30);

        var damStation = new DamStation();

        var targets = new List<ITarget>
        {
            new StationAdapter(weatherStation),
            new StationAdapter(damStation)
        };

        foreach (ITarget target in targets)
        {
            target.Request();
        }
    }
}

// OBSERVER PATTERN:
public interface IObserver
{
    void Update(float temperature);
}

public abstract class Display : IObserver
{
    public abstract void Update(float temperature);
}

// FACTORY PATTERN:
public class PhoneDisplay : Display
{
    public override void Update(float temperature)
    {
        Console.WriteLine("Phone display: Temperature updated to " + temperature + " degrees.");
    }
}

public class DesktopDisplay : Display
{
    public override void Update(float temperature)
    {
        Console.WriteLine("Desktop display: Temperature updated to " + temperature + " degrees.");
    }
}

public class DisplayFactory
{
    public static Display CreateDisplay(string type)
    {
        return type switch
        {
            "Phone"   => new PhoneDisplay(),
            "Desktop" => new DesktopDisplay(),
            _         => throw new ArgumentException("Invalid display type")
        };
    }
}

// ADAPTER PATTERN:
public interface ITarget
{
    void Request();
}

public interface IStationAdaptee
{
    void SpecificRequest();
}

public class StationAdapter : ITarget
{
    private readonly IStationAdaptee adaptee;

    public StationAdapter(IStationAdaptee adaptee)
    {
        this.adaptee = adaptee;
    }

    public void Request()
    {
        adaptee.SpecificRequest();
    }
}

public class DamStation : IStationAdaptee
{
    private float WaterLevel { get; set; }

    public DamStation()
    {
        WaterLevel = 12.5f;
        Console.WriteLine("The Dam Station is operational now.");
    }

    public void RequestWaterLevel()
    {
        Console.WriteLine("Dam Station: Current water level is " + WaterLevel + " meters.");
    }

    public void SpecificRequest()
    {
        RequestWaterLevel();
    }
}

// SINGLETON PATTERN + OBSERVER PATTERN
public class WeatherStation : IStationAdaptee
{
    private static WeatherStation? _instance;
    private static readonly object _lockObject = new object();
    private List<IObserver> _observers = new List<IObserver>();
    private float Temperature { get; set; }

    private WeatherStation()
    {
        Temperature = 20.0f;
        Console.WriteLine("The Weather Station is operational now.");
    }

    public static WeatherStation GetInstance()
    {
        if (_instance is null)
        {
            // Locking for thread-safety:
            //  - two threads both could request instance at the same time
            //  - but only one thread can lock onto the lockObject 
            lock (_lockObject)
            {
                if (_instance is null)
                {
                    _instance = new WeatherStation();
                }
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

    public void NotifyObservers()
    {
        foreach (IObserver observer in _observers)
        {
            observer.Update(Temperature);
        }
    }

    public void SetTemperature(float newTemperature)
    {
        Temperature = newTemperature;
        NotifyObservers();
    }

    public void RequestTemperature()
    {
        Console.WriteLine("Weather Station: Current Temperature is at " + Temperature + " degrees.");
    }

    public void SpecificRequest()
    {
        RequestTemperature();
    }
}
