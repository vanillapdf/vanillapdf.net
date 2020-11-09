using NUnit.Framework;
using System;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    [TestFixture]
    public class PdfSigningKeyTest
    {
        public class PdfCustomSigningContext : PdfSigningContext
        {
            public override uint Initialize(PdfMessageDigestAlgorithmType digest)
            {
                return PdfReturnValues.ERROR_SUCCESS;
            }

            public override uint Update(PdfBuffer buffer)
            {
                return PdfReturnValues.ERROR_SUCCESS;
            }

            public override uint Final(out PdfBuffer buffer)
            {
                buffer = PdfBuffer.Create();
                return PdfReturnValues.ERROR_SUCCESS;
            }

            public override void Cleanup()
            {
            }
        }

        [Test]
        public void TestLicensing()
        {
            try {
                PdfSigningKey.CreateCustom(new PdfCustomSigningContext());
            }
            catch (Exception ex) {
                Assert.IsTrue(ex is PdfLicenseRequiredException);
            }
        }
    }
}
