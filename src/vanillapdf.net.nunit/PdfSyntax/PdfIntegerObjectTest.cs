using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.PdfSyntax
{
    [TestFixture]
    public class PdfIntegerObjectTest
    {
        [Test]
        public void GettersSetters()
        {
            const long TEST_VALUE_SIGNED = long.MinValue;
            const ulong TEST_VALUE_UNSIGNED = ulong.MaxValue;

            var IntegerObject = PdfIntegerObject.Create();

            IntegerObject.IntegerValue = TEST_VALUE_SIGNED;
            ClassicAssert.AreEqual(TEST_VALUE_SIGNED, IntegerObject.IntegerValue);

            IntegerObject.UnsignedIntegerValue = TEST_VALUE_UNSIGNED;
            ClassicAssert.AreEqual(TEST_VALUE_UNSIGNED, IntegerObject.UnsignedIntegerValue);
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfIntegerObject.Create();
            }

            GC.Collect();
        }

        [Test]
        public void TryFromObject_WithRealObject_ReturnsInteger()
        {
            // PDF spec allows integers and reals to be used interchangeably
            // e.g., MediaBox can contain [0 0 612.0 792.0] where 612.0 is a Real
            using var realObject = PdfRealObject.Create();
            realObject.Value = 612.0;

            using var integerFromReal = PdfIntegerObject.TryFromObject(realObject);

            ClassicAssert.IsNotNull(integerFromReal, "TryFromObject should accept Real objects");
            ClassicAssert.AreEqual(612, integerFromReal.IntegerValue);
        }

        [Test]
        public void TryFromObject_WithRealObjectFractional_TruncatesValue()
        {
            using var realObject = PdfRealObject.Create();
            realObject.Value = 612.7;

            using var integerFromReal = PdfIntegerObject.TryFromObject(realObject);

            ClassicAssert.IsNotNull(integerFromReal, "TryFromObject should accept Real objects");
            ClassicAssert.AreEqual(612, integerFromReal.IntegerValue);
        }
    }
}
