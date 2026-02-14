using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSyntax
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
                ClassicAssert.IsTrue(ex is PdfUnmanagedException);
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
