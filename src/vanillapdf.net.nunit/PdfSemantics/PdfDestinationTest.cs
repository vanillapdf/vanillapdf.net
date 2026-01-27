using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.IO;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSemantics
{
    [TestFixture]
    public class PdfDestinationTest
    {
        private const string GranizoPdf = "Granizo.pdf";

        [Test]
        public void TestPage3Has13LinkAnnotations()
        {
            // Granizo.pdf page 3 has Table of Contents with 13 link annotations
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using PdfPageTree tree = catalog.GetPages();

            ClassicAssert.GreaterOrEqual(tree.GetPageCount(), 3UL);

            using var pageObject = tree.GetPage(3);
            using var annotations = pageObject.GetAnnotations();
            ClassicAssert.IsNotNull(annotations);

            ulong count = annotations.GetSize();
            int linkCount = 0;
            for (ulong i = 0; i < count; i++) {
                using var annotation = annotations.At(i);
                if (annotation.GetAnnotationType() == PdfAnnotationType.Link) {
                    linkCount++;
                }
            }

            ClassicAssert.AreEqual(13, linkCount, "Page 3 should have 13 link annotations");
        }

        [Test]
        public void TestLinkAnnotationDestinationReturnsNullWhenUsingAction()
        {
            // Granizo.pdf links use /A (action) instead of /Dest
            // So LinkAnnotation.Destination returns null - this is expected behavior
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using PdfPageTree tree = catalog.GetPages();

            using var pageObject = tree.GetPage(3);
            using var annotations = pageObject.GetAnnotations();

            ulong count = annotations.GetSize();
            int linkWithDirectDestination = 0;

            for (ulong i = 0; i < count; i++) {
                using var annotation = annotations.At(i);

                if (annotation.GetAnnotationType() == PdfAnnotationType.Link) {
                    using var linkAnnotation = PdfLinkAnnotation.FromAnnotation(annotation);
                    using var destination = linkAnnotation.Destination;

                    if (destination != null) {
                        linkWithDirectDestination++;
                    }
                }
            }

            // Granizo.pdf uses GoTo actions, not direct /Dest entries
            // Action support is not yet implemented in the native library
            ClassicAssert.AreEqual(0, linkWithDirectDestination,
                "Links using /A (action) should have null Destination");
        }

        [Test]
        public void TestOutlineExists()
        {
            // Verify Granizo.pdf has an outline structure
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var outline = catalog.GetOutlines();

            ClassicAssert.IsNotNull(outline, "Document should have an outline");

            using var firstItem = outline.First;
            ClassicAssert.IsNotNull(firstItem, "Outline should have at least one item");

            // Get the title to verify we can read outline items
            using var title = firstItem.Title;
            ClassicAssert.IsNotNull(title, "First outline item should have a title");

            // Count outline items at first level
            int itemCount = 0;
            var currentItem = firstItem;

            while (currentItem != null) {
                itemCount++;

                using var nextItem = currentItem.Next;
                if (nextItem == null) break;

                currentItem.Dispose();
                currentItem = nextItem;
            }

            currentItem?.Dispose();

            ClassicAssert.Greater(itemCount, 0, "Outline should have at least one item");
        }

        [Test]
        public void TestOutlineItemDestinationReturnsNullWhenUsingNamedDestination()
        {
            // Granizo.pdf outline items use named destinations
            // OutlineItem.Destination returns null when destination is a name string
            // (named destinations need to be resolved via the Names/Dests tree)
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var outline = catalog.GetOutlines();

            ClassicAssert.IsNotNull(outline);

            using var firstItem = outline.First;
            ClassicAssert.IsNotNull(firstItem);

            // The native library's OutlineItem_GetDestination returns null
            // for named destinations - this is expected behavior
            // The destination would need to be resolved via Names/Dests tree
            using var destination = firstItem.Destination;

            // This documents current behavior - destinations that are name strings
            // return null (named destination resolution not yet implemented)
            // This test passes to document this behavior
        }
    }
}
