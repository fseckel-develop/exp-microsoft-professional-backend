namespace ApiDependencyInjectionDemo.Services;

public sealed class RequestAuditService : IRequestAuditService
{
    private readonly List<string> _records = [];
    public string InstanceId { get; } = Guid.NewGuid().ToString();

    public RequestAuditService()
    {
        Console.WriteLine($"RequestAuditService created with ID: {InstanceId}");
    }

    public void Record(string message)
    {
        var entry = $"[{InstanceId}] {message}";
        _records.Add(entry);
        Console.WriteLine(entry);
    }

    public IReadOnlyList<string> GetRecords()
    {
        return _records.AsReadOnly();
    }
}