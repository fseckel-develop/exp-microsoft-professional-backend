using LinearDataSupportTicketApi.Models;

namespace LinearDataSupportTicketApi.Services;

public sealed class SupportLaneService
{
    // Array demonstration
    private readonly SupportLane[] _lanes =
    [
        new("General Inquiry"),
        new("Billing"),
        new("Technical Support"),
        new("Account Access")
    ];

    public IReadOnlyList<SupportLane> GetAll() => _lanes;

    public bool Exists(string laneName)
    {
        return _lanes.Any(x => string.Equals(x.Name, laneName, StringComparison.OrdinalIgnoreCase));
    }
}