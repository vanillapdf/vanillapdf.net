using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.IO;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    [TestFixture]
    public class SignatureVerificationTest
    {
        private const string PKCS12_KEY_BASE64 = @"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";

        #region SignatureVerificationStatus Enum

        [Test]
        public void TestSignatureVerificationStatusEnumValues()
        {
            var values = Enum.GetValues(typeof(SignatureVerificationStatus));
            ClassicAssert.AreEqual(11, values.Length, "SignatureVerificationStatus should have 11 values");

            ClassicAssert.IsTrue(Enum.IsDefined(typeof(SignatureVerificationStatus), SignatureVerificationStatus.Undefined));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(SignatureVerificationStatus), SignatureVerificationStatus.Valid));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(SignatureVerificationStatus), SignatureVerificationStatus.Invalid));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(SignatureVerificationStatus), SignatureVerificationStatus.CertificateExpired));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(SignatureVerificationStatus), SignatureVerificationStatus.CertificateNotYetValid));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(SignatureVerificationStatus), SignatureVerificationStatus.CertificateRevoked));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(SignatureVerificationStatus), SignatureVerificationStatus.CertificateUntrusted));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(SignatureVerificationStatus), SignatureVerificationStatus.DocumentModified));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(SignatureVerificationStatus), SignatureVerificationStatus.WeakAlgorithm));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(SignatureVerificationStatus), SignatureVerificationStatus.MissingCertificate));
            ClassicAssert.IsTrue(Enum.IsDefined(typeof(SignatureVerificationStatus), SignatureVerificationStatus.Unknown));
        }

        #endregion

        #region TrustedCertificateStore

        [Test]
        public void TestTrustedCertificateStoreCreate()
        {
            using var store = TrustedCertificateStore.Create();
            ClassicAssert.IsNotNull(store);
        }

        [Test]
        public void TestTrustedCertificateStoreLoadSystemDefaults()
        {
            using var store = TrustedCertificateStore.Create();
            store.LoadSystemDefaults();
        }

        #endregion

        #region SignatureVerificationSettings

        [Test]
        public void TestSignatureVerificationSettingsCreate()
        {
            using var settings = SignatureVerificationSettings.Create();
            ClassicAssert.IsNotNull(settings);
        }

        [Test]
        public void TestSignatureVerificationSettingsDefaults()
        {
            using var settings = SignatureVerificationSettings.Create();

            // Test default values
            ClassicAssert.IsFalse(settings.SkipCertificateValidation);
            ClassicAssert.IsFalse(settings.CheckSigningTime);
            ClassicAssert.IsFalse(settings.AllowWeakAlgorithms);
        }

        [Test]
        public void TestSignatureVerificationSettingsSkipCertificateValidation()
        {
            using var settings = SignatureVerificationSettings.Create();

            settings.SkipCertificateValidation = true;
            ClassicAssert.IsTrue(settings.SkipCertificateValidation);

            settings.SkipCertificateValidation = false;
            ClassicAssert.IsFalse(settings.SkipCertificateValidation);
        }

        [Test]
        public void TestSignatureVerificationSettingsCheckSigningTime()
        {
            using var settings = SignatureVerificationSettings.Create();

            settings.CheckSigningTime = true;
            ClassicAssert.IsTrue(settings.CheckSigningTime);

            settings.CheckSigningTime = false;
            ClassicAssert.IsFalse(settings.CheckSigningTime);
        }

        [Test]
        public void TestSignatureVerificationSettingsAllowWeakAlgorithms()
        {
            using var settings = SignatureVerificationSettings.Create();

            settings.AllowWeakAlgorithms = true;
            ClassicAssert.IsTrue(settings.AllowWeakAlgorithms);

            settings.AllowWeakAlgorithms = false;
            ClassicAssert.IsFalse(settings.AllowWeakAlgorithms);
        }

        #endregion

        #region Signing and Verification

        [Test]
        public void TestSignAndVerifySignature()
        {
            string sourceFile = Path.Combine("Resources", "Granizo.pdf");
            string signedFile = Path.Combine(Path.GetTempPath(), $"signed_{Guid.NewGuid()}.pdf");

            try {
                SignDocument(sourceFile, signedFile);

                using var store = TrustedCertificateStore.Create();
                using var settings = SignatureVerificationSettings.Create();
                settings.SkipCertificateValidation = true;

                using var result = VerifySignedDocument(signedFile, store, settings);

                ClassicAssert.IsTrue(result.IsSignatureValid);
                ClassicAssert.IsTrue(result.IsDocumentIntact);
                ClassicAssert.AreEqual(SignatureVerificationStatus.Valid, result.Status);
            } finally {
                if (File.Exists(signedFile)) {
                    File.Delete(signedFile);
                }
            }
        }

        [Test]
        public void TestVerificationResultProperties()
        {
            string sourceFile = Path.Combine("Resources", "Granizo.pdf");
            string signedFile = Path.Combine(Path.GetTempPath(), $"signed_{Guid.NewGuid()}.pdf");

            try {
                SignDocument(sourceFile, signedFile);

                using var store = TrustedCertificateStore.Create();
                using var settings = SignatureVerificationSettings.Create();
                settings.SkipCertificateValidation = true;

                using var result = VerifySignedDocument(signedFile, store, settings);

                // Signer common name should be non-empty
                string signerName = result.SignerCommonName;
                ClassicAssert.IsNotNull(signerName);
                ClassicAssert.IsNotEmpty(signerName);

                // Message should be available
                string message = result.Message;
                ClassicAssert.IsNotNull(message);

                // Signer certificate should be available
                using var signerCert = result.GetSignerCertificate();
                ClassicAssert.IsNotNull(signerCert);
                ClassicAssert.Greater(signerCert.Data.Length, 0);

                // Certificate chain should have at least one entry
                int chainCount = result.CertificateChainCount;
                ClassicAssert.Greater(chainCount, 0);

                using var chainCert = result.GetCertificateChainAt(0);
                ClassicAssert.IsNotNull(chainCert);
                ClassicAssert.Greater(chainCert.Data.Length, 0);
            } finally {
                if (File.Exists(signedFile)) {
                    File.Delete(signedFile);
                }
            }
        }

        [Test]
        public void TestSelfSignedCertificateIsUntrusted()
        {
            string sourceFile = Path.Combine("Resources", "Granizo.pdf");
            string signedFile = Path.Combine(Path.GetTempPath(), $"signed_{Guid.NewGuid()}.pdf");

            try {
                SignDocument(sourceFile, signedFile);

                using var store = TrustedCertificateStore.Create();
                // Do NOT skip certificate validation - verify self-signed cert is untrusted
                using var result = VerifySignedDocument(signedFile, store);

                ClassicAssert.IsFalse(result.IsCertificateTrusted);
            } finally {
                if (File.Exists(signedFile)) {
                    File.Delete(signedFile);
                }
            }
        }

        #endregion

        #region AcroForm API

        [Test]
        public void TestAcroFormFromFile()
        {
            string sourceFile = Path.Combine("Resources", "Granizo.pdf");
            string signedFile = Path.Combine(Path.GetTempPath(), $"signed_{Guid.NewGuid()}.pdf");

            try {
                SignDocument(sourceFile, signedFile);

                using var pdfFile = PdfFile.Open(signedFile);
                pdfFile.Initialize();
                using var document = PdfDocument.OpenFile(pdfFile);
                using var catalog = document.GetCatalog();
                using var acroForm = catalog.GetAcroForm();

                ClassicAssert.IsNotNull(acroForm);

                using var fields = acroForm.GetFields();
                int sigFieldCount = 0;
                UInt64 count = fields.GetSize();
                for (UInt64 i = 0; i < count; i++) {
                    using var field = fields.GetValueAt(i);
                    if (field.GetFieldType() == PdfFieldType.Signature) {
                        sigFieldCount++;
                    }
                }

                ClassicAssert.Greater(sigFieldCount, 0);
            } finally {
                if (File.Exists(signedFile)) {
                    File.Delete(signedFile);
                }
            }
        }

        [Test]
        public void TestSignatureFieldProperties()
        {
            string sourceFile = Path.Combine("Resources", "Granizo.pdf");
            string signedFile = Path.Combine(Path.GetTempPath(), $"signed_{Guid.NewGuid()}.pdf");

            try {
                SignDocument(sourceFile, signedFile);

                using var pdfFile = PdfFile.Open(signedFile);
                pdfFile.Initialize();
                using var document = PdfDocument.OpenFile(pdfFile);
                using var catalog = document.GetCatalog();
                using var acroForm = catalog.GetAcroForm();

                ClassicAssert.IsNotNull(acroForm);

                using var fields = acroForm.GetFields();
                UInt64 count = fields.GetSize();
                for (UInt64 i = 0; i < count; i++) {
                    using var field = fields.GetValueAt(i);
                    if (field.GetFieldType() != PdfFieldType.Signature) {
                        continue;
                    }

                    using var sigField = PdfSignatureField.FromField(field);
                    using var sig = sigField.GetValue();
                    ClassicAssert.IsNotNull(sig);
                    break;
                }
            } finally {
                if (File.Exists(signedFile)) {
                    File.Delete(signedFile);
                }
            }
        }

        [Test]
        public void TestAcroFormFromUnsignedFile()
        {
            string sourceFile = Path.Combine("Resources", "Granizo.pdf");

            using var pdfFile = PdfFile.Open(sourceFile);
            pdfFile.Initialize();
            using var document = PdfDocument.OpenFile(pdfFile);
            using var catalog = document.GetCatalog();
            using var acroForm = catalog.GetAcroForm();

            ClassicAssert.IsNull(acroForm);
        }

        #endregion

        #region Stability

        [Test]
        public void TestTrustedCertificateStoreStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                TrustedCertificateStore.Create();
            }

            GC.Collect();
        }

        [Test]
        public void TestSignatureVerificationSettingsStability()
        {
            for (int i = 0; i < OneTimeSetup.STABILITY_REPEAT_COUNT; ++i) {
                SignatureVerificationSettings.Create();
            }

            GC.Collect();
        }

        #endregion

        private static void SignDocument(string sourceFile, string signedFile)
        {
            using var file = PdfFile.Open(sourceFile);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);
            using var destFile = PdfFile.Create(signedFile);
            using var signSettings = PdfDocumentSignatureSettings.Create();
            using var keyBuffer = PdfBuffer.Create();
            keyBuffer.Data = Convert.FromBase64String(PKCS12_KEY_BASE64);
            using var key = PdfPKCS12Key.CreateFromBuffer(keyBuffer, null);

            signSettings.SigningKey = key;
            signSettings.Digest = PdfMessageDigestAlgorithmType.SHA256;
            document.Sign(destFile, signSettings);
        }

        private static SignatureVerificationResult VerifySignedDocument(string signedFile, TrustedCertificateStore store, SignatureVerificationSettings settings = null)
        {
            using var pdfFile = PdfFile.Open(signedFile);
            pdfFile.Initialize();
            using var document = PdfDocument.OpenFile(pdfFile);
            using var catalog = document.GetCatalog();
            using var acroForm = catalog.GetAcroForm();
            using var fields = acroForm.GetFields();

            UInt64 count = fields.GetSize();
            for (UInt64 i = 0; i < count; i++) {
                using var field = fields.GetValueAt(i);
                if (field.GetFieldType() != PdfFieldType.Signature) {
                    continue;
                }

                using var sigField = PdfSignatureField.FromField(field);
                using var sig = sigField.GetValue();
                if (sig != null) {
                    return sig.Verify(document, store, settings);
                }
            }

            throw new InvalidOperationException("No signed signature fields found in signed document.");
        }
    }
}
