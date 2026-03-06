using Microsoft.AspNetCore.Authorization;
using SafeVault.Web.Controllers;

namespace SafeVault.Web.Tests.Controllers;

[TestFixture]
public sealed class AuthorizationAttributeTests
{
    [Test]
    public void AdminController_ShouldRequireAdminRole()
    {
        var attribute = typeof(AdminController)
            .GetCustomAttributes(typeof(AuthorizeAttribute), inherit: true)
            .Cast<AuthorizeAttribute>()
            .FirstOrDefault();

        Assert.That(attribute, Is.Not.Null);
        Assert.That(attribute!.Roles, Is.EqualTo("Admin"));
    }
}