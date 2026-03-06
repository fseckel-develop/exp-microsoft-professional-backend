namespace CatalogApi.Infrastructure;

public sealed class InstanceContext
{
    public InstanceContext(string instanceName)
    {
        InstanceName = instanceName;
    }

    public string InstanceName { get; }
    public string MachineName => Environment.MachineName;
    public int ProcessId => Environment.ProcessId;
    public DateTime UtcNow => DateTime.UtcNow;
}