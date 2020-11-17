using NUnit.Framework;
using System;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSyntax
{
    [TestFixture]
    public class PdfNameObjectTest
    {
        [Test]
        public void GettersSetters()
        {
            const string TEST_VALUE = "TEST_VALUE";

            var Buffer = PdfBuffer.Create();
            Buffer.StringData = TEST_VALUE;

            var NameObject = PdfNameObject.Create();

            NameObject.Value = Buffer;

            var checkBuffer = NameObject.Value;

            Assert.AreEqual(TEST_VALUE, checkBuffer.StringData);
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfNameObject.Create();
            }

            GC.Collect();
        }
    }
}
