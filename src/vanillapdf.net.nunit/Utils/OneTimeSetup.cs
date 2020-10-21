using NUnit.Framework;
using vanillapdf.net.Utils;

namespace vanillapdf.net.nunit.Utils
{
    [SetUpFixture]
    public class OneTimeSetup
    {
        public const int STABILITY_REPEAT_COUNT = 1000;

        [OneTimeSetUp]
        public static void InitializeLibrary()
        {
            LibraryInstance.Initialize(TestContext.CurrentContext.TestDirectory);
        }
    }
}
