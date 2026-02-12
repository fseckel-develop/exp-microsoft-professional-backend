using NUnit.Framework;
using SafeVault.Web.Storage;

namespace SafeVault.Tests
{
    [TestFixture]
    public class TestDatabaseSecurity
    {
        [Test]
        public void TestParameterizedQueryBlocksInjection()
        {
            string malicious = "' OR '1'='1";

            bool exists = Database.UserExists(malicious);

            Assert.That(!exists);
        }
    }
}
