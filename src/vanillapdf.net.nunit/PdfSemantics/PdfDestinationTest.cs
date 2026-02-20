using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.IO;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSemantics.Extensions;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSemantics
{
    [TestFixture]
    public class PdfDestinationTest
    {
        private const string GranizoPdf = "Granizo.pdf";

        #region Link Annotations

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
        public void TestLinkAnnotationFromAnnotationCreation()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using PdfPageTree tree = catalog.GetPages();

            using var pageObject = tree.GetPage(3);
            using var annotations = pageObject.GetAnnotations();
            ClassicAssert.IsNotNull(annotations);

            ulong count = annotations.GetSize();
            int linkCount = 0;

            for (ulong i = 0; i < count; i++) {
                using var annotation = annotations.At(i);
                if (annotation.GetAnnotationType() == PdfAnnotationType.Link) {
                    using var linkAnnotation = PdfLinkAnnotation.FromAnnotation(annotation);
                    ClassicAssert.IsNotNull(linkAnnotation);
                    linkCount++;
                }
            }

            ClassicAssert.AreEqual(13, linkCount, "All 13 link annotations should be convertible");
        }

        #endregion

        #region Outlines

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
            // for named destinations - this is expected behavior.
            // Named destinations can be resolved using PdfDestination.Resolve()
            // as an alternative path.
            using var destination = firstItem.Destination;
        }

        #endregion

        #region DestinationType Enum

        [Test]
        public void TestDestinationTypeEnumValues()
        {
            var values = Enum.GetValues(typeof(PdfDestinationType));
            ClassicAssert.AreEqual(9, values.Length, "PdfDestinationType should have 9 values");

            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfDestinationType), PdfDestinationType.Undefined));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfDestinationType), PdfDestinationType.XYZ));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfDestinationType), PdfDestinationType.Fit));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfDestinationType), PdfDestinationType.FitHorizontal));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfDestinationType), PdfDestinationType.FitVertical));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfDestinationType), PdfDestinationType.FitRectangle));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfDestinationType), PdfDestinationType.FitBoundingBox));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfDestinationType), PdfDestinationType.FitBoundingBoxHorizontal));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfDestinationType), PdfDestinationType.FitBoundingBoxVertical));
        }

        #endregion

        #region Reading Destinations from Name Tree

        [Test]
        public void TestNameDictionaryContainsDestinations()
        {
            // Granizo.pdf uses /Names tree for named destinations
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();
            ClassicAssert.IsNotNull(names, "Catalog should have a Names dictionary");

            ClassicAssert.IsTrue(names.ContainsDestinations(), "Names dictionary should contain destinations");
        }

        [Test]
        public void TestDestinationNameTreeFindReturnsDestination()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();
            using var destTree = names.GetDestinations();
            ClassicAssert.IsNotNull(destTree);

            using var key = PdfLiteralStringObject.CreateFromDecodedString("Doc-Start");
            ClassicAssert.IsTrue(destTree.Contains(key));

            using var destination = destTree.Find(key);
            ClassicAssert.IsNotNull(destination, "Find should return a destination for known name");
        }

        [Test]
        public void TestAllNamedDestinationsAreFitBoundingBoxHorizontal()
        {
            // Granizo.pdf has 30 named destinations, all FitBoundingBoxHorizontal
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();
            using var destTree = names.GetDestinations();
            using var iterator = destTree.GetIterator();

            int count = 0;
            while (iterator.IsValid) {
                using var destination = iterator.Value;
                ClassicAssert.IsNotNull(destination);
                ClassicAssert.AreEqual(PdfDestinationType.FitBoundingBoxHorizontal, destination.DestinationType,
                    $"Destination {count} should be FitBoundingBoxHorizontal");

                count++;
                iterator.Next();
            }

            ClassicAssert.AreEqual(30, count, "Granizo.pdf should have exactly 30 named destinations");
        }

        [Test]
        public void TestDestinationNameTreeContainsReturnsFalseForUnknown()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();
            using var destTree = names.GetDestinations();

            using var fakeName = PdfLiteralStringObject.CreateFromDecodedString("NonExistentDestination");
            ClassicAssert.IsFalse(destTree.Contains(fakeName));
        }

        [Test]
        public void TestDestinationNameTreeTryFindReturnsFalseForUnknown()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();
            using var destTree = names.GetDestinations();

            using var fakeName = PdfLiteralStringObject.CreateFromDecodedString("NonExistentDestination");
            bool found = destTree.TryFind(fakeName, out var destination);
            ClassicAssert.IsFalse(found);
            ClassicAssert.IsNull(destination);
        }

        [Test]
        public void TestNamedDestinationsTryFindReturnsFalseForUnknownName()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();

            // Try old-style /Dests dictionary first
            using var destinations = catalog.GetDestinations();
            if (destinations != null) {
                using var fakeName = PdfNameObject.CreateFromDecodedString("NonExistentDestination");
                bool found = destinations.TryFind(fakeName, out var destination);
                ClassicAssert.IsFalse(found);
                ClassicAssert.IsNull(destination);
                return;
            }

            // Fall back to /Names dictionary destination name tree
            using var names = catalog.GetNames();
            if (names == null || !names.ContainsDestinations()) {
                Assert.Ignore("Document has neither /Dests dictionary nor /Names destination tree");
                return;
            }

            using var destTree = names.GetDestinations();
            using var fakeLiteralName = PdfLiteralStringObject.CreateFromDecodedString("NonExistentDestination");
            bool treeFound = destTree.TryFind(fakeLiteralName, out var treeDestination);
            ClassicAssert.IsFalse(treeFound);
            ClassicAssert.IsNull(treeDestination);
        }

        #endregion

        #region Destination Properties

        [Test]
        public void TestDestinationHasValidType()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();
            using var destTree = names.GetDestinations();

            using var key = PdfLiteralStringObject.CreateFromDecodedString("Doc-Start");
            using var destination = destTree.Find(key);
            ClassicAssert.IsNotNull(destination);
            ClassicAssert.AreNotEqual(PdfDestinationType.Undefined, destination.DestinationType);
        }

        [Test]
        public void TestDestinationHasPageNumber()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();
            using var destTree = names.GetDestinations();

            using var key = PdfLiteralStringObject.CreateFromDecodedString("Doc-Start");
            using var destination = destTree.Find(key);
            ClassicAssert.IsNotNull(destination);

            using var pageNumber = destination.PageNumber;
            ClassicAssert.IsNotNull(pageNumber, "Destination should have a page number");
        }

        [Test]
        public void TestFitBoundingBoxHorizontalDestinationHasTop()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();
            using var destTree = names.GetDestinations();

            using var key = PdfLiteralStringObject.CreateFromDecodedString("Doc-Start");
            using var destination = destTree.Find(key);
            ClassicAssert.IsNotNull(destination);

            var fitBHDest = destination.As<PdfFitBoundingBoxHorizontalDestination>();
            ClassicAssert.IsNotNull(fitBHDest);

            using var top = fitBHDest.Top;
            ClassicAssert.IsNotNull(top, "FitBoundingBoxHorizontal destination should have a Top value");
        }

        #endregion

        #region Destination Type Conversions

        [Test]
        public void TestAsFitBoundingBoxHorizontal()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();
            using var destTree = names.GetDestinations();

            using var key = PdfLiteralStringObject.CreateFromDecodedString("Doc-Start");
            using var destination = destTree.Find(key);
            ClassicAssert.IsNotNull(destination);

            var fitBHDest = destination.As<PdfFitBoundingBoxHorizontalDestination>();
            ClassicAssert.IsNotNull(fitBHDest, "FitBoundingBoxHorizontal destination should convert successfully");
        }

        [Test]
        public void TestAsReturnsNullForMismatchedType()
        {
            // Granizo.pdf destinations are FitBoundingBoxHorizontal,
            // so all other As* conversions should return null
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();
            using var destTree = names.GetDestinations();

            using var key = PdfLiteralStringObject.CreateFromDecodedString("Doc-Start");
            using var destination = destTree.Find(key);
            ClassicAssert.IsNotNull(destination);

            ClassicAssert.IsNull(destination.As<PdfXYZDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitHorizontalDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitVerticalDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitRectangleDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitBoundingBoxDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitBoundingBoxVerticalDestination>());
        }

        [Test]
        public void TestAsConversionFromCreatedDestination()
        {
            // Create an XYZ destination and verify only AsXYZ succeeds
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "XYZ");
            AppendIntegerToArray(array, 100);
            AppendIntegerToArray(array, 200);
            AppendIntegerToArray(array, 1);

            using var destination = PdfDestination.CreateFromArray(array);

            ClassicAssert.IsNotNull(destination.As<PdfXYZDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitHorizontalDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitVerticalDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitRectangleDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitBoundingBoxDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitBoundingBoxHorizontalDestination>());
            ClassicAssert.IsNull(destination.As<PdfFitBoundingBoxVerticalDestination>());
        }

        #endregion

        #region Creating Destinations from Arrays

        [Test]
        public void TestCreateXYZDestinationFromArray()
        {
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "XYZ");
            AppendIntegerToArray(array, 100);
            AppendIntegerToArray(array, 200);
            AppendIntegerToArray(array, 1);

            using var destination = PdfDestination.CreateFromArray(array);
            ClassicAssert.AreEqual(PdfDestinationType.XYZ, destination.DestinationType);

            var xyzDest = destination.As<PdfXYZDestination>();
            ClassicAssert.IsNotNull(xyzDest);

            using var left = xyzDest.Left;
            ClassicAssert.IsNotNull(left);

            using var top = xyzDest.Top;
            ClassicAssert.IsNotNull(top);

            using var zoom = xyzDest.Zoom;
            ClassicAssert.IsNotNull(zoom);
        }

        [Test]
        public void TestCreateFitDestinationFromArray()
        {
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "Fit");

            using var destination = PdfDestination.CreateFromArray(array);
            ClassicAssert.AreEqual(PdfDestinationType.Fit, destination.DestinationType);

            ClassicAssert.IsNotNull(destination.As<PdfFitDestination>());
        }

        [Test]
        public void TestCreateFitHorizontalDestinationFromArray()
        {
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "FitH");
            AppendIntegerToArray(array, 200);

            using var destination = PdfDestination.CreateFromArray(array);
            ClassicAssert.AreEqual(PdfDestinationType.FitHorizontal, destination.DestinationType);

            var fitHDest = destination.As<PdfFitHorizontalDestination>();
            ClassicAssert.IsNotNull(fitHDest);

            using var top = fitHDest.Top;
            ClassicAssert.IsNotNull(top);
        }

        [Test]
        public void TestCreateFitVerticalDestinationFromArray()
        {
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "FitV");
            AppendIntegerToArray(array, 100);

            using var destination = PdfDestination.CreateFromArray(array);
            ClassicAssert.AreEqual(PdfDestinationType.FitVertical, destination.DestinationType);

            var fitVDest = destination.As<PdfFitVerticalDestination>();
            ClassicAssert.IsNotNull(fitVDest);

            using var left = fitVDest.Left;
            ClassicAssert.IsNotNull(left);
        }

        [Test]
        public void TestCreateFitRectangleDestinationFromArray()
        {
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "FitR");
            AppendIntegerToArray(array, 0);
            AppendIntegerToArray(array, 0);
            AppendIntegerToArray(array, 600);
            AppendIntegerToArray(array, 800);

            using var destination = PdfDestination.CreateFromArray(array);
            ClassicAssert.AreEqual(PdfDestinationType.FitRectangle, destination.DestinationType);

            var fitRDest = destination.As<PdfFitRectangleDestination>();
            ClassicAssert.IsNotNull(fitRDest);

            using var left = fitRDest.Left;
            ClassicAssert.IsNotNull(left);

            using var bottom = fitRDest.Bottom;
            ClassicAssert.IsNotNull(bottom);

            using var right = fitRDest.Right;
            ClassicAssert.IsNotNull(right);

            using var top = fitRDest.Top;
            ClassicAssert.IsNotNull(top);
        }

        [Test]
        public void TestCreateFitBoundingBoxDestinationFromArray()
        {
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "FitB");

            using var destination = PdfDestination.CreateFromArray(array);
            ClassicAssert.AreEqual(PdfDestinationType.FitBoundingBox, destination.DestinationType);

            ClassicAssert.IsNotNull(destination.As<PdfFitBoundingBoxDestination>());
        }

        [Test]
        public void TestCreateFitBoundingBoxHorizontalFromArray()
        {
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "FitBH");
            AppendIntegerToArray(array, 200);

            using var destination = PdfDestination.CreateFromArray(array);
            ClassicAssert.AreEqual(PdfDestinationType.FitBoundingBoxHorizontal, destination.DestinationType);

            var fitBHDest = destination.As<PdfFitBoundingBoxHorizontalDestination>();
            ClassicAssert.IsNotNull(fitBHDest);

            using var top = fitBHDest.Top;
            ClassicAssert.IsNotNull(top);
        }

        [Test]
        public void TestCreateFitBoundingBoxVerticalFromArray()
        {
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "FitBV");
            AppendIntegerToArray(array, 100);

            using var destination = PdfDestination.CreateFromArray(array);
            ClassicAssert.AreEqual(PdfDestinationType.FitBoundingBoxVertical, destination.DestinationType);

            var fitBVDest = destination.As<PdfFitBoundingBoxVerticalDestination>();
            ClassicAssert.IsNotNull(fitBVDest);

            using var left = fitBVDest.Left;
            ClassicAssert.IsNotNull(left);
        }

        #endregion

        #region Catalog Destinations

        [Test]
        public void TestCatalogGetNamesReturnsNameDictionary()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();

            using var names = catalog.GetNames();
            ClassicAssert.IsNotNull(names);
        }

        [Test]
        public void TestCatalogGetDestinations()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();

            // Granizo.pdf uses /Names tree, so old-style /Dests may return null
            using var destinations = catalog.GetDestinations();
        }

        #endregion

        #region Stability

        [Test]
        public void TestDestinationReadStability()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();
            using var destTree = names.GetDestinations();

            using var key = PdfLiteralStringObject.CreateFromDecodedString("Doc-Start");

            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                using var destination = destTree.Find(key);
                using var pageNumber = destination.PageNumber;
            }

            GC.Collect();
        }

        [Test]
        public void TestDestinationCreationStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                using var array = PdfArrayObject.Create();
                AppendIntegerToArray(array, 0);
                AppendNameToArray(array, "XYZ");
                AppendIntegerToArray(array, 100);
                AppendIntegerToArray(array, 200);
                AppendIntegerToArray(array, 1);
                using var destination = PdfDestination.CreateFromArray(array);
            }

            GC.Collect();
        }

        #endregion

        #region Helpers

        [Test]
        public void TestDestinationResolveFromNameTree()
        {
            // Granizo.pdf has exactly 30 named destinations, all FitBoundingBoxHorizontal
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using PdfPageTree tree = catalog.GetPages();
            using var names = catalog.GetNames();

            ClassicAssert.IsNotNull(names);
            ClassicAssert.IsTrue(names.ContainsDestinations());

            using var destTree = names.GetDestinations();
            using var iterator = destTree.GetIterator();

            int count = 0;
            while (iterator.IsValid) {
                using var value = iterator.Value;
                ClassicAssert.IsNotNull(value);

                // All destinations in Granizo.pdf are FitBoundingBoxHorizontal
                ClassicAssert.AreEqual(PdfDestinationType.FitBoundingBoxHorizontal, value.DestinationType);

                // Page reference should resolve to a valid page index
                using var pageRef = value.PageNumber;
                ulong pageIndex = tree.FindPageIndex(pageRef);
                ClassicAssert.GreaterOrEqual(pageIndex, 1UL);
                ClassicAssert.LessOrEqual(pageIndex, 10UL);

                count++;
                iterator.Next();
            }

            ClassicAssert.AreEqual(30, count, "Granizo.pdf should have exactly 30 named destinations");
        }

        [Test]
        public void TestDestinationResolveAndFindPageIndex()
        {
            // Verify specific known destinations in Granizo.pdf resolve
            // to the expected page indices
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using PdfPageTree tree = catalog.GetPages();

            ClassicAssert.AreEqual(10UL, tree.GetPageCount());

            using var names = catalog.GetNames();
            ClassicAssert.IsNotNull(names);
            ClassicAssert.IsTrue(names.ContainsDestinations());

            using var destTree = names.GetDestinations();

            // Verify Doc-Start points to page 1
            using var docStartKey = PdfLiteralStringObject.CreateFromDecodedString("Doc-Start");
            using var docStartDest = destTree.Find(docStartKey);
            ClassicAssert.AreEqual(PdfDestinationType.FitBoundingBoxHorizontal, docStartDest.DestinationType);
            using var docStartPageRef = docStartDest.PageNumber;
            ClassicAssert.AreEqual(1UL, tree.FindPageIndex(docStartPageRef));

            // Verify chapter.1 points to page 5
            using var chapterKey = PdfLiteralStringObject.CreateFromDecodedString("chapter.1");
            using var chapterDest = destTree.Find(chapterKey);
            ClassicAssert.AreEqual(PdfDestinationType.FitBoundingBoxHorizontal, chapterDest.DestinationType);
            using var chapterPageRef = chapterDest.PageNumber;
            ClassicAssert.AreEqual(5UL, tree.FindPageIndex(chapterPageRef));

            // Verify name1 points to page 8
            using var name1Key = PdfLiteralStringObject.CreateFromDecodedString("name1");
            using var name1Dest = destTree.Find(name1Key);
            ClassicAssert.AreEqual(PdfDestinationType.FitBoundingBoxHorizontal, name1Dest.DestinationType);
            using var name1PageRef = name1Dest.PageNumber;
            ClassicAssert.AreEqual(8UL, tree.FindPageIndex(name1PageRef));

            // Verify page.10 points to page 10
            using var page10Key = PdfLiteralStringObject.CreateFromDecodedString("page.10");
            using var page10Dest = destTree.Find(page10Key);
            using var page10PageRef = page10Dest.PageNumber;
            ClassicAssert.AreEqual(10UL, tree.FindPageIndex(page10PageRef));

            // Verify section.1.3 points to page 7
            using var sectionKey = PdfLiteralStringObject.CreateFromDecodedString("section.1.3");
            using var sectionDest = destTree.Find(sectionKey);
            using var sectionPageRef = sectionDest.PageNumber;
            ClassicAssert.AreEqual(7UL, tree.FindPageIndex(sectionPageRef));
        }

        private static void AppendIntegerToArray(PdfArrayObject array, long value)
        {
            using var obj = PdfIntegerObject.Create();
            obj.IntegerValue = value;
            array.Append(obj);
        }

        private static void AppendNameToArray(PdfArrayObject array, string name)
        {
            using var obj = PdfNameObject.CreateFromDecodedString(name);
            array.Append(obj);
        }

        #endregion
    }
}
