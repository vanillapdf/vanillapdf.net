using NUnit.Framework;
using System;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.Utils
{
    [TestFixture]
    public class PdfNullObjectTest
    {
        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfNullObject.Create();
            }

            GC.Collect();
        }
    }
}
