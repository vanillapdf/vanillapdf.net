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
    public class PdfAnnotationTest
    {
        private const string GranizoPdf = "Granizo.pdf";

        #region TextAnnotation

        [Test]
        public void TestTextAnnotationCreate()
        {
            using var rect = PdfRectangle.Create();
            rect.LowerLeftX = 100;
            rect.LowerLeftY = 200;
            rect.UpperRightX = 300;
            rect.UpperRightY = 400;

            using var annotation = PdfTextAnnotation.Create(rect);
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.Text, annotation.GetAnnotationType());
        }

        [Test]
        public void TestTextAnnotationCreateWithContents()
        {
            using var rect = PdfRectangle.Create();
            rect.LowerLeftX = 0;
            rect.LowerLeftY = 0;
            rect.UpperRightX = 100;
            rect.UpperRightY = 100;

            using var contents = PdfLiteralStringObject.CreateFromDecodedString("Test note");
            using var annotation = PdfTextAnnotation.CreateWithContents(rect, contents);
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.Text, annotation.GetAnnotationType());
        }

        [Test]
        public void TestTextAnnotationSetAndGetDates()
        {
            using var rect = PdfRectangle.Create();
            using var annotation = PdfTextAnnotation.Create(rect);

            using var modDate = PdfDate.CreateCurrent();
            annotation.ModificationDate = modDate;

            using var creationDate = PdfDate.CreateCurrent();
            annotation.CreationDate = creationDate;

            using var retrievedModDate = annotation.ModificationDate;
            ClassicAssert.IsNotNull(retrievedModDate);

            using var retrievedCreationDate = annotation.CreationDate;
            ClassicAssert.IsNotNull(retrievedCreationDate);
        }

        [Test]
        public void TestTextAnnotationStability()
        {
            using var rect = PdfRectangle.Create();

            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                PdfTextAnnotation.Create(rect);
            }

            GC.Collect();
        }

        #endregion

        #region HighlightAnnotation

        [Test]
        public void TestHighlightAnnotationCreate()
        {
            using var annotation = PdfHighlightAnnotation.Create();
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.Highlight, annotation.GetAnnotationType());
        }

        [Test]
        public void TestHighlightAnnotationCreateFromRect()
        {
            using var rect = PdfRectangle.Create();
            rect.LowerLeftX = 50;
            rect.LowerLeftY = 50;
            rect.UpperRightX = 200;
            rect.UpperRightY = 70;

            using var annotation = PdfHighlightAnnotation.CreateFromRect(rect);
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.Highlight, annotation.GetAnnotationType());
        }

        [Test]
        public void TestHighlightAnnotationQuadPoints()
        {
            using var annotation = PdfHighlightAnnotation.Create();

            using var quadPoints = PdfArrayObject.Create();
            annotation.QuadPoints = quadPoints;

            using var retrievedQuadPoints = annotation.QuadPoints;
            ClassicAssert.IsNotNull(retrievedQuadPoints);
        }

        #endregion

        #region FreeTextAnnotation

        [Test]
        public void TestFreeTextAnnotationCreate()
        {
            using var rect = PdfRectangle.Create();
            rect.LowerLeftX = 100;
            rect.LowerLeftY = 100;
            rect.UpperRightX = 400;
            rect.UpperRightY = 200;

            using var contents = PdfLiteralStringObject.CreateFromDecodedString("Free text content");
            using var defaultAppearance = PdfLiteralStringObject.CreateFromDecodedString("/Helv 12 Tf 0 0 0 rg");

            using var annotation = PdfFreeTextAnnotation.Create(rect, contents, defaultAppearance);
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.FreeText, annotation.GetAnnotationType());
        }

        [Test]
        public void TestFreeTextAnnotationDefaultAppearance()
        {
            using var rect = PdfRectangle.Create();
            using var contents = PdfLiteralStringObject.CreateFromDecodedString("Text");
            using var defaultAppearance = PdfLiteralStringObject.CreateFromDecodedString("/Helv 12 Tf");

            using var annotation = PdfFreeTextAnnotation.Create(rect, contents, defaultAppearance);

            using var da = annotation.DefaultAppearance;
            ClassicAssert.IsNotNull(da);
        }

        #endregion

        #region UnderlineAnnotation

        [Test]
        public void TestUnderlineAnnotationCreate()
        {
            using var annotation = PdfUnderlineAnnotation.Create();
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.Underline, annotation.GetAnnotationType());
        }

        [Test]
        public void TestUnderlineAnnotationCreateFromRect()
        {
            using var rect = PdfRectangle.Create();
            using var annotation = PdfUnderlineAnnotation.CreateFromRect(rect);
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.Underline, annotation.GetAnnotationType());
        }

        #endregion

        #region StrikeOutAnnotation

        [Test]
        public void TestStrikeOutAnnotationCreate()
        {
            using var annotation = PdfStrikeOutAnnotation.Create();
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.StrikeOut, annotation.GetAnnotationType());
        }

        [Test]
        public void TestStrikeOutAnnotationCreateFromRect()
        {
            using var rect = PdfRectangle.Create();
            using var annotation = PdfStrikeOutAnnotation.CreateFromRect(rect);
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.StrikeOut, annotation.GetAnnotationType());
        }

        #endregion

        #region SquigglyAnnotation

        [Test]
        public void TestSquigglyAnnotationCreate()
        {
            using var annotation = PdfSquigglyAnnotation.Create();
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.Squiggly, annotation.GetAnnotationType());
        }

        [Test]
        public void TestSquigglyAnnotationCreateFromRect()
        {
            using var rect = PdfRectangle.Create();
            using var annotation = PdfSquigglyAnnotation.CreateFromRect(rect);
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.Squiggly, annotation.GetAnnotationType());
        }

        #endregion

        #region InkAnnotation

        [Test]
        public void TestInkAnnotationCreate()
        {
            using var annotation = PdfInkAnnotation.Create();
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.Ink, annotation.GetAnnotationType());
        }

        [Test]
        public void TestInkAnnotationCreateFromRect()
        {
            using var rect = PdfRectangle.Create();
            using var annotation = PdfInkAnnotation.CreateFromRect(rect);
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.Ink, annotation.GetAnnotationType());
        }

        [Test]
        public void TestInkAnnotationInkList()
        {
            using var annotation = PdfInkAnnotation.Create();

            using var inkList = PdfArrayObject.Create();
            annotation.InkList = inkList;

            using var retrievedInkList = annotation.InkList;
            ClassicAssert.IsNotNull(retrievedInkList);
        }

        #endregion

        #region Base Annotation Properties

        [Test]
        public void TestAnnotationRectProperty()
        {
            using var rect = PdfRectangle.Create();
            rect.LowerLeftX = 10;
            rect.LowerLeftY = 20;
            rect.UpperRightX = 300;
            rect.UpperRightY = 400;

            using var annotation = PdfTextAnnotation.Create(rect);

            using var retrievedRect = annotation.Rect;
            ClassicAssert.IsNotNull(retrievedRect);
            ClassicAssert.AreEqual(10, retrievedRect.LowerLeftX);
            ClassicAssert.AreEqual(20, retrievedRect.LowerLeftY);
            ClassicAssert.AreEqual(300, retrievedRect.UpperRightX);
            ClassicAssert.AreEqual(400, retrievedRect.UpperRightY);
        }

        [Test]
        public void TestAnnotationSetRect()
        {
            using var rect = PdfRectangle.Create();
            using var annotation = PdfTextAnnotation.Create(rect);

            using var newRect = PdfRectangle.Create();
            newRect.LowerLeftX = 50;
            newRect.LowerLeftY = 60;
            newRect.UpperRightX = 150;
            newRect.UpperRightY = 160;

            annotation.Rect = newRect;

            using var retrievedRect = annotation.Rect;
            ClassicAssert.AreEqual(50, retrievedRect.LowerLeftX);
            ClassicAssert.AreEqual(60, retrievedRect.LowerLeftY);
            ClassicAssert.AreEqual(150, retrievedRect.UpperRightX);
            ClassicAssert.AreEqual(160, retrievedRect.UpperRightY);
        }

        [Test]
        public void TestAnnotationContentsProperty()
        {
            using var rect = PdfRectangle.Create();
            using var contents = PdfLiteralStringObject.CreateFromDecodedString("Hello World");
            using var annotation = PdfTextAnnotation.CreateWithContents(rect, contents);

            using var retrievedContents = annotation.Contents;
            ClassicAssert.IsNotNull(retrievedContents);
        }

        [Test]
        public void TestAnnotationSetContents()
        {
            using var rect = PdfRectangle.Create();
            using var annotation = PdfTextAnnotation.Create(rect);

            using var contents = PdfLiteralStringObject.CreateFromDecodedString("New contents");
            annotation.Contents = contents;

            using var retrievedContents = annotation.Contents;
            ClassicAssert.IsNotNull(retrievedContents);
        }

        [Test]
        public void TestAnnotationColorProperty()
        {
            using var rect = PdfRectangle.Create();
            using var annotation = PdfTextAnnotation.Create(rect);

            using var color = PdfColor.CreateRGB(1.0, 0.0, 0.0);
            annotation.Color = color;

            using var retrievedColor = annotation.Color;
            ClassicAssert.IsNotNull(retrievedColor);
            ClassicAssert.AreEqual(PdfColorSpaceType.DeviceRGB, retrievedColor.ColorSpace);
            ClassicAssert.AreEqual(1.0, retrievedColor.Red, 0.001);
            ClassicAssert.AreEqual(0.0, retrievedColor.Green, 0.001);
            ClassicAssert.AreEqual(0.0, retrievedColor.Blue, 0.001);
        }

        [Test]
        public void TestAnnotationFlagsProperty()
        {
            using var rect = PdfRectangle.Create();
            using var annotation = PdfTextAnnotation.Create(rect);

            annotation.Flags = PdfAnnotationFlags.Print | PdfAnnotationFlags.Locked;

            var flags = annotation.Flags;
            ClassicAssert.IsTrue(flags.HasFlag(PdfAnnotationFlags.Print));
            ClassicAssert.IsTrue(flags.HasFlag(PdfAnnotationFlags.Locked));
            ClassicAssert.IsFalse(flags.HasFlag(PdfAnnotationFlags.Hidden));
        }

        #endregion

        #region PageAnnotations Collection

        [Test]
        public void TestPageAnnotationsCreate()
        {
            using var annotations = PdfPageAnnotations.Create();
            ClassicAssert.IsNotNull(annotations);
            ClassicAssert.AreEqual(0UL, annotations.GetSize());
        }

        [Test]
        public void TestPageAnnotationsAppendAndSize()
        {
            using var annotations = PdfPageAnnotations.Create();

            using var rect = PdfRectangle.Create();
            using var textAnnotation = PdfTextAnnotation.Create(rect);
            annotations.Append(textAnnotation);

            ClassicAssert.AreEqual(1UL, annotations.GetSize());
        }

        [Test]
        public void TestPageAnnotationsAppendMultiple()
        {
            using var annotations = PdfPageAnnotations.Create();
            using var rect = PdfRectangle.Create();

            using var text = PdfTextAnnotation.Create(rect);
            annotations.Append(text);

            using var highlight = PdfHighlightAnnotation.Create();
            annotations.Append(highlight);

            using var ink = PdfInkAnnotation.Create();
            annotations.Append(ink);

            ClassicAssert.AreEqual(3UL, annotations.GetSize());
        }

        [Test]
        public void TestPageAnnotationsRemove()
        {
            using var annotations = PdfPageAnnotations.Create();
            using var rect = PdfRectangle.Create();

            using var text = PdfTextAnnotation.Create(rect);
            annotations.Append(text);

            using var highlight = PdfHighlightAnnotation.Create();
            annotations.Append(highlight);

            ClassicAssert.AreEqual(2UL, annotations.GetSize());

            annotations.Remove(0);
            ClassicAssert.AreEqual(1UL, annotations.GetSize());
        }

        [Test]
        public void TestPageAnnotationsAtReturnsCorrectType()
        {
            using var annotations = PdfPageAnnotations.Create();
            using var rect = PdfRectangle.Create();

            using var text = PdfTextAnnotation.Create(rect);
            annotations.Append(text);

            using var annotation = annotations.At(0);
            ClassicAssert.IsNotNull(annotation);
            ClassicAssert.AreEqual(PdfAnnotationType.Text, annotation.GetAnnotationType());
        }

        #endregion

        #region Annotation Extensions

        [Test]
        public void TestUpgradeTextAnnotation()
        {
            using var rect = PdfRectangle.Create();
            using var textAnnotation = PdfTextAnnotation.Create(rect);

            using var annotations = PdfPageAnnotations.Create();
            annotations.Append(textAnnotation);

            using var baseAnnotation = annotations.At(0);
            using var upgraded = baseAnnotation.Upgrade();

            ClassicAssert.IsInstanceOf<PdfTextAnnotation>(upgraded);
        }

        [Test]
        public void TestUpgradeHighlightAnnotation()
        {
            using var highlightAnnotation = PdfHighlightAnnotation.Create();

            using var annotations = PdfPageAnnotations.Create();
            annotations.Append(highlightAnnotation);

            using var baseAnnotation = annotations.At(0);
            using var upgraded = baseAnnotation.Upgrade();

            ClassicAssert.IsInstanceOf<PdfHighlightAnnotation>(upgraded);
        }

        [Test]
        public void TestIsAnnotationType()
        {
            using var rect = PdfRectangle.Create();
            using var textAnnotation = PdfTextAnnotation.Create(rect);

            using var annotations = PdfPageAnnotations.Create();
            annotations.Append(textAnnotation);

            using var baseAnnotation = annotations.At(0);
            ClassicAssert.IsTrue(baseAnnotation.Is<PdfTextAnnotation>());
            ClassicAssert.IsFalse(baseAnnotation.Is<PdfHighlightAnnotation>());
        }

        [Test]
        public void TestAsAnnotationType()
        {
            using var rect = PdfRectangle.Create();
            using var textAnnotation = PdfTextAnnotation.Create(rect);

            using var annotations = PdfPageAnnotations.Create();
            annotations.Append(textAnnotation);

            using var baseAnnotation = annotations.At(0);

            using var asText = baseAnnotation.As<PdfTextAnnotation>();
            ClassicAssert.IsNotNull(asText);

            using var asHighlight = baseAnnotation.As<PdfHighlightAnnotation>();
            ClassicAssert.IsNull(asHighlight);
        }

        [Test]
        public void TestUpgradeAllSupportedTypes()
        {
            using var annotations = PdfPageAnnotations.Create();
            using var rect = PdfRectangle.Create();

            using var text = PdfTextAnnotation.Create(rect);
            annotations.Append(text);

            using var highlight = PdfHighlightAnnotation.Create();
            annotations.Append(highlight);

            using var contents = PdfLiteralStringObject.CreateFromDecodedString("Text");
            using var da = PdfLiteralStringObject.CreateFromDecodedString("/Helv 12 Tf");
            using var freeText = PdfFreeTextAnnotation.Create(rect, contents, da);
            annotations.Append(freeText);

            using var underline = PdfUnderlineAnnotation.Create();
            annotations.Append(underline);

            using var strikeOut = PdfStrikeOutAnnotation.Create();
            annotations.Append(strikeOut);

            using var squiggly = PdfSquigglyAnnotation.Create();
            annotations.Append(squiggly);

            using var ink = PdfInkAnnotation.Create();
            annotations.Append(ink);

            ClassicAssert.AreEqual(7UL, annotations.GetSize());

            Type[] expectedTypes = {
                typeof(PdfTextAnnotation),
                typeof(PdfHighlightAnnotation),
                typeof(PdfFreeTextAnnotation),
                typeof(PdfUnderlineAnnotation),
                typeof(PdfStrikeOutAnnotation),
                typeof(PdfSquigglyAnnotation),
                typeof(PdfInkAnnotation),
            };

            for (ulong i = 0; i < annotations.GetSize(); i++) {
                using var baseAnnotation = annotations.At(i);
                using var upgraded = baseAnnotation.Upgrade();
                ClassicAssert.IsInstanceOf(expectedTypes[i], upgraded,
                    $"Annotation at index {i} should be {expectedTypes[i].Name}");
            }
        }

        #endregion

        #region Reading Annotations from Existing PDF

        [Test]
        public void TestReadLinkAnnotationBaseProperties()
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

            ulong count = annotations.GetSize();
            ClassicAssert.Greater(count, 0UL);

            // Find first link annotation and check its rect
            for (ulong i = 0; i < count; i++) {
                using var annotation = annotations.At(i);
                if (annotation.GetAnnotationType() == PdfAnnotationType.Link) {
                    using var rect = annotation.Rect;
                    ClassicAssert.IsNotNull(rect);
                    // Rect should have non-zero dimensions for visible annotations
                    ClassicAssert.AreNotEqual(rect.LowerLeftX, rect.UpperRightX);
                    ClassicAssert.AreNotEqual(rect.LowerLeftY, rect.UpperRightY);
                    return;
                }
            }

            Assert.Fail("Expected at least one link annotation on page 3");
        }

        [Test]
        public void TestUpgradeLinkAnnotationFromDocument()
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

            ulong count = annotations.GetSize();
            for (ulong i = 0; i < count; i++) {
                using var annotation = annotations.At(i);
                if (annotation.GetAnnotationType() == PdfAnnotationType.Link) {
                    ClassicAssert.IsTrue(annotation.Is<PdfLinkAnnotation>());
                    ClassicAssert.IsFalse(annotation.Is<PdfTextAnnotation>());

                    using var upgraded = annotation.As<PdfLinkAnnotation>();
                    ClassicAssert.IsNotNull(upgraded);
                    return;
                }
            }

            Assert.Fail("Expected at least one link annotation on page 3");
        }

        #endregion

        #region Annotation Flags Enum

        [Test]
        public void TestAnnotationFlagsEnumValues()
        {
            ClassicAssert.AreEqual(0, (int)PdfAnnotationFlags.None);
            ClassicAssert.AreEqual(1, (int)PdfAnnotationFlags.Invisible);
            ClassicAssert.AreEqual(2, (int)PdfAnnotationFlags.Hidden);
            ClassicAssert.AreEqual(4, (int)PdfAnnotationFlags.Print);
            ClassicAssert.AreEqual(8, (int)PdfAnnotationFlags.NoZoom);
            ClassicAssert.AreEqual(16, (int)PdfAnnotationFlags.NoRotate);
            ClassicAssert.AreEqual(32, (int)PdfAnnotationFlags.NoView);
            ClassicAssert.AreEqual(64, (int)PdfAnnotationFlags.ReadOnly);
            ClassicAssert.AreEqual(128, (int)PdfAnnotationFlags.Locked);
            ClassicAssert.AreEqual(256, (int)PdfAnnotationFlags.ToggleNoView);
            ClassicAssert.AreEqual(512, (int)PdfAnnotationFlags.LockedContents);
        }

        [Test]
        public void TestAnnotationFlagsCombination()
        {
            var combined = PdfAnnotationFlags.Print | PdfAnnotationFlags.ReadOnly | PdfAnnotationFlags.Locked;
            ClassicAssert.AreEqual(4 + 64 + 128, (int)combined);
            ClassicAssert.IsTrue(combined.HasFlag(PdfAnnotationFlags.Print));
            ClassicAssert.IsTrue(combined.HasFlag(PdfAnnotationFlags.ReadOnly));
            ClassicAssert.IsTrue(combined.HasFlag(PdfAnnotationFlags.Locked));
            ClassicAssert.IsFalse(combined.HasFlag(PdfAnnotationFlags.Hidden));
        }

        #endregion
    }
}
