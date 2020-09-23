using System;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.test
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
}
