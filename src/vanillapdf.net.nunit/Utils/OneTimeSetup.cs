using NUnit.Framework;
using vanillapdf.net.Utils;

namespace vanillapdf.net.nunit.Utils
{
    [SetUpFixture]
    public class OneTimeSetup
    {
        [OneTimeSetUp]
        public static void InitializeLibrary()
        {
            LibraryInstance.Initialize(TestContext.CurrentContext.TestDirectory);
        }
    }
}
