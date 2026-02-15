using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using vanillapdf.net.PdfSemantics;

namespace vanillapdf.net.nunit.PdfSemantics
{
    [TestFixture]
    public class PdfColorTest
    {
        [Test]
        public void TestCreateTransparent()
        {
            using var color = PdfColor.CreateTransparent();
            ClassicAssert.IsNotNull(color);
            ClassicAssert.AreEqual(PdfColorSpaceType.Transparent, color.ColorSpace);
        }

        [Test]
        public void TestCreateGray()
        {
            using var color = PdfColor.CreateGray(0.5);
            ClassicAssert.IsNotNull(color);
            ClassicAssert.AreEqual(PdfColorSpaceType.DeviceGray, color.ColorSpace);
            ClassicAssert.AreEqual(0.5, color.Gray, 0.001);
        }

        [Test]
        public void TestCreateRGB()
        {
            using var color = PdfColor.CreateRGB(0.1, 0.2, 0.3);
            ClassicAssert.IsNotNull(color);
            ClassicAssert.AreEqual(PdfColorSpaceType.DeviceRGB, color.ColorSpace);
            ClassicAssert.AreEqual(0.1, color.Red, 0.001);
            ClassicAssert.AreEqual(0.2, color.Green, 0.001);
            ClassicAssert.AreEqual(0.3, color.Blue, 0.001);
        }

        [Test]
        public void TestCreateCMYK()
        {
            using var color = PdfColor.CreateCMYK(0.1, 0.2, 0.3, 0.4);
            ClassicAssert.IsNotNull(color);
            ClassicAssert.AreEqual(PdfColorSpaceType.DeviceCMYK, color.ColorSpace);
            ClassicAssert.AreEqual(0.1, color.Cyan, 0.001);
            ClassicAssert.AreEqual(0.2, color.Magenta, 0.001);
            ClassicAssert.AreEqual(0.3, color.Yellow, 0.001);
            ClassicAssert.AreEqual(0.4, color.Black, 0.001);
        }

        [Test]
        public void TestStabilityCreateTransparent()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfColor.CreateTransparent();
            }

            GC.Collect();
        }

        [Test]
        public void TestStabilityCreateRGB()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfColor.CreateRGB(1.0, 0.0, 0.0);
            }

            GC.Collect();
        }
    }
}
