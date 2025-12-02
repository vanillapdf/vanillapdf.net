using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    [TestFixture]
    public class SignatureVerificationTest
    {
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
    }
}
