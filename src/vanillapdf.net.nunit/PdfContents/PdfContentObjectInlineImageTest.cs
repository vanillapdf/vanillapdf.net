using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.IO;
using System.Linq;
using vanillapdf.net.PdfContents;
using vanillapdf.net.PdfContents.Extensions;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfContents
{
    [TestFixture]
    public class PdfContentObjectInlineImageTest
    {
        // 2x2 DeviceGray 8bpc checkerboard, matching Resources/inline-image.pdf
        private static readonly byte[] ExpectedData = new byte[] { 0x00, 0xFF, 0xFF, 0x00 };

        [Test]
        public void TestInlineImageDataAndDictionary()
        {
            string sourceFile = Path.Combine("Resources", "inline-image.pdf");

            using var inlineImage = FindFirstInlineImage(sourceFile);
            ClassicAssert.IsNotNull(inlineImage, "Expected an inline image content object in the document");

            using var data = inlineImage.Data;
            byte[] actual = data.Data;

            // Native returns the raw inline-image sample data. Depending on the native
            // version this may be prefixed by the single white-space byte that delimits
            // the ID operator from the data, so assert on the trailing pixel bytes.
            ClassicAssert.GreaterOrEqual(actual.Length, ExpectedData.Length);
            byte[] pixels = actual.Skip(actual.Length - ExpectedData.Length).ToArray();
            CollectionAssert.AreEqual(ExpectedData, pixels);

            using var dictionary = inlineImage.Dictionary;
            ClassicAssert.IsNotNull(dictionary);
            ClassicAssert.Greater(dictionary.Count, 0);
        }

        private static PdfContentObjectInlineImage FindFirstInlineImage(string sourcePath)
        {
            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourcePath);
            using var sourceFile = PdfFile.OpenStream(sourceStream, "sourceStream");

            sourceFile.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(sourceFile);
            using PdfCatalog catalog = document.GetCatalog();
            using PdfPageTree tree = catalog.GetPages();

            for (ulong i = 0; i < tree.GetPageCount(); ++i) {
                using var pageObject = tree.GetPage(i + 1);
                using var pageContents = pageObject.GetContents();
                using var instructions = pageContents.GetInstructionCollection();

                for (ulong j = 0; j < instructions.GetInstructionsSize(); ++j) {
                    using var instruction = instructions.At(j);
                    if (instruction.GetInstructionType() != PdfContentInstructionType.Object) {
                        continue;
                    }

                    using var contentObject = PdfContentObject.FromContentInstruction(instruction);
                    if (contentObject.GetObjectType() != PdfContentObjectType.InlineImage) {
                        continue;
                    }

                    ClassicAssert.IsTrue(contentObject.Is<PdfContentObjectInlineImage>());
                    return contentObject.As<PdfContentObjectInlineImage>();
                }
            }

            return null;
        }
    }
}
