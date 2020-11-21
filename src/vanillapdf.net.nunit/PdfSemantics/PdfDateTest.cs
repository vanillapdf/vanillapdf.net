using NUnit.Framework;
using System;
using vanillapdf.net.PdfSemantics;

namespace vanillapdf.net.nunit.PdfSemantics
{
    [TestFixture]
    public class PdfDateTest
    {
        [Test]
        public void TestStabilityEmpty()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfDate.CreateEmpty();
            }

            GC.Collect();
        }

        [Test]
        public void TestStabilityCurrent()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfDate.CreateCurrent();
            }

            GC.Collect();
        }
    }
}
