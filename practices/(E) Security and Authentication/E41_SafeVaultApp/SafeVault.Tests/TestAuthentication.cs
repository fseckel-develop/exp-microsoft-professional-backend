using SafeVault.Web.Services;
using SafeVault.Web.Storage;

namespace SafeVault.Tests
{
    [TestFixture]
    public class TestAuthentication
    {
        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            _authService = new AuthService();
        }

        [Test]
        public void Login_WithValidCredentials_ShouldSucceed()
        {
            // Arrange
            string username = "testuser";
            string password = "SecurePass123!";
            string hash = PasswordHasher.HashPassword(password);

            Database.InsertUser(username, "test@example.com", hash, "User");

            // Act
            bool result = _authService.Authenticate(username, password);

            // Assert
            Assert.That(result, "Expected login to succeed with valid credentials.");
        }

        [Test]
        public void Login_WithInvalidPassword_ShouldFail()
        {
            // Arrange
            string username = "wrongpassuser";
            string correctPassword = "Correct123!";
            string wrongPassword = "Wrong123!";
            string hash = PasswordHasher.HashPassword(correctPassword);

            Database.InsertUser(username, "wrong@example.com", hash, "User");

            // Act
            bool result = _authService.Authenticate(username, wrongPassword);

            // Assert
            Assert.That(!result, "Expected login to fail with incorrect password.");
        }

        [Test]
        public void Login_WithNonexistentUser_ShouldFail()
        {
            // Act
            bool result = _authService.Authenticate("ghostuser", "anything");

            // Assert
            Assert.That(!result, "Expected login to fail for nonexistent user.");
        }
    }
}
