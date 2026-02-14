using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.IO;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSemantics.Extensions;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSemantics
{
    [TestFixture]
    public class PdfOutlineBaseExtensionsTest
    {
        private const string FaqPdf = "19005-1_FAQ.PDF";

        [Test]
        public void TestOutlineIsOutline()
        {
            string sourceFile = Path.Combine("Resources", FaqPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var outline = catalog.GetOutlines();

            ClassicAssert.IsNotNull(outline);
            ClassicAssert.IsTrue(((PdfOutlineBase)outline).Is<PdfOutline>());
            ClassicAssert.IsFalse(((PdfOutlineBase)outline).Is<PdfOutlineItem>());
        }

        [Test]
        public void TestOutlineAsOutline()
        {
            string sourceFile = Path.Combine("Resources", FaqPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var outline = catalog.GetOutlines();

            ClassicAssert.IsNotNull(outline);

            using var asOutline = ((PdfOutlineBase)outline).As<PdfOutline>();
            ClassicAssert.IsNotNull(asOutline);

            using var asItem = ((PdfOutlineBase)outline).As<PdfOutlineItem>();
            ClassicAssert.IsNull(asItem);
        }

        [Test]
        public void TestFirstItemIsOutlineItem()
        {
            string sourceFile = Path.Combine("Resources", FaqPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var outline = catalog.GetOutlines();

            ClassicAssert.IsNotNull(outline);

            using var firstItem = outline.First;
            ClassicAssert.IsNotNull(firstItem);

            ClassicAssert.IsTrue(((PdfOutlineBase)firstItem).Is<PdfOutlineItem>());
            ClassicAssert.IsFalse(((PdfOutlineBase)firstItem).Is<PdfOutline>());
        }

        [Test]
        public void TestFirstItemAsOutlineItem()
        {
            string sourceFile = Path.Combine("Resources", FaqPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var outline = catalog.GetOutlines();

            ClassicAssert.IsNotNull(outline);

            using var firstItem = outline.First;
            ClassicAssert.IsNotNull(firstItem);

            using var asItem = ((PdfOutlineBase)firstItem).As<PdfOutlineItem>();
            ClassicAssert.IsNotNull(asItem);

            using var asOutline = ((PdfOutlineBase)firstItem).As<PdfOutline>();
            ClassicAssert.IsNull(asOutline);
        }
    }
}
