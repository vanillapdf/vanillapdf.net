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
    }
}
