using NUnit.Framework;
using System;
using vanillapdf.net.PdfContents;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfContents
{
    [TestFixture]
    public class PdfBaseFontRangeTest
    {
        [Test]
        public void TestStabilityEmpty()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfBaseFontRange.Create();
            }

            GC.Collect();
        }

        [Test]
        public void TestBasic()
        {
            using PdfBaseFontRange fontRange = PdfBaseFontRange.Create();

            using PdfHexadecimalStringObject rangeLow = PdfHexadecimalStringObject.CreateFromEncodedString("0001");
            using PdfHexadecimalStringObject rangeHigh = PdfHexadecimalStringObject.CreateFromEncodedString("0002");
            using PdfHexadecimalStringObject rangeDestination = PdfHexadecimalStringObject.CreateFromEncodedString("00FF");

            fontRange.RangeLow = rangeLow;
            fontRange.RangeHigh = rangeHigh;
            fontRange.Destination = rangeDestination;

            using var key1 = PdfBuffer.Create();
            using var check1 = PdfBuffer.Create();

            key1.Data = new byte[] { 0x00, 0x01 };
            check1.Data = new byte[] { 0x00, 0xFF };

            using var map1 = fontRange.GetMappedValue(key1);

            Assert.AreEqual(map1, check1);

            using var key2 = PdfBuffer.Create();
            using var check2 = PdfBuffer.Create();

            key2.Data = new byte[] { 0x00, 0x02 };
            check2.Data = new byte[] { 0x01, 0x00 };

            using var map2 = fontRange.GetMappedValue(key2);

            Assert.AreEqual(map2, check2);
        }
    }
}
