using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Text;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    [TestFixture]
    public class TextStringEncodingTest
    {
        [Test]
        public void TestEncodingTypeEnumValues()
        {
            var values = Enum.GetValues(typeof(PdfTextStringEncodingType));
            ClassicAssert.AreEqual(4, values.Length, "PdfTextStringEncodingType should have 4 values");

            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfTextStringEncodingType), PdfTextStringEncodingType.Undefined));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfTextStringEncodingType), PdfTextStringEncodingType.PDFDocEncoding));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfTextStringEncodingType), PdfTextStringEncodingType.UTF16BE));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfTextStringEncodingType), PdfTextStringEncodingType.UTF8));
        }

        [Test]
        public void TestDetectUtf8()
        {
            byte[] data = { 0xEF, 0xBB, 0xBF, 0x48, 0x65, 0x6C, 0x6C, 0x6F };
            ClassicAssert.AreEqual(PdfTextStringEncodingType.UTF8, PdfTextStringEncoding.Detect(data));
        }

        [Test]
        public void TestDetectUtf16BigEndian()
        {
            byte[] data = { 0xFE, 0xFF, 0x00, 0x48, 0x00, 0x69 };
            ClassicAssert.AreEqual(PdfTextStringEncodingType.UTF16BE, PdfTextStringEncoding.Detect(data));
        }

        [Test]
        public void TestDetectPdfDocEncoding()
        {
            byte[] data = Encoding.ASCII.GetBytes("Hello world");
            ClassicAssert.AreEqual(PdfTextStringEncodingType.PDFDocEncoding, PdfTextStringEncoding.Detect(data));
        }

        [Test]
        public void TestDetectNullThrows()
        {
            Assert.Throws<ArgumentNullException>(DetectNull);
        }

        [Test]
        public void TestPdfDocEncodingAsciiByteMapsToItself()
        {
            ClassicAssert.AreEqual(0x41u, PdfTextStringEncoding.PDFDocEncodingByteToUnicode(0x41));
            ClassicAssert.AreEqual(0x7Au, PdfTextStringEncoding.PDFDocEncodingByteToUnicode(0x7A));
        }

        [Test]
        public void TestPdfDocEncodingEuroSign()
        {
            // PDF spec Table D.2: PDFDocEncoding 0xA0 is the euro sign
            ClassicAssert.AreEqual(0x20ACu, PdfTextStringEncoding.PDFDocEncodingByteToUnicode(0xA0));
        }

        private static void DetectNull()
        {
            PdfTextStringEncoding.Detect(null);
        }
    }
}
