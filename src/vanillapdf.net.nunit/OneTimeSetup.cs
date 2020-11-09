using NUnit.Framework;
using System.Collections.Generic;
using vanillapdf.net.Utils;

namespace vanillapdf.net.nunit
{
    [SetUpFixture]
    public class OneTimeSetup
    {
        public const int STABILITY_REPEAT_COUNT = 1000;

        public static readonly List<string> TEST_DOCUMENTS = new List<string>() {
            "Resources\\minimalist.pdf",
            "Resources\\19005-1_FAQ.PDF"
        };

        [OneTimeSetUp]
        public static void InitializeLibrary()
        {
            LibraryInstance.Initialize(TestContext.CurrentContext.TestDirectory);
        }
    }
}
