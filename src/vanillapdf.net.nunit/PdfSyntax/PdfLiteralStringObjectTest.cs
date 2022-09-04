using NUnit.Framework;
using System;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.PdfSyntax
{
    [TestFixture]
    public class PdfLiteralStringObjectTest
    {
        [Test]
        public void EncodedValue()
        {
            const string ENCODED_VALUE = "TEST_VALUE \\n \\r \\t \\b \\f \\( \\) \\\\ \\007";
            //const string DECODED_VALUE = "TEST_VALUE \n \r \t \b \f ( ) \\ \x07";

            var StringObject = PdfLiteralStringObject.CreateFromEncodedString(ENCODED_VALUE);

            //PdfBuffer check = StringObject.GetValue();
            //var checkValue = check.GetDataString();

            // TODO: Fix encoded string initialization
            //Assert.AreEqual(DECODED_VALUE, checkValue);
        }

        [Test]
        public void DecodedValue()
        {
            const string DECODED_VALUE = "TEST_VALUE \n \r \t \b \f ( ) \\ \x07";

            var StringObject = PdfLiteralStringObject.CreateFromDecodedString(DECODED_VALUE);

            Assert.AreEqual(DECODED_VALUE, StringObject.Value.StringData);
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfLiteralStringObject.Create();
            }

            GC.Collect();
        }
    }
}
