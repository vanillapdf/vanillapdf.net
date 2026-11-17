using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.IO;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSemantics
{
    /// <summary>
    /// Page and document level setters introduced in native 2.4.0,
    /// verified on the document read back from memory.
    /// </summary>
    [TestFixture]
    public class PdfDocumentEditingTest
    {
        private static readonly string SourcePath = Path.Combine("Resources", "19005-1_FAQ.PDF");

        [Test]
        public void TestPageGeometryRoundTrip()
        {
            using var source = TestDocument.Open(SourcePath);
            using (var page = GetFirstPage(source.Document)) {
                using var cropBox = PdfInteractiveFormTest.CreateRectangle(10.5, 20, 500, 700.25);
                page.CropBox = cropBox;
                using var trimBox = PdfInteractiveFormTest.CreateRectangle(15, 25, 495, 695);
                page.TrimBox = trimBox;
                page.SetRotate(90);
                page.SetUserUnit(2.5);

                using var group = PdfTransparencyGroup.Create();
                group.SetIsolated(true);
                page.Group = group;
            }

            using var reopened = source.SaveAndReopen();
            using var reopenedPage = GetFirstPage(reopened.Document);

            using var reopenedCropBox = reopenedPage.CropBox;
            ClassicAssert.AreEqual(10.5, reopenedCropBox.LowerLeftXReal);
            ClassicAssert.AreEqual(700.25, reopenedCropBox.UpperRightYReal);

            using var reopenedTrimBox = reopenedPage.TrimBox;
            ClassicAssert.AreEqual(495.0, reopenedTrimBox.UpperRightXReal);

            ClassicAssert.AreEqual(90, reopenedPage.GetRotate());
            ClassicAssert.AreEqual(2.5, reopenedPage.GetUserUnit());

            using var reopenedGroup = reopenedPage.Group;
            ClassicAssert.AreEqual(true, reopenedGroup.GetIsolated());
            ClassicAssert.IsNull(reopenedGroup.GetKnockout());
        }

        [Test]
        public void TestPageRotateRejectsNonRightAngle()
        {
            using var source = TestDocument.Open(SourcePath);
            using var page = GetFirstPage(source.Document);

            Assert.Throws<PdfParameterValueException>(() => page.SetRotate(45));
        }

        [Test]
        public void TestPageContentsAndResourcesReplacement()
        {
            using var source = TestDocument.Open(SourcePath);
            using (var page = GetFirstPage(source.Document)) {
                using var contents = PdfPageContents.CreateFromDocument(source.Document);
                page.SetContents(contents);

                using var resources = PdfResourceDictionary.Create();
                using var xobject = PdfFormXObject.CreateFromDocument(source.Document);
                using var name = PdfNameObject.CreateFromDecodedString("Fm1");
                resources.AddXObject(name, xobject);
                page.SetResources(resources);
            }

            using var reopened = source.SaveAndReopen();
            using var reopenedPage = GetFirstPage(reopened.Document);

            using var reopenedContents = reopenedPage.GetContents();
            using var instructions = reopenedContents.GetInstructionCollection();
            ClassicAssert.AreEqual(0, instructions.GetInstructionsSize());

            using var reopenedResources = reopenedPage.GetResources();
            ClassicAssert.AreEqual(1, reopenedResources.GetXObjectCount());
        }

        [Test]
        public void TestDocumentInfoRoundTrip()
        {
            using var source = TestDocument.Open(SourcePath);
            using (var info = PdfDocumentInfo.CreateFromDocument(source.Document)) {
                using var title = PdfLiteralStringObject.CreateFromDecodedString("Quarterly report");
                using var author = PdfLiteralStringObject.CreateFromDecodedString("Vanilla.PDF tests");
                using var creationDate = PdfDate.CreateEmpty();
                creationDate.Year = 2026;
                creationDate.Month = 11;
                creationDate.Day = 17;

                info.Title = title;
                info.Author = author;
                info.CreationDate = creationDate;
                info.SetTrapped(PdfDocumentTrappedType.False);

                source.Document.SetDocumentInfo(info);
            }

            using var reopened = source.SaveAndReopen();
            using var reopenedInfo = reopened.Document.GetDocumentInfo();

            using var reopenedTitle = reopenedInfo.Title;
            ClassicAssert.AreEqual("Quarterly report", reopenedTitle.Value.StringData);
            using var reopenedAuthor = reopenedInfo.Author;
            ClassicAssert.AreEqual("Vanilla.PDF tests", reopenedAuthor.Value.StringData);
            ClassicAssert.IsNull(reopenedInfo.Subject);

            using var reopenedCreationDate = reopenedInfo.CreationDate;
            ClassicAssert.AreEqual(2026, reopenedCreationDate.Year);
            ClassicAssert.AreEqual(11, reopenedCreationDate.Month);
            ClassicAssert.AreEqual(17, reopenedCreationDate.Day);

            ClassicAssert.AreEqual(PdfDocumentTrappedType.False, reopenedInfo.GetTrapped());
        }

        private static PdfPageObject GetFirstPage(PdfDocument document)
        {
            using var catalog = document.GetCatalog();
            using var pages = catalog.GetPages();
            return pages.GetPage(1);
        }
    }
}
