using NUnit.Framework;

namespace SafeVault.Tests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            TestDatabaseSetup.Initialize();
        }
    }
}