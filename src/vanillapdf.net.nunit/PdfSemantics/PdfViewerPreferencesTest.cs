using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.IO;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSemantics
{
    [TestFixture]
    public class PdfViewerPreferencesTest
    {
        // ViewerPreferences << /CenterWindow true >>
        private const string CenterWindowPdf = "20131231103232738561744.pdf";

        // Empty ViewerPreferences << >>
        private const string EmptyPrefsPdf = "Granizo.pdf";

        // /PageLayout /OneColumn
        private const string PageLayoutPdf = "19005-1_FAQ.PDF";

        #region PageLayout

        [Test]
        public void TestGetPageLayoutOneColumn()
        {
            using var sourceStream = PdfInputOutputStream.CreateFromFile(Path.Combine("Resources", PageLayoutPdf));
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();

            ClassicAssert.AreEqual(PdfPageLayoutType.OneColumn, catalog.GetPageLayout());
        }

        [Test]
        public void TestGetPageLayoutAbsentReturnsNull()
        {
            using var sourceStream = PdfInputOutputStream.CreateFromFile(Path.Combine("Resources", CenterWindowPdf));
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();

            ClassicAssert.IsNull(catalog.GetPageLayout());
        }

        [Test]
        public void TestPageLayoutEnumValues()
        {
            ClassicAssert.AreEqual(0, (int)PdfPageLayoutType.Undefined);
            ClassicAssert.AreEqual(1, (int)PdfPageLayoutType.SinglePage);
            ClassicAssert.AreEqual(2, (int)PdfPageLayoutType.OneColumn);
            ClassicAssert.AreEqual(3, (int)PdfPageLayoutType.TwoColumnLeft);
            ClassicAssert.AreEqual(4, (int)PdfPageLayoutType.TwoColumnRight);
            ClassicAssert.AreEqual(5, (int)PdfPageLayoutType.TwoPageLeft);
            ClassicAssert.AreEqual(6, (int)PdfPageLayoutType.TwoPageRight);
        }

        #endregion

        #region ViewerPreferences

        [Test]
        public void TestGetViewerPreferencesReturnsObject()
        {
            using var sourceStream = PdfInputOutputStream.CreateFromFile(Path.Combine("Resources", CenterWindowPdf));
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();

            using var preferences = catalog.GetViewerPreferences();
            ClassicAssert.IsNotNull(preferences);
        }

        [Test]
        public void TestViewerPreferencesCenterWindow()
        {
            using var sourceStream = PdfInputOutputStream.CreateFromFile(Path.Combine("Resources", CenterWindowPdf));
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();

            using var preferences = catalog.GetViewerPreferences();
            ClassicAssert.IsNotNull(preferences);

            using var centerWindow = preferences.CenterWindow;
            ClassicAssert.IsNotNull(centerWindow);
            ClassicAssert.IsTrue(centerWindow.Value);
        }

        [Test]
        public void TestViewerPreferencesAbsentEntriesReturnNull()
        {
            using var sourceStream = PdfInputOutputStream.CreateFromFile(Path.Combine("Resources", CenterWindowPdf));
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();

            using var preferences = catalog.GetViewerPreferences();
            ClassicAssert.IsNotNull(preferences);

            // Only CenterWindow is present in this document.
            ClassicAssert.IsNull(preferences.HideToolbar);
            ClassicAssert.IsNull(preferences.FitWindow);
            ClassicAssert.IsNull(preferences.PrintScaling);
            ClassicAssert.IsNull(preferences.NumCopies);
        }

        [Test]
        public void TestViewerPreferencesEmptyDictionary()
        {
            using var sourceStream = PdfInputOutputStream.CreateFromFile(Path.Combine("Resources", EmptyPrefsPdf));
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();

            using var preferences = catalog.GetViewerPreferences();
            ClassicAssert.IsNotNull(preferences);

            // Empty dictionary: every entry is absent.
            ClassicAssert.IsNull(preferences.CenterWindow);
            ClassicAssert.IsNull(preferences.HideToolbar);
            ClassicAssert.IsNull(preferences.Direction);
            ClassicAssert.IsNull(preferences.Duplex);
        }

        #endregion
    }
}
