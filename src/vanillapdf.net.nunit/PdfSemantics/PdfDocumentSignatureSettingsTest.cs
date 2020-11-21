using NUnit.Framework;
using System;
using vanillapdf.net.PdfSemantics;

namespace vanillapdf.net.nunit.PdfSemantics
{
    [TestFixture]
    public class PdfDocumentSignatureSettingsTest
    {
        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfDocumentSignatureSettings.Create();
            }

            GC.Collect();
        }
    }
}
