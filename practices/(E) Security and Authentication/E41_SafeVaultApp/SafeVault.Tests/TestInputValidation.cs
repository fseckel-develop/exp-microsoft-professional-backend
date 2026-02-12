using NUnit.Framework;
using SafeVault.Web.Services;

namespace SafeVault.Tests
{
    [TestFixture]
    public class TestInputValidation
    {
        [Test]
        public void TestForSQLInjection()
        {
            string malicious = "'; DROP TABLE Users; --";
            string sanitized = InputSanitizer.Sanitize(malicious);

            Assert.That(!sanitized.Contains("DROP"));
            Assert.That(!sanitized.Contains("--"));
            Assert.That(!sanitized.Contains("'"));
        }

        [Test]
        public void TestForXSS()
        {
            string malicious = "<script>alert('XSS');</script>";
            string sanitized = InputSanitizer.Sanitize(malicious);

            Assert.That(!sanitized.Contains("<script>"));
            Assert.That(!sanitized.Contains("</script>"));
            Assert.That(!sanitized.Contains("alert"));
        }
    }
}
