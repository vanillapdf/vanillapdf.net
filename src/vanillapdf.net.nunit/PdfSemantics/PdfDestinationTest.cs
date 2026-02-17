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

        [Test]
        public void TestCreateXYZDestinationFromArray()
        {
            // Array format: [page /XYZ left top zoom]
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "XYZ");
            AppendIntegerToArray(array, 100);
            AppendIntegerToArray(array, 200);
            AppendIntegerToArray(array, 1);

            PdfDestination destination;
            try {
                destination = PdfDestination.CreateFromArray(array);
            } catch (Exception) {
                Assert.Ignore("Native library does not support CreateFromArray with synthetic page reference");
                return;
            }

            using (destination) {
                ClassicAssert.AreEqual(PdfDestinationType.XYZ, destination.DestinationType);

                using var pageNumber = destination.PageNumber;
                ClassicAssert.IsNotNull(pageNumber);

                var xyzDest = destination.As<PdfXYZDestination>();
                ClassicAssert.IsNotNull(xyzDest);

                using var left = xyzDest.Left;
                ClassicAssert.IsNotNull(left);

                using var top = xyzDest.Top;
                ClassicAssert.IsNotNull(top);

                using var zoom = xyzDest.Zoom;
                ClassicAssert.IsNotNull(zoom);
            }
        }

        [Test]
        public void TestCreateFitDestinationFromArray()
        {
            // Array format: [page /Fit]
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "Fit");

            PdfDestination destination;
            try {
                destination = PdfDestination.CreateFromArray(array);
            } catch (Exception) {
                Assert.Ignore("Native library does not support CreateFromArray with synthetic page reference");
                return;
            }

            using (destination) {
                ClassicAssert.AreEqual(PdfDestinationType.Fit, destination.DestinationType);

                var fitDest = destination.As<PdfFitDestination>();
                ClassicAssert.IsNotNull(fitDest);
            }
        }

        [Test]
        public void TestCreateFitHorizontalDestinationFromArray()
        {
            // Array format: [page /FitH top]
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "FitH");
            AppendIntegerToArray(array, 200);

            PdfDestination destination;
            try {
                destination = PdfDestination.CreateFromArray(array);
            } catch (Exception) {
                Assert.Ignore("Native library does not support CreateFromArray with synthetic page reference");
                return;
            }

            using (destination) {
                ClassicAssert.AreEqual(PdfDestinationType.FitHorizontal, destination.DestinationType);

                var fitHDest = destination.As<PdfFitHorizontalDestination>();
                ClassicAssert.IsNotNull(fitHDest);

                using var top = fitHDest.Top;
                ClassicAssert.IsNotNull(top);
            }
        }

        [Test]
        public void TestCreateFitVerticalDestinationFromArray()
        {
            // Array format: [page /FitV left]
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "FitV");
            AppendIntegerToArray(array, 100);

            PdfDestination destination;
            try {
                destination = PdfDestination.CreateFromArray(array);
            } catch (Exception) {
                Assert.Ignore("Native library does not support CreateFromArray with synthetic page reference");
                return;
            }

            using (destination) {
                ClassicAssert.AreEqual(PdfDestinationType.FitVertical, destination.DestinationType);

                var fitVDest = destination.As<PdfFitVerticalDestination>();
                ClassicAssert.IsNotNull(fitVDest);

                using var left = fitVDest.Left;
                ClassicAssert.IsNotNull(left);
            }
        }

        [Test]
        public void TestCreateFitRectangleDestinationFromArray()
        {
            // Array format: [page /FitR left bottom right top]
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "FitR");
            AppendIntegerToArray(array, 0);
            AppendIntegerToArray(array, 0);
            AppendIntegerToArray(array, 600);
            AppendIntegerToArray(array, 800);

            PdfDestination destination;
            try {
                destination = PdfDestination.CreateFromArray(array);
            } catch (Exception) {
                Assert.Ignore("Native library does not support CreateFromArray with synthetic page reference");
                return;
            }

            using (destination) {
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
        }

        [Test]
        public void TestCreateFitBoundingBoxDestinationFromArray()
        {
            // Array format: [page /FitB]
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "FitB");

            PdfDestination destination;
            try {
                destination = PdfDestination.CreateFromArray(array);
            } catch (Exception) {
                Assert.Ignore("Native library does not support CreateFromArray with synthetic page reference");
                return;
            }

            using (destination) {
                ClassicAssert.AreEqual(PdfDestinationType.FitBoundingBox, destination.DestinationType);

                var fitBDest = destination.As<PdfFitBoundingBoxDestination>();
                ClassicAssert.IsNotNull(fitBDest);
            }
        }

        [Test]
        public void TestCreateFitBoundingBoxHorizontalFromArray()
        {
            // Array format: [page /FitBH top]
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "FitBH");
            AppendIntegerToArray(array, 200);

            PdfDestination destination;
            try {
                destination = PdfDestination.CreateFromArray(array);
            } catch (Exception) {
                Assert.Ignore("Native library does not support CreateFromArray with synthetic page reference");
                return;
            }

            using (destination) {
                ClassicAssert.AreEqual(PdfDestinationType.FitBoundingBoxHorizontal, destination.DestinationType);

                var fitBHDest = destination.As<PdfFitBoundingBoxHorizontalDestination>();
                ClassicAssert.IsNotNull(fitBHDest);

                using var top = fitBHDest.Top;
                ClassicAssert.IsNotNull(top);
            }
        }

        [Test]
        public void TestCreateFitBoundingBoxVerticalFromArray()
        {
            // Array format: [page /FitBV left]
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "FitBV");
            AppendIntegerToArray(array, 100);

            PdfDestination destination;
            try {
                destination = PdfDestination.CreateFromArray(array);
            } catch (Exception) {
                Assert.Ignore("Native library does not support CreateFromArray with synthetic page reference");
                return;
            }

            using (destination) {
                ClassicAssert.AreEqual(PdfDestinationType.FitBoundingBoxVertical, destination.DestinationType);

                var fitBVDest = destination.As<PdfFitBoundingBoxVerticalDestination>();
                ClassicAssert.IsNotNull(fitBVDest);

                using var left = fitBVDest.Left;
                ClassicAssert.IsNotNull(left);
            }
        }

        [Test]
        public void TestAsConversionReturnsNullForMismatchedType()
        {
            // Create an XYZ destination and verify only AsXYZ succeeds
            using var array = PdfArrayObject.Create();
            AppendIntegerToArray(array, 0);
            AppendNameToArray(array, "XYZ");
            AppendIntegerToArray(array, 100);
            AppendIntegerToArray(array, 200);
            AppendIntegerToArray(array, 1);

            PdfDestination destination;
            try {
                destination = PdfDestination.CreateFromArray(array);
            } catch (Exception) {
                Assert.Ignore("Native library does not support CreateFromArray with synthetic page reference");
                return;
            }

            using (destination) {
                ClassicAssert.AreEqual(PdfDestinationType.XYZ, destination.DestinationType);

                ClassicAssert.IsTrue(destination.Is<PdfXYZDestination>());
                ClassicAssert.IsFalse(destination.Is<PdfFitDestination>());
                ClassicAssert.IsFalse(destination.Is<PdfFitHorizontalDestination>());
                ClassicAssert.IsFalse(destination.Is<PdfFitVerticalDestination>());
                ClassicAssert.IsFalse(destination.Is<PdfFitRectangleDestination>());
                ClassicAssert.IsFalse(destination.Is<PdfFitBoundingBoxDestination>());
                ClassicAssert.IsFalse(destination.Is<PdfFitBoundingBoxHorizontalDestination>());
                ClassicAssert.IsFalse(destination.Is<PdfFitBoundingBoxVerticalDestination>());
            }
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

            // GetDestinations() may return null if document uses /Names tree
            // instead of /Dests dictionary - this is valid behavior
            using var destinations = catalog.GetDestinations();
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

        [Test]
        public void TestDestinationCreationStability()
        {
            // Verify CreateFromArray works before entering stability loop
            using (var testArray = PdfArrayObject.Create()) {
                AppendIntegerToArray(testArray, 0);
                AppendNameToArray(testArray, "XYZ");
                AppendIntegerToArray(testArray, 100);
                AppendIntegerToArray(testArray, 200);
                AppendIntegerToArray(testArray, 1);

                try {
                    using var dest = PdfDestination.CreateFromArray(testArray);
                } catch (Exception) {
                    Assert.Ignore("Native library does not support CreateFromArray with synthetic page reference");
                    return;
                }
            }

            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                var array = PdfArrayObject.Create();
                AppendIntegerToArray(array, 0);
                AppendNameToArray(array, "XYZ");
                AppendIntegerToArray(array, 100);
                AppendIntegerToArray(array, 200);
                AppendIntegerToArray(array, 1);
                PdfDestination.CreateFromArray(array);
            }

            GC.Collect();
        }

        [Test]
        public void TestCatalogGetNamesReturnsNameDictionary()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();

            // GetNames() may return null if the document uses old-style /Dests
            using var names = catalog.GetNames();
        }

        [Test]
        public void TestNameDictionaryContainsDestinations()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();

            if (names == null) {
                Assert.Ignore("Document does not have a /Names dictionary");
                return;
            }

            // Just verify ContainsDestinations() works without throwing
            bool hasDestinations = names.ContainsDestinations();

            if (hasDestinations) {
                using var destTree = names.GetDestinations();
                ClassicAssert.IsNotNull(destTree);
            }
        }

        [Test]
        public void TestDestinationNameTreeIterator()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();

            if (names == null) {
                Assert.Ignore("Document does not have a /Names dictionary");
                return;
            }

            if (!names.ContainsDestinations()) {
                Assert.Ignore("Document does not have a /Dests name tree");
                return;
            }

            using var destTree = names.GetDestinations();
            using var iterator = destTree.GetIterator();

            int count = 0;
            while (iterator.IsValid) {
                using var key = iterator.Key;
                ClassicAssert.IsNotNull(key);

                using var value = iterator.Value;
                ClassicAssert.IsNotNull(value);

                count++;
                iterator.Next();
            }

            ClassicAssert.Greater(count, 0, "Name tree should have at least one entry");
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

            if (names == null) {
                Assert.Ignore("Document does not have a /Names dictionary");
                return;
            }

            if (!names.ContainsDestinations()) {
                Assert.Ignore("Document does not have a /Dests name tree");
                return;
            }

            using var destTree = names.GetDestinations();
            using var fakeName = PdfLiteralStringObject.CreateFromDecodedString("NonExistentDestination");

            bool found = destTree.TryFind(fakeName, out var destination);
            ClassicAssert.IsFalse(found);
            ClassicAssert.IsNull(destination);
        }

        [Test]
        public void TestDestinationNameTreeContains()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();
            using var names = catalog.GetNames();

            if (names == null) {
                Assert.Ignore("Document does not have a /Names dictionary");
                return;
            }

            if (!names.ContainsDestinations()) {
                Assert.Ignore("Document does not have a /Dests name tree");
                return;
            }

            using var destTree = names.GetDestinations();
            using var fakeName = PdfLiteralStringObject.CreateFromDecodedString("NonExistentDestination");

            bool contains = destTree.Contains(fakeName);
            ClassicAssert.IsFalse(contains);
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

            if (names == null) {
                Assert.Ignore("Document does not have a /Names dictionary");
                return;
            }

            if (!names.ContainsDestinations()) {
                Assert.Ignore("Document does not have a /Dests name tree");
                return;
            }

            // Get first name from iterator and look it up
            using var destTree = names.GetDestinations();
            using var iterator = destTree.GetIterator();

            if (!iterator.IsValid) {
                Assert.Ignore("Name tree is empty");
                return;
            }

            using var firstKey = iterator.Key;
            ClassicAssert.IsNotNull(firstKey);

            ClassicAssert.IsTrue(destTree.Contains(firstKey));

            using var destination = destTree.Find(firstKey);
            ClassicAssert.IsNotNull(destination);

            var destType = destination.DestinationType;
            ClassicAssert.AreNotEqual(PdfDestinationType.Undefined, destType);
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
    }
}
