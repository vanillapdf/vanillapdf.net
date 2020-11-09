using NUnit.Framework;
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
            Assert.AreEqual(TEST_VALUE_SIGNED, IntegerObject.IntegerValue);

            IntegerObject.UnsignedIntegerValue = TEST_VALUE_UNSIGNED;
            Assert.AreEqual(TEST_VALUE_UNSIGNED, IntegerObject.UnsignedIntegerValue);
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfIntegerObject.Create();
            }

            GC.Collect();
        }
    }
}
