using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
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

            var fitBHDest = destination.AsFitBoundingBoxHorizontal();
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

            var fitBHDest = destination.AsFitBoundingBoxHorizontal();
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

            ClassicAssert.IsNull(destination.AsXYZ());
            ClassicAssert.IsNull(destination.AsFit());
            ClassicAssert.IsNull(destination.AsFitHorizontal());
            ClassicAssert.IsNull(destination.AsFitVertical());
            ClassicAssert.IsNull(destination.AsFitRectangle());
            ClassicAssert.IsNull(destination.AsFitBoundingBox());
            ClassicAssert.IsNull(destination.AsFitBoundingBoxVertical());
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

            ClassicAssert.IsNotNull(destination.AsXYZ());
            ClassicAssert.IsNull(destination.AsFit());
            ClassicAssert.IsNull(destination.AsFitHorizontal());
            ClassicAssert.IsNull(destination.AsFitVertical());
            ClassicAssert.IsNull(destination.AsFitRectangle());
            ClassicAssert.IsNull(destination.AsFitBoundingBox());
            ClassicAssert.IsNull(destination.AsFitBoundingBoxHorizontal());
            ClassicAssert.IsNull(destination.AsFitBoundingBoxVertical());
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

            var xyzDest = destination.AsXYZ();
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

            ClassicAssert.IsNotNull(destination.AsFit());
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

            var fitHDest = destination.AsFitHorizontal();
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

            var fitVDest = destination.AsFitVertical();
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

            var fitRDest = destination.AsFitRectangle();
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

            ClassicAssert.IsNotNull(destination.AsFitBoundingBox());
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

            var fitBHDest = destination.AsFitBoundingBoxHorizontal();
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

            var fitBVDest = destination.AsFitBoundingBoxVertical();
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
