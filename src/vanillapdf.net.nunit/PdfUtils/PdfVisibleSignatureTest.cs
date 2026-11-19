using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.IO;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    /// <summary>
    /// Signing into a caller-built signature field (native 2.4.0 Document_SignField)
    /// and the new signature settings.
    /// </summary>
    [TestFixture]
    public class PdfVisibleSignatureTest
    {
        private static readonly string SourcePath = Path.Combine("Resources", "minimalist.pdf");

        // minimalist.pdf has no page tree /Kids, so a visible signature needs a real document.
        private static readonly string PagedSourcePath = Path.Combine("Resources", "19005-1_FAQ.PDF");
        private const string FieldName = "VisibleSignature";

        [Test]
        public void TestSignatureSettingsDefaults()
        {
            using var settings = PdfDocumentSignatureSettings.Create();
            ClassicAssert.AreEqual(16384, settings.ReservedSignatureSize);

            using var subFilter = settings.SubFilter;
            ClassicAssert.AreEqual("adbe.pkcs7.detached", (string)subFilter);

            settings.ReservedSignatureSize = 32768;
            ClassicAssert.AreEqual(32768, settings.ReservedSignatureSize);

            using var cades = PdfNameObject.CreateFromDecodedString("ETSI.CAdES.detached");
            settings.SubFilter = cades;
            using var retrieved = settings.SubFilter;
            ClassicAssert.AreEqual("ETSI.CAdES.detached", (string)retrieved);
        }

        [Test]
        public void TestReservedSignatureSizeRejectsZero()
        {
            using var settings = PdfDocumentSignatureSettings.Create();
            Assert.Throws<PdfParameterValueException>(() => settings.ReservedSignatureSize = 0);
        }

        [Test]
        public void TestSignIntoVisibleField()
        {
            using var file = PdfFile.Open(PagedSourcePath);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);

            using var name = PdfLiteralStringObject.CreateFromDecodedString(FieldName);
            using var field = PdfSignatureField.CreateFromDocument(document, name);

            using var widget = PdfWidgetAnnotation.CreateFromDocument(document);
            using var rect = TestExtensions.CreateRectangle(50, 50, 250, 100);
            widget.Rect = rect;
            field.AddWidget(widget);

            using var page = document.GetPage(1);
            page.AddAnnotation(widget);

            using var signedStream = PdfInputOutputStream.CreateFromMemory();
            using (var destination = PdfFile.CreateStream(signedStream, "signedStream")) {
                using var keyBuffer = PdfBuffer.CreateFromData(Convert.FromBase64String(SignatureVerificationTest.PKCS12_KEY_BASE64));
                using var key = PdfPKCS12Key.CreateFromBuffer(keyBuffer, null);
                using var settings = PdfDocumentSignatureSettings.Create();
                settings.SigningKey = key;
                settings.Digest = PdfMessageDigestAlgorithmType.SHA256;

                document.SignField(destination, field.GetQualifiedName(), settings);
            }

            using var signedFile = signedStream.OpenInitialized();
            using var signedDocument = PdfDocument.OpenFile(signedFile);
            using var catalog = signedDocument.GetCatalog();
            using var form = catalog.GetAcroForm();
            using var tree = form.GetFieldTree();

            // The signature landed in the caller's field instead of a new invisible one.
            ClassicAssert.AreEqual(1, tree.GetFieldCount());
            using var signedField = tree.FindField(FieldName);
            using var signatureField = PdfSignatureField.FromField(signedField);
            ClassicAssert.AreEqual(1, signatureField.GetWidgetCount());

            using var signature = signatureField.GetValue();
            ClassicAssert.IsNotNull(signature);
            using var subFilter = signature.SubFilter;
            ClassicAssert.AreEqual("adbe.pkcs7.detached", (string)subFilter);
        }

        [Test]
        public void TestSignFieldRejectsMissingField()
        {
            using var file = PdfFile.Open(SourcePath);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);
            using var name = PdfLiteralStringObject.CreateFromDecodedString(FieldName);
            using var field = PdfSignatureField.CreateFromDocument(document, name);

            using var signedStream = PdfInputOutputStream.CreateFromMemory();
            using var destination = PdfFile.CreateStream(signedStream, "signedStream");
            using var keyBuffer = PdfBuffer.CreateFromData(Convert.FromBase64String(SignatureVerificationTest.PKCS12_KEY_BASE64));
            using var key = PdfPKCS12Key.CreateFromBuffer(keyBuffer, null);
            using var settings = PdfDocumentSignatureSettings.Create();
            settings.SigningKey = key;

            Assert.Throws<PdfObjectMissingException>(() => document.SignField(destination, "NoSuchField", settings));
        }
    }
}
