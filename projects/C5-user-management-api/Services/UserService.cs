using System.Collections.Concurrent;
using System.Threading;

// ----------------------
// UserService
// Thread-safe in-memory store for `User` instances used by the C5 exercise.
// Encapsulates storage and basic CRUD operations so controllers or APIs
// don't manipulate the underlying dictionary directly.
// ----------------------
public class UserService
{
    // Concurrent dictionary holds users keyed by id
    private readonly ConcurrentDictionary<int, User> _users = new();
    // Counter for generating incremental ids (Interlocked for thread-safety)
    private int _nextId = 0;

    // ----------------------
    // Add(User)
    // Generates a new id, assigns it to the incoming user and stores it.
    // Returns the created user (with Id populated).
    // ----------------------
    public User Add(User user)
    {
        var id = Interlocked.Increment(ref _nextId);
        user.Id = id;
        _users[id] = user;
        return user;
    }

    // ----------------------
    // GetAll()
    // Returns the collection of all stored users.
    // ----------------------
    public IEnumerable<User> GetAll() => _users.Values;

    // ----------------------
    // TryGet(id, out User)
    // Attempts to retrieve a user by id. Returns true and the user if found.
    // ----------------------
    public bool TryGet(int id, out User? user) => _users.TryGetValue(id, out user);

    // ----------------------
    // Update(id, updatedUser)
    // Replaces the existing user at `id` with `updatedUser` when present.
    // Returns false when the user does not exist.
    // ----------------------
    public bool Update(int id, User updatedUser)
    {
        if (!_users.ContainsKey(id))
            return false;
        updatedUser.Id = id;
        _users[id] = updatedUser;
        return true;
    }

    // ----------------------
    // Remove(id)
    // Removes a user by id. Returns true when a user was removed.
    // ----------------------
    public bool Remove(int id) => _users.TryRemove(id, out _);
}
