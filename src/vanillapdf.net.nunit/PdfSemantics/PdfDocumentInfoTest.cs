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
    public class PdfDocumentInfoTest
    {
        [Test]
        public void TestGetDocumentInfo()
        {
            string sourceFile = Path.Combine("Resources", "19005-1_FAQ.PDF");

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using var document = PdfDocument.OpenFile(file);
            using var documentInfo = document.GetDocumentInfo();

            ClassicAssert.IsNotNull(documentInfo);
        }

        [Test]
        public void TestDocumentInfoProperties()
        {
            string sourceFile = Path.Combine("Resources", "19005-1_FAQ.PDF");

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using var document = PdfDocument.OpenFile(file);
            using var documentInfo = document.GetDocumentInfo();

            // Properties may be null if not set in the document
            var title = documentInfo.Title;
            var author = documentInfo.Author;
            var subject = documentInfo.Subject;
            var keywords = documentInfo.Keywords;
            var creator = documentInfo.Creator;
            var producer = documentInfo.Producer;
            var creationDate = documentInfo.CreationDate;
            var modificationDate = documentInfo.ModificationDate;

            // At least verify we can access the properties without exceptions
            ClassicAssert.IsNotNull(documentInfo);
        }

        [Test]
        public void TestStability()
        {
            string sourceFile = Path.Combine("Resources", "19005-1_FAQ.PDF");

            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
                using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
                file.Initialize();

                using var document = PdfDocument.OpenFile(file);
                using var documentInfo = document.GetDocumentInfo();
            }

            GC.Collect();
        }
    }
}
