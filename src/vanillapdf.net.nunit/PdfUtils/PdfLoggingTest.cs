using NUnit.Framework;

namespace vanillapdf.net.nunit.PdfUtils
{
    [TestFixture]
    public class PdfLoggingTest
    {
        [Test]
        public void TestInterface()
        {
            PdfLogging.SetSeverity(LoggingSeverity.Debug);
            Assert.AreEqual(LoggingSeverity.Debug, PdfLogging.GetSeverity());

            PdfLogging.Enable();
            Assert.AreEqual(true, PdfLogging.IsEnabled());

            PdfLogging.Disable();
            Assert.AreEqual(false, PdfLogging.IsEnabled());
        }
    }
}
