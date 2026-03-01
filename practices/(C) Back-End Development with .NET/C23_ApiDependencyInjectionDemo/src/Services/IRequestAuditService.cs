namespace ApiDependencyInjectionDemo.Services;

public interface IRequestAuditService
{
    string InstanceId { get; }
    void Record(string message);
    IReadOnlyList<string> GetRecords();
}