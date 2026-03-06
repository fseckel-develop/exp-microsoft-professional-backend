using SafeVault.Web.Models;

namespace SafeVault.Web.Data;

public interface IUserRepository
{
    Task<bool> UserExistsAsync(string username, CancellationToken ct = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default);
    Task<User> InsertUserAsync(User user, CancellationToken ct = default);
}