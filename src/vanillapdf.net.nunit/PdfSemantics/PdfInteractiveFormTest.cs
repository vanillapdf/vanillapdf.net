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
        internal static readonly string PagedSourcePath = Path.Combine("Resources", "19005-1_FAQ.PDF");

        [Test]
        public void TestSignatureFieldCreatesFormAndHierarchy()
        {
            using var source = TestDocument.Open(SourcePath);
            using var name = PdfLiteralStringObject.CreateFromDecodedString("Signature1");
            using var field = PdfSignatureField.CreateFromDocument(source.Document, name);

            ClassicAssert.IsTrue(field.IsTerminal());
            ClassicAssert.AreEqual("Signature1", field.GetQualifiedName());
            ClassicAssert.IsNull(field.GetParent());

            using var reopened = source.SaveAndReopen();
            using var catalog = reopened.Document.GetCatalog();
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
            using var source = TestDocument.Open(SourcePath);
            using var catalog = source.Document.GetCatalog();
            ClassicAssert.IsNull(catalog.GetAcroForm());

            using var form = PdfAcroForm.CreateFromDocument(source.Document);
            using var tree = PdfFieldTree.CreateFromDocument(source.Document);
            form.SetFieldTree(tree);
            catalog.SetAcroForm(form);

            using var defaultAppearance = PdfLiteralStringObject.CreateFromDecodedString("/Helv 12 Tf 0 g");
            form.SetDefaultAppearance(defaultAppearance);
            form.SetQuadding(PdfQuaddingType.Centered);

            using var flags = PdfSignatureFlags.Create();
            flags.SignaturesExist = true;
            form.SetSignatureFlags(flags);

            using var name = PdfLiteralStringObject.CreateFromDecodedString("Signature1");
            using var field = PdfSignatureField.CreateFromDocument(source.Document, name);
            ClassicAssert.AreEqual(1, tree.GetFieldCount());

            using var reopened = source.SaveAndReopen();
            using var reopenedCatalog = reopened.Document.GetCatalog();
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
            using var source = TestDocument.Open(SourcePath);

            using var groupName = PdfLiteralStringObject.CreateFromDecodedString("Group");
            using var group = PdfSignatureField.CreateFromDocument(source.Document, groupName);
            using var childName = PdfLiteralStringObject.CreateFromDecodedString("Child");
            using var child = PdfSignatureField.CreateFromDocument(source.Document, childName);

            using var catalog = source.Document.GetCatalog();
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
            using var source = TestDocument.Open(PagedSourcePath);
            using var name = PdfLiteralStringObject.CreateFromDecodedString("Signature1");
            using var field = PdfSignatureField.CreateFromDocument(source.Document, name);

            using var widget = PdfWidgetAnnotation.CreateFromDocument(source.Document);
            using var rect = CreateRectangle(50, 50, 250, 100);
            widget.Rect = rect;

            using var characteristics = PdfAppearanceCharacteristics.Create();
            characteristics.SetRotation(90);
            widget.AppearanceCharacteristics = characteristics;

            using var appearance = PdfFormXObject.CreateFromDocument(source.Document);
            using var boundingBox = CreateRectangle(0, 0, 200, 50);
            appearance.BoundingBox = boundingBox;
            widget.SetAppearance(PdfAppearanceType.Normal, appearance);

            field.AddWidget(widget);
            ClassicAssert.AreEqual(1, field.GetWidgetCount());

            AttachToFirstPage(source.Document, widget);

            using var reopened = source.SaveAndReopen();
            using var catalog = reopened.Document.GetCatalog();
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

            // The page sees the same widget through its /Annots array.
            using var pages = catalog.GetPages();
            using var page = pages.GetPage(1);
            using var annotations = page.GetAnnotations();
            using var annotation = annotations.At(0);
            ClassicAssert.AreEqual(PdfAnnotationType.Widget, annotation.GetAnnotationType());
            ClassicAssert.IsTrue(annotation.Is<PdfWidgetAnnotation>());
        }

        [Test]
        public void TestResourceDictionaryXObjects()
        {
            using var source = TestDocument.Open(SourcePath);

            using var resources = PdfResourceDictionary.Create();
            ClassicAssert.AreEqual(0, resources.GetXObjectCount());

            using var xobject = PdfFormXObject.CreateFromDocument(source.Document);
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

        internal static void AttachToFirstPage(PdfDocument document, PdfAnnotation annotation)
        {
            using var catalog = document.GetCatalog();
            using var pages = catalog.GetPages();
            using var page = pages.GetPage(1);
            using var annotations = PdfPageAnnotations.Create();
            annotations.Append(annotation);
            page.SetAnnotations(annotations);
        }

        internal static PdfRectangle CreateRectangle(double lowerLeftX, double lowerLeftY, double upperRightX, double upperRightY)
        {
            var rect = PdfRectangle.Create();
            rect.LowerLeftXReal = lowerLeftX;
            rect.LowerLeftYReal = lowerLeftY;
            rect.UpperRightXReal = upperRightX;
            rect.UpperRightYReal = upperRightY;
            return rect;
        }
    }
}
