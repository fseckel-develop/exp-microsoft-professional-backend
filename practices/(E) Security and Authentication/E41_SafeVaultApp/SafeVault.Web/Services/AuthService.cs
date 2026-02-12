using SafeVault.Web.Storage;

namespace SafeVault.Web.Services
{
    public class AuthService
    {
        public bool Authenticate(string username, string password)
        {
            string? storedHash = Database.GetPasswordHash(username);

            if (storedHash == null)
                return false;

            return PasswordHasher.VerifyPassword(password, storedHash);
        }

        public bool UserHasRole(string username, string requiredRole)
        {
            string? role = Database.GetUserRole(username);

            if (role == null)
                return false;

            return role.Equals(requiredRole, StringComparison.OrdinalIgnoreCase);
        }
    }
}
