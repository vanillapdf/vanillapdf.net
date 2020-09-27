using NUnit.Framework;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.Utils
{
    [TestFixture]
    public class PdfLiteralStringObjectTest
    {
        private PdfLiteralStringObject StringObject { get; set; }

        [SetUp]
        public void Setup()
        {
            StringObject = PdfLiteralStringObject.Create();
            Assert.NotNull(StringObject);
        }

        //[Test]
        //public void EncodedValue()
        //{
        //    StringObject = PdfLiteralStringObject.Create();
        //    Assert.NotNull(StringObject);
        //}

        //[Test]
        //public void DecodedValue()
        //{
        //    string value = "TEST_VALUE";

        //    StringObject = PdfLiteralStringObject.CreateFromDecodedString(value);

        //    PdfBuffer check = StringObject.GetValue();

        //    Assert.Equals(value, checkValue);
        //}
    }
}
