using NUnit.Framework;
using NUnit.Framework.Legacy;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    [TestFixture]
    public class PdfLoggingTest
    {
        [Test]
        public void TestInterface()
        {
            PdfLogging.Severity = PdfLoggingSeverity.Debug;
            ClassicAssert.AreEqual(PdfLoggingSeverity.Debug, PdfLogging.Severity);
        }
    }
}
