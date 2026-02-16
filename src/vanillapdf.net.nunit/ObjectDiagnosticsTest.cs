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

        [Test]
        public void GetTotalObjectsCreated_ReturnsPositive()
        {
            using var obj = PdfNameObject.CreateFromDecodedString("Test");
            var total = ObjectDiagnostics.GetTotalObjectsCreated();

            ClassicAssert.Greater(total, 0);
        }

        [Test]
        public void GetTotalObjectsCreated_IncreasesAfterCreatingObject()
        {
            var before = ObjectDiagnostics.GetTotalObjectsCreated();
            using var obj = PdfNameObject.CreateFromDecodedString("Test");
            var after = ObjectDiagnostics.GetTotalObjectsCreated();

            ClassicAssert.Greater(after, before);
        }

        [Test]
        public void ResetCounters_ResetsPeakAndTotal()
        {
            using var obj = PdfNameObject.CreateFromDecodedString("Test");
            ObjectDiagnostics.ResetCounters();

            var active = ObjectDiagnostics.GetActiveObjectCount();
            var peak = ObjectDiagnostics.GetPeakObjectCount();
            var total = ObjectDiagnostics.GetTotalObjectsCreated();

            ClassicAssert.AreEqual(active, peak);
            ClassicAssert.AreEqual(active, total);
        }
    }
}
