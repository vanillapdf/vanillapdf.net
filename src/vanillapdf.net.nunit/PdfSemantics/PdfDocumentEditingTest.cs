using NUnit.Framework;
using NUnit.Framework.Legacy;
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
            using var file = PdfFile.Open(SourcePath);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);
            using var page = document.GetPage(1);

            using var cropBox = TestExtensions.CreateRectangle(10.5, 20, 500, 700.25);
            page.CropBox = cropBox;
            using var trimBox = TestExtensions.CreateRectangle(15, 25, 495, 695);
            page.TrimBox = trimBox;
            page.SetRotate(90);
            page.SetUserUnit(2.5);

            using var group = PdfTransparencyGroup.Create();
            group.SetIsolated(true);
            page.Group = group;

            using var reopenedStream = document.SaveToMemory();
            using var reopenedFile = reopenedStream.OpenInitialized();
            using var reopened = PdfDocument.OpenFile(reopenedFile);
            using var reopenedPage = reopened.GetPage(1);

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
            using var file = PdfFile.Open(SourcePath);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);
            using var page = document.GetPage(1);

            Assert.Throws<PdfParameterValueException>(() => page.SetRotate(45));
        }

        [Test]
        public void TestPageContentsAndResourcesReplacement()
        {
            using var file = PdfFile.Open(SourcePath);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);
            using var page = document.GetPage(1);

            using var contents = PdfPageContents.CreateFromDocument(document);
            page.SetContents(contents);

            using var resources = PdfResourceDictionary.Create();
            using var xobject = PdfFormXObject.CreateFromDocument(document);
            using var name = PdfNameObject.CreateFromDecodedString("Fm1");
            resources.AddXObject(name, xobject);
            page.SetResources(resources);

            using var reopenedStream = document.SaveToMemory();
            using var reopenedFile = reopenedStream.OpenInitialized();
            using var reopened = PdfDocument.OpenFile(reopenedFile);
            using var reopenedPage = reopened.GetPage(1);

            using var reopenedContents = reopenedPage.GetContents();
            using var instructions = reopenedContents.GetInstructionCollection();
            ClassicAssert.AreEqual(0, instructions.GetInstructionsSize());

            using var reopenedResources = reopenedPage.GetResources();
            ClassicAssert.AreEqual(1, reopenedResources.GetXObjectCount());
        }

        [Test]
        public void TestDocumentInfoRoundTrip()
        {
            using var file = PdfFile.Open(SourcePath);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);

            using var info = PdfDocumentInfo.CreateFromDocument(document);
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

            document.SetDocumentInfo(info);

            using var reopenedStream = document.SaveToMemory();
            using var reopenedFile = reopenedStream.OpenInitialized();
            using var reopened = PdfDocument.OpenFile(reopenedFile);
            using var reopenedInfo = reopened.GetDocumentInfo();

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
    }
}
