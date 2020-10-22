using NUnit.Framework;
using System;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.Utils
{
    [TestFixture]
    public class PdfIndirectReferenceObjectTest
    {
        [Test]
        public void TestDirectObject()
        {
            var IndirectReferenceObject = PdfIndirectReferenceObject.Create();

            try {
                // Cannot point to direct object
                IndirectReferenceObject.ReferencedObject = PdfIntegerObject.Create();
            } catch (Exception ex) {
                Assert.IsTrue(ex is PdfGeneralException);
            }
        }

        [Test]
        public void TestStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfIndirectReferenceObject.Create();
            }

            GC.Collect();
        }
    }
}
