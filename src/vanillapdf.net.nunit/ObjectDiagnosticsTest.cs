using NUnit.Framework;
using NUnit.Framework.Legacy;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;

namespace vanillapdf.net.nunit
{
    /// <summary>
    /// Tests for ObjectDiagnostics API.
    /// Verifies native-side IUnknown object counting.
    /// </summary>
    [TestFixture]
    public class ObjectDiagnosticsTest
    {
        [Test]
        public void GetActiveObjectCount_ReturnsNonNegative()
        {
            var count = ObjectDiagnostics.GetActiveObjectCount();
            ClassicAssert.GreaterOrEqual(count, 0);
        }

        [Test]
        public void GetActiveObjectCount_IncreasesAfterCreatingObject()
        {
            var before = ObjectDiagnostics.GetActiveObjectCount();
            using var obj = PdfNameObject.CreateFromDecodedString("Test");
            var after = ObjectDiagnostics.GetActiveObjectCount();

            ClassicAssert.Greater(after, before);
        }

        [Test]
        public void GetPeakObjectCount_GreaterOrEqualToActiveCount()
        {
            using var obj = PdfNameObject.CreateFromDecodedString("Test");
            var active = ObjectDiagnostics.GetActiveObjectCount();
            var peak = ObjectDiagnostics.GetPeakObjectCount();

            ClassicAssert.GreaterOrEqual(peak, active);
        }
    }
}
