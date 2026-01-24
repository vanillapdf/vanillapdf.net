using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.PdfSyntax
{
    [TestFixture]
    public class PdfRealObjectTest
    {
        [Test]
        public void GettersSetters()
        {
            const double TEST_VALUE_MIN = double.MinValue;
            const double TEST_VALUE_MAX = double.MaxValue;

            var RealObject = PdfRealObject.Create();

            RealObject.Value = TEST_VALUE_MIN;
            ClassicAssert.AreEqual(TEST_VALUE_MIN, RealObject.Value);

            RealObject.Value = TEST_VALUE_MAX;
            ClassicAssert.AreEqual(TEST_VALUE_MAX, RealObject.Value);
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfRealObject.Create();
            }

            GC.Collect();
        }

        [Test]
        public void TryFromObject_WithIntegerObject_ReturnsReal()
        {
            // PDF spec allows integers and reals to be used interchangeably
            // e.g., MediaBox can contain [0 0 612 792] where 612 is an Integer
            using var integerObject = PdfIntegerObject.Create();
            integerObject.IntegerValue = 612;

            using var realFromInteger = PdfRealObject.TryFromObject(integerObject);

            ClassicAssert.IsNotNull(realFromInteger, "TryFromObject should accept Integer objects");
            ClassicAssert.AreEqual(612.0, realFromInteger.Value);
        }
    }
}
