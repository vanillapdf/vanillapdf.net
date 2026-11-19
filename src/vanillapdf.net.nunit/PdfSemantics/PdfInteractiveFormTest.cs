using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.IO;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSemantics.Extensions;
using vanillapdf.net.PdfSyntax;

namespace vanillapdf.net.nunit.PdfSemantics
{
    /// <summary>
    /// Builds interactive forms through the 2.4.0 form API, saves them to memory
    /// and verifies what the reopened document contains.
    /// </summary>
    [TestFixture]
    public class PdfInteractiveFormTest
    {
        private static readonly string SourcePath = Path.Combine("Resources", "minimalist.pdf");

        // minimalist.pdf has no page tree /Kids, so anything that needs a page uses a real document.
        private static readonly string PagedSourcePath = Path.Combine("Resources", "19005-1_FAQ.PDF");

        [Test]
        public void TestSignatureFieldCreatesFormAndHierarchy()
        {
            using var file = PdfFile.Open(SourcePath);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);
            using var name = PdfLiteralStringObject.CreateFromDecodedString("Signature1");
            using var field = PdfSignatureField.CreateFromDocument(document, name);

            ClassicAssert.IsTrue(field.IsTerminal());
            ClassicAssert.AreEqual("Signature1", field.GetQualifiedName());
            ClassicAssert.IsNull(field.GetParent());

            using var reopenedStream = document.SaveToMemory();
            using var reopenedFile = reopenedStream.OpenInitialized();
            using var reopened = PdfDocument.OpenFile(reopenedFile);
            using var catalog = reopened.GetCatalog();
            using var form = catalog.GetAcroForm();
            ClassicAssert.IsNotNull(form);

            using var tree = form.GetFieldTree();
            ClassicAssert.IsNotNull(tree);
            ClassicAssert.AreEqual(1, tree.GetFieldCount());
            ClassicAssert.AreEqual(1, tree.GetRootChildCount());

            using var found = tree.FindField("Signature1");
            ClassicAssert.IsNotNull(found);
            ClassicAssert.AreEqual(PdfFieldType.Signature, found.GetFieldType());
            ClassicAssert.IsTrue(found.Is<PdfSignatureField>());

            ClassicAssert.IsNull(tree.FindField("Missing"));
        }

        [Test]
        public void TestFormLevelAttributes()
        {
            using var file = PdfFile.Open(SourcePath);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();
            ClassicAssert.IsNull(catalog.GetAcroForm());

            using var form = PdfAcroForm.CreateFromDocument(document);
            using var tree = PdfFieldTree.CreateFromDocument(document);
            form.SetFieldTree(tree);
            catalog.SetAcroForm(form);

            using var defaultAppearance = PdfLiteralStringObject.CreateFromDecodedString("/Helv 12 Tf 0 g");
            form.SetDefaultAppearance(defaultAppearance);
            form.SetQuadding(PdfQuaddingType.Centered);

            using var flags = PdfSignatureFlags.Create();
            flags.SignaturesExist = true;
            form.SetSignatureFlags(flags);

            using var name = PdfLiteralStringObject.CreateFromDecodedString("Signature1");
            using var field = PdfSignatureField.CreateFromDocument(document, name);
            ClassicAssert.AreEqual(1, tree.GetFieldCount());

            using var reopenedStream = document.SaveToMemory();
            using var reopenedFile = reopenedStream.OpenInitialized();
            using var reopened = PdfDocument.OpenFile(reopenedFile);
            using var reopenedCatalog = reopened.GetCatalog();
            using var reopenedForm = reopenedCatalog.GetAcroForm();

            using var reopenedDefaultAppearance = reopenedForm.GetDefaultAppearance();
            ClassicAssert.AreEqual("/Helv 12 Tf 0 g", reopenedDefaultAppearance.Value.StringData);
            ClassicAssert.AreEqual(PdfQuaddingType.Centered, reopenedForm.GetQuadding());

            using var reopenedFlags = reopenedForm.GetSignatureFlags();
            ClassicAssert.IsTrue(reopenedFlags.SignaturesExist);
            ClassicAssert.IsFalse(reopenedFlags.AppendOnly);

            using var reopenedTree = reopenedForm.GetFieldTree();
            using var reopenedField = reopenedTree.GetField(0);

            // The field carries neither /DA nor /Q, so resolution falls back to the form defaults.
            ClassicAssert.IsNull(reopenedField.GetDefaultAppearance());
            ClassicAssert.IsNull(reopenedField.GetQuadding());
            using var resolvedAppearance = reopenedForm.ResolveDefaultAppearance(reopenedField);
            ClassicAssert.AreEqual("/Helv 12 Tf 0 g", resolvedAppearance.Value.StringData);
            ClassicAssert.AreEqual(PdfQuaddingType.Centered, reopenedForm.ResolveQuadding(reopenedField));
        }

        [Test]
        public void TestFieldHierarchyMutation()
        {
            using var file = PdfFile.Open(SourcePath);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);

            using var groupName = PdfLiteralStringObject.CreateFromDecodedString("Group");
            using var group = PdfSignatureField.CreateFromDocument(document, groupName);
            using var childName = PdfLiteralStringObject.CreateFromDecodedString("Child");
            using var child = PdfSignatureField.CreateFromDocument(document, childName);

            using var catalog = document.GetCatalog();
            using var form = catalog.GetAcroForm();
            using var tree = form.GetFieldTree();
            ClassicAssert.AreEqual(2, tree.GetRootChildCount());

            // A field already in the hierarchy has to leave it before it can move under another one.
            tree.RemoveChild(child);
            ClassicAssert.AreEqual(1, tree.GetRootChildCount());

            tree.AddChild(group, child);
            ClassicAssert.IsFalse(group.IsTerminal());
            ClassicAssert.AreEqual(1, group.GetChildCount());
            ClassicAssert.AreEqual("Group.Child", child.GetQualifiedName());

            using var parent = child.GetParent();
            ClassicAssert.AreEqual("Group", parent.GetQualifiedName());

            using var found = tree.FindField("Group.Child");
            ClassicAssert.IsNotNull(found);
            ClassicAssert.AreEqual(1, tree.GetFieldCount());
        }

        [Test]
        public void TestWidgetAppearanceRoundTrip()
        {
            using var file = PdfFile.Open(PagedSourcePath);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);
            using var name = PdfLiteralStringObject.CreateFromDecodedString("Signature1");
            using var field = PdfSignatureField.CreateFromDocument(document, name);

            using var widget = PdfWidgetAnnotation.CreateFromDocument(document);
            using var rect = TestExtensions.CreateRectangle(50, 50, 250, 100);
            widget.Rect = rect;

            using var characteristics = PdfAppearanceCharacteristics.Create();
            characteristics.SetRotation(90);
            widget.AppearanceCharacteristics = characteristics;

            using var appearance = PdfFormXObject.CreateFromDocument(document);
            using var boundingBox = TestExtensions.CreateRectangle(0, 0, 200, 50);
            appearance.BoundingBox = boundingBox;
            widget.SetAppearance(PdfAppearanceType.Normal, appearance);

            field.AddWidget(widget);
            ClassicAssert.AreEqual(1, field.GetWidgetCount());

            using var page = document.GetPage(1);
            page.AddAnnotation(widget);

            using var reopenedStream = document.SaveToMemory();
            using var reopenedFile = reopenedStream.OpenInitialized();
            using var reopened = PdfDocument.OpenFile(reopenedFile);
            using var catalog = reopened.GetCatalog();
            using var form = catalog.GetAcroForm();
            using var tree = form.GetFieldTree();
            using var reopenedField = tree.FindField("Signature1");
            ClassicAssert.AreEqual(1, reopenedField.GetWidgetCount());

            using var reopenedWidget = reopenedField.GetWidget(0);
            using var reopenedRect = reopenedWidget.Rect;
            ClassicAssert.AreEqual(250.0, reopenedRect.UpperRightXReal);

            using var reopenedCharacteristics = reopenedWidget.AppearanceCharacteristics;
            ClassicAssert.AreEqual(90, reopenedCharacteristics.GetRotation());

            using var reopenedAppearance = reopenedWidget.GetAppearance(PdfAppearanceType.Normal);
            ClassicAssert.IsNotNull(reopenedAppearance);
            ClassicAssert.AreEqual(PdfXObjectType.Form, reopenedAppearance.GetXObjectType());
            using var reopenedBoundingBox = reopenedAppearance.BoundingBox;
            ClassicAssert.AreEqual(200.0, reopenedBoundingBox.UpperRightXReal);
            ClassicAssert.IsNull(reopenedWidget.GetAppearance(PdfAppearanceType.Down));

            // The page sees the widget as the last entry of its /Annots array.
            using var reopenedPage = reopened.GetPage(1);
            using var annotations = reopenedPage.GetAnnotations();
            using var annotation = annotations.At(annotations.GetSize() - 1);
            ClassicAssert.AreEqual(PdfAnnotationType.Widget, annotation.GetAnnotationType());
            ClassicAssert.IsTrue(annotation.Is<PdfWidgetAnnotation>());
        }

        [Test]
        public void TestResourceDictionaryXObjects()
        {
            using var file = PdfFile.Open(SourcePath);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);

            using var resources = PdfResourceDictionary.Create();
            ClassicAssert.AreEqual(0, resources.GetXObjectCount());

            using var xobject = PdfFormXObject.CreateFromDocument(document);
            using var name = PdfNameObject.CreateFromDecodedString("Fm1");
            resources.AddXObject(name, xobject);
            ClassicAssert.AreEqual(1, resources.GetXObjectCount());

            using var storedName = resources.GetXObjectName(0);
            ClassicAssert.AreEqual("Fm1", (string)storedName);

            using var found = resources.FindXObject(name);
            ClassicAssert.IsNotNull(found);
            ClassicAssert.AreEqual(PdfXObjectType.Form, found.GetXObjectType());
            using var form = PdfFormXObject.FromXObject(found);
            ClassicAssert.IsNotNull(form);

            ClassicAssert.IsTrue(resources.RemoveXObject(name));
            ClassicAssert.IsFalse(resources.RemoveXObject(name));
            ClassicAssert.IsNull(resources.FindXObject(name));
        }
    }
}
