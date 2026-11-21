using System;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit
{
    /// <summary>
    /// Shortcuts for steps that many tests repeat.
    /// Every returned object is owned by the caller and has to be disposed.
    /// </summary>
    internal static class TestExtensions
    {
        private const string MemoryStreamName = "memoryStream";

        /// <summary>
        /// Save the document into a new in-memory stream.
        /// </summary>
        public static PdfInputOutputStream SaveToMemory(this PdfDocument document)
        {
            var stream = PdfInputOutputStream.CreateFromMemory();
            using var destination = PdfFile.CreateStream(stream, MemoryStreamName);
            document.SaveFile(destination);
            return stream;
        }

        /// <summary>
        /// Open the stream as a file and parse its cross-reference tables.
        /// </summary>
        public static PdfFile OpenInitialized(this PdfInputOutputStream stream)
        {
            var file = PdfFile.OpenStream(stream, MemoryStreamName);
            file.Initialize();
            return file;
        }

        /// <summary>
        /// Get the page with the given one-based number.
        /// </summary>
        public static PdfPageObject GetPage(this PdfDocument document, UInt64 pageNumber)
        {
            using var catalog = document.GetCatalog();
            using var pages = catalog.GetPages();
            return pages.GetPage(pageNumber);
        }

        /// <summary>
        /// Append the annotation to the page's /Annots array, creating the array when the page has none.
        /// </summary>
        public static void AddAnnotation(this PdfPageObject page, PdfAnnotation annotation)
        {
            using var annotations = page.GetAnnotations() ?? PdfPageAnnotations.Create();
            annotations.Append(annotation);
            page.SetAnnotations(annotations);
        }

        /// <summary>
        /// Create a rectangle from its lower-left and upper-right corners.
        /// </summary>
        public static PdfRectangle CreateRectangle(double lowerLeftX, double lowerLeftY, double upperRightX, double upperRightY)
        {
            var rectangle = PdfRectangle.Create();
            rectangle.LowerLeftXReal = lowerLeftX;
            rectangle.LowerLeftYReal = lowerLeftY;
            rectangle.UpperRightXReal = upperRightX;
            rectangle.UpperRightYReal = upperRightY;
            return rectangle;
        }
    }
}
