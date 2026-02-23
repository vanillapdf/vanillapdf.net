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
    public class PdfActionTest
    {
        private const string GranizoPdf = "Granizo.pdf";

        #region ActionType Enum

        [Test]
        public void TestActionTypeEnumValues()
        {
            var values = Enum.GetValues(typeof(PdfActionType));
            ClassicAssert.AreEqual(7, values.Length, "PdfActionType should have 7 values");

            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfActionType), PdfActionType.Undefined));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfActionType), PdfActionType.GoTo));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfActionType), PdfActionType.GoToRemote));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfActionType), PdfActionType.URI));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfActionType), PdfActionType.Launch));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfActionType), PdfActionType.Named));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(PdfActionType), PdfActionType.JavaScript));
        }

        #endregion

        #region Reading Actions from Link Annotations

        [Test]
        public void TestLinkAnnotationActionIsNotNull()
        {
            // Granizo.pdf links use /A (action) instead of /Dest
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
                    using var linkAnnotation = PdfLinkAnnotation.FromAnnotation(annotation);
                    using var action = linkAnnotation.Action;
                    ClassicAssert.IsNotNull(action, $"Link annotation {i} should have an action");
                    return;
                }
            }

            Assert.Fail("Expected at least one link annotation on page 3");
        }

        [Test]
        public void TestAllLinkAnnotationsHaveGoToActions()
        {
            // Granizo.pdf page 3 has 13 link annotations, all using GoTo actions
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
            int actionCount = 0;

            for (ulong i = 0; i < count; i++) {
                using var annotation = annotations.At(i);
                if (annotation.GetAnnotationType() == PdfAnnotationType.Link) {
                    using var linkAnnotation = PdfLinkAnnotation.FromAnnotation(annotation);
                    using var action = linkAnnotation.Action;
                    ClassicAssert.IsNotNull(action);
                    ClassicAssert.AreEqual(PdfActionType.GoTo, action.ActionType,
                        $"Link annotation {i} should be GoTo action");
                    actionCount++;
                }
            }

            ClassicAssert.AreEqual(13, actionCount, "Page 3 should have 13 link annotations with actions");
        }

        [Test]
        public void TestLinkAnnotationActionAndDestinationAreMutuallyExclusive()
        {
            // When a link uses /A (action), /Dest should be null and vice versa
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
                    using var linkAnnotation = PdfLinkAnnotation.FromAnnotation(annotation);
                    using var action = linkAnnotation.Action;
                    using var destination = linkAnnotation.Destination;

                    // Granizo.pdf links use actions, so destination should be null
                    ClassicAssert.IsNotNull(action, $"Link {i} should have action");
                    ClassicAssert.IsNull(destination, $"Link {i} should not have direct destination");
                    return;
                }
            }

            Assert.Fail("Expected at least one link annotation on page 3");
        }

        #endregion

        #region GoTo Action Properties

        [Test]
        public void TestGoToActionHasDestination()
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
                    using var linkAnnotation = PdfLinkAnnotation.FromAnnotation(annotation);
                    using var action = linkAnnotation.Action;
                    ClassicAssert.IsNotNull(action);

                    using var goToAction = action.As<PdfGoToAction>();
                    ClassicAssert.IsNotNull(goToAction);

                    using var destination = goToAction.Destination;
                    ClassicAssert.IsNotNull(destination, "GoTo action should have a destination");
                    return;
                }
            }

            Assert.Fail("Expected at least one link annotation on page 3");
        }

        [Test]
        public void TestGoToActionDestinationHasValidType()
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
                    using var linkAnnotation = PdfLinkAnnotation.FromAnnotation(annotation);
                    using var action = linkAnnotation.Action;
                    ClassicAssert.IsNotNull(action);

                    using var goToAction = action.As<PdfGoToAction>();
                    ClassicAssert.IsNotNull(goToAction);

                    using var destination = goToAction.Destination;
                    ClassicAssert.IsNotNull(destination);
                    ClassicAssert.AreNotEqual(PdfDestinationType.Undefined, destination.DestinationType);
                    return;
                }
            }

            Assert.Fail("Expected at least one link annotation on page 3");
        }

        #endregion

        #region Action Extensions

        [Test]
        public void TestIsGoToAction()
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
                    using var linkAnnotation = PdfLinkAnnotation.FromAnnotation(annotation);
                    using var action = linkAnnotation.Action;
                    ClassicAssert.IsNotNull(action);

                    ClassicAssert.IsTrue(action.Is<PdfGoToAction>());
                    ClassicAssert.IsFalse(action.Is<PdfURIAction>());
                    ClassicAssert.IsFalse(action.Is<PdfGoToRemoteAction>());
                    ClassicAssert.IsFalse(action.Is<PdfNamedAction>());
                    return;
                }
            }

            Assert.Fail("Expected at least one link annotation on page 3");
        }

        [Test]
        public void TestAsGoToAction()
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
                    using var linkAnnotation = PdfLinkAnnotation.FromAnnotation(annotation);
                    using var action = linkAnnotation.Action;
                    ClassicAssert.IsNotNull(action);

                    using var asGoTo = action.As<PdfGoToAction>();
                    ClassicAssert.IsNotNull(asGoTo);

                    using var asURI = action.As<PdfURIAction>();
                    ClassicAssert.IsNull(asURI);

                    using var asRemote = action.As<PdfGoToRemoteAction>();
                    ClassicAssert.IsNull(asRemote);

                    using var asNamed = action.As<PdfNamedAction>();
                    ClassicAssert.IsNull(asNamed);
                    return;
                }
            }

            Assert.Fail("Expected at least one link annotation on page 3");
        }

        [Test]
        public void TestUpgradeGoToAction()
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
                    using var linkAnnotation = PdfLinkAnnotation.FromAnnotation(annotation);
                    using var action = linkAnnotation.Action;
                    ClassicAssert.IsNotNull(action);

                    using var upgraded = action.Upgrade();
                    ClassicAssert.IsInstanceOf<PdfGoToAction>(upgraded);
                    return;
                }
            }

            Assert.Fail("Expected at least one link annotation on page 3");
        }

        #endregion

        #region Catalog Open Action

        [Test]
        public void TestCatalogGetOpenAction()
        {
            string sourceFile = Path.Combine("Resources", GranizoPdf);

            using var sourceStream = PdfInputOutputStream.CreateFromFile(sourceFile);
            using var file = PdfFile.OpenStream(sourceStream, "sourceStream");
            file.Initialize();

            using PdfDocument document = PdfDocument.OpenFile(file);
            using PdfCatalog catalog = document.GetCatalog();

            // GetOpenAction() may return null if the document has no open action
            using var openAction = catalog.GetOpenAction();
        }

        #endregion

        #region Stability

        [Test]
        public void TestActionReadStability()
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

            // Find first link annotation index
            ulong linkIndex = 0;
            bool found = false;
            ulong count = annotations.GetSize();
            for (ulong i = 0; i < count; i++) {
                using var annotation = annotations.At(i);
                if (annotation.GetAnnotationType() == PdfAnnotationType.Link) {
                    linkIndex = i;
                    found = true;
                    break;
                }
            }

            if (!found) {
                Assert.Ignore("No link annotations found on page 3");
                return;
            }

            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                using var annotation = annotations.At(linkIndex);
                using var linkAnnotation = PdfLinkAnnotation.FromAnnotation(annotation);
                using var action = linkAnnotation.Action;
                using var goToAction = action.As<PdfGoToAction>();
            }

            GC.Collect();
        }

        #endregion
    }
}
