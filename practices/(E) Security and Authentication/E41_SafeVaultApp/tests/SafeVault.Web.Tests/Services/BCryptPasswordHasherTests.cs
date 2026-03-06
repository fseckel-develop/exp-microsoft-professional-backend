using SafeVault.Web.Services;

namespace SafeVault.Web.Tests.Services;

[TestFixture]
public sealed class BCryptPasswordHasherTests
{
    private BCryptPasswordHasher _hasher = null!;

    [SetUp]
    public void SetUp()
    {
        _hasher = new BCryptPasswordHasher();
    }

    [Test]
    public void HashPassword_ShouldReturnDifferentValueThanPlainText()
    {
        var password = "Password123!";
        var hash = _hasher.HashPassword(password);

        Assert.That(hash, Is.Not.EqualTo(password));
        Assert.That(hash, Is.Not.Empty);
    }

    [Test]
    public void VerifyPassword_ShouldReturnTrue_ForMatchingPassword()
    {
        var password = "Password123!";
        var hash = _hasher.HashPassword(password);

        var result = _hasher.VerifyPassword(password, hash);

        Assert.That(result, Is.True);
    }

    [Test]
    public void VerifyPassword_ShouldReturnFalse_ForWrongPassword()
    {
        var hash = _hasher.HashPassword("Password123!");

        var result = _hasher.VerifyPassword("WrongPassword!", hash);

        Assert.That(result, Is.False);
    }
}