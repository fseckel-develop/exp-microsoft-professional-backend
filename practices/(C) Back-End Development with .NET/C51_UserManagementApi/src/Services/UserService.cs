using System.Collections.Concurrent;
using System.Threading;
using UserManagementApi.Models;

namespace UserManagementApi.Services;

public sealed class UserService
{
    private readonly ConcurrentDictionary<int, User> _users = new();
    private int _nextId = 0;

    public User Add(User user)
    {
        var id = Interlocked.Increment(ref _nextId);
        user.Id = id;
        _users[id] = user;
        return user;
    }

    public IReadOnlyCollection<User> GetAll()
    {
        return _users.Values
            .OrderBy(u => u.Id)
            .ToList();
    }

    public bool TryGet(int id, out User? user)
    {
        return _users.TryGetValue(id, out user);
    }

    public bool Update(int id, User updatedUser)
    {
        if (!_users.ContainsKey(id))
            return false;

        updatedUser.Id = id;
        _users[id] = updatedUser;
        return true;
    }

    public bool Remove(int id)
    {
        return _users.TryRemove(id, out _);
    }
}