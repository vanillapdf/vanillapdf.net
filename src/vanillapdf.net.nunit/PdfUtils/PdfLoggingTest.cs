using NUnit.Framework;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    [TestFixture]
    public class PdfLoggingTest
    {
        [Test]
        public void TestInterface()
        {
            PdfLogging.SetSeverity(PdfLoggingSeverity.Debug);
            Assert.AreEqual(PdfLoggingSeverity.Debug, PdfLogging.GetSeverity());

            PdfLogging.Enable();
            Assert.AreEqual(true, PdfLogging.IsEnabled());

            PdfLogging.Disable();
            Assert.AreEqual(false, PdfLogging.IsEnabled());
        }
    }
}
