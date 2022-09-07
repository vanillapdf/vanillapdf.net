using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using vanillapdf.net.Utils;

namespace vanillapdf.net.nunit
{
    [SetUpFixture]
    public class OneTimeSetup
    {
        public const int STABILITY_REPEAT_COUNT = 1000;
        public static readonly UInt64 PLATFORM_MAXIMUM_VALUE;

        public static readonly List<string> TEST_DOCUMENTS = new List<string>() {
            Path.Combine("Resources", "minimalist.pdf"),
            Path.Combine("Resources", "19005-1_FAQ.PDF")
        };

        static OneTimeSetup()
        {
            if (Environment.Is64BitProcess) {
                PLATFORM_MAXIMUM_VALUE = UInt64.MaxValue;
            } else {
                PLATFORM_MAXIMUM_VALUE = UInt32.MaxValue;
            }
        }

        [OneTimeSetUp]
        public static void InitializeLibrary()
        {
            LibraryInstance.Initialize(TestContext.CurrentContext.TestDirectory);
        }
    }
}
