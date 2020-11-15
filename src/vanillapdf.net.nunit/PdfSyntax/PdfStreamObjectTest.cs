using NUnit.Framework;
using System;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSyntax
{
    [TestFixture]
    public class PdfStreamObjectTest
    {
        [Test]
        public void TestHeader()
        {
            var StreamObject = PdfStreamObject.Create();
            StreamObject.Header = PdfDictionaryObject.Create();
        }

        [Test]
        public void TestBody()
        {
            var StreamObject = PdfStreamObject.Create();
            StreamObject.Body = PdfBuffer.Create();
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfStreamObject.Create();
            }

            GC.Collect();
        }
    }
}
