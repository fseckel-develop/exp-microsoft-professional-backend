using LinearDataSupportTicketApi.Models;

namespace LinearDataSupportTicketApi.Services;

public sealed class TicketUndoService
{
    private readonly Stack<TicketAction> _actions = new();
    private readonly object _sync = new();

    public void Push(TicketAction action)
    {
        lock (_sync)
        {
            _actions.Push(action);
        }
    }

    public TicketAction? Pop()
    {
        lock (_sync)
        {
            return _actions.Count > 0
                ? _actions.Pop()
                : null;
        }
    }

    public int Count
    {
        get
        {
            lock (_sync)
            {
                return _actions.Count;
            }
        }
    }
}