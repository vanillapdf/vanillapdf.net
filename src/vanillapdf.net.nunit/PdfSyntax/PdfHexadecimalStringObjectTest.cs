using NUnit.Framework;
using System;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSyntax
{
    [TestFixture]
    public class PdfHexadecimalStringObjectTest
    {
        [Test]
        public void EncodedValue()
        {
            const string ENCODED_VALUE = "544553545f56414C5545";
            const string DECODED_VALUE = "TEST_VALUE";

            var StringObject = PdfHexadecimalStringObject.CreateFromEncodedString(ENCODED_VALUE);

            Assert.AreEqual(DECODED_VALUE, StringObject.Value.StringData);
        }

        [Test]
        public void DecodedValue()
        {
            const string DECODED_VALUE = "TEST_VALUE";

            var StringObject = PdfHexadecimalStringObject.CreateFromDecodedString(DECODED_VALUE);

            Assert.AreEqual(DECODED_VALUE, StringObject.Value.StringData);
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfHexadecimalStringObject.Create();
            }

            GC.Collect();
        }
    }
}
