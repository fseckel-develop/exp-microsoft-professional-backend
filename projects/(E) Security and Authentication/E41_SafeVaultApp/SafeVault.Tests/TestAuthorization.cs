using NUnit.Framework;
using SafeVault.Web.Services;
using SafeVault.Web.Storage;

namespace SafeVault.Tests
{
    [TestFixture]
    public class TestAuthorization
    {
        private AuthService _authService = null!;

        [SetUp]
        public void Setup()
        {
            _authService = new AuthService();
        }

        [Test]
        public void AdminUser_ShouldHaveAccess()
        {
            // Arrange
            string username = "adminUser";
            string password = "AdminPass!";
            string hash = PasswordHasher.HashPassword(password);

            Database.InsertUser(username, "admin@example.com", hash, "Admin");

            // Act
            bool hasAccess = _authService.UserHasRole(username, "Admin");

            // Assert
            Assert.That(hasAccess, "Admin user should have admin access.");
        }

        [Test]
        public void RegularUser_ShouldNotHaveAdminAccess()
        {
            // Arrange
            string username = "normalUser";
            string password = "UserPass!";
            string hash = PasswordHasher.HashPassword(password);

            Database.InsertUser(username, "user@example.com", hash, "User");

            // Act
            bool hasAccess = _authService.UserHasRole(username, "Admin");

            // Assert
            Assert.That(!hasAccess, "Regular user should NOT have admin access.");
        }

        [Test]
        public void UnknownUser_ShouldNotHaveAccess()
        {
            // Act
            bool hasAccess = _authService.UserHasRole("ghost", "Admin");

            // Assert
            Assert.That(!hasAccess, "Unknown user should not have any role access.");
        }
    }
}
