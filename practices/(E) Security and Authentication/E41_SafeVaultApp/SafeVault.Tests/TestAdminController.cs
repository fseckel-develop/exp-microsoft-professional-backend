using Microsoft.AspNetCore.Mvc;
using SafeVault.Web.Controllers;
using SafeVault.Web.Storage;
using SafeVault.Web.Services;

namespace SafeVault.Tests
{
    [TestFixture]
    public class TestAdminController
    {
        [Test]
        public void AdminUser_ShouldAccessDashboard()
        {
            // Arrange
            string username = "adminTest";
            string password = "Admin123!";
            string hash = PasswordHasher.HashPassword(password);

            Database.InsertUser(username, "admin@test.com", hash, "Admin");

            var controller = new AdminController();

            // Act
            var result = controller.Dashboard(username) as OkObjectResult;

            // Assert
            Assert.That(result is not null);
            Assert.That(200 == result!.StatusCode);
        }

        [Test]
        public void RegularUser_ShouldBeDenied()
        {
            // Arrange
            string username = "userTest";
            string password = "User123!";
            string hash = PasswordHasher.HashPassword(password);

            Database.InsertUser(username, "user@test.com", hash, "User");

            var controller = new AdminController();

            // Act
            var result = controller.Dashboard(username) as UnauthorizedObjectResult;

            // Assert
            Assert.That(result is not null);
            Assert.That(401 == result!.StatusCode);
        }
    }
}
