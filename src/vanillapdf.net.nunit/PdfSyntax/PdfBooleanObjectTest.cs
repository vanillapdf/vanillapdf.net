using NUnit.Framework;
using System;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.Utils
{
    [TestFixture]
    public class PdfBooleanObjectTest
    {
        [Test]
        public void GettersSetters()
        {
            const bool TEST_VALUE_POSITIVE = true;
            const bool TEST_VALUE_NEGATIVE = false;

            var BooleanObject = PdfBooleanObject.Create();

            BooleanObject.Value = TEST_VALUE_POSITIVE;
            Assert.AreEqual(TEST_VALUE_POSITIVE, BooleanObject.Value);

            BooleanObject.Value = TEST_VALUE_NEGATIVE;
            Assert.AreEqual(TEST_VALUE_NEGATIVE, BooleanObject.Value);
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfBooleanObject.Create();
            }

            GC.Collect();
        }
    }
}
