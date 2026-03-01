namespace DesignPatternsDemo.Adapter;

public sealed class StationAdapter : IMonitorTarget
{
    private readonly IStationSource _source;

    public StationAdapter(IStationSource source)
    {
        _source = source;
    }

    public void ShowStatus()
    {
        _source.OutputStationReading();
    }
}