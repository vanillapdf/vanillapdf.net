using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Text;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfUtils
{
    /// <summary>
    /// Cryptographic toolkit configuration and the standard security handler primitives
    /// introduced in native 2.4.0.
    /// </summary>
    [TestFixture]
    public class PdfCryptoTest
    {
        [Test]
        public void TestCryptoSettingsLegacyAlgorithmsPolicy()
        {
            using var settings = PdfCryptoSettings.Create();
            ClassicAssert.AreEqual(PdfLegacyAlgorithmsPolicyType.Optional, settings.LegacyAlgorithmsPolicy);

            settings.LegacyAlgorithmsPolicy = PdfLegacyAlgorithmsPolicyType.Disabled;
            ClassicAssert.AreEqual(PdfLegacyAlgorithmsPolicyType.Disabled, settings.LegacyAlgorithmsPolicy);
        }

        [Test]
        public void TestCryptoUtilsInitializeWithDefaults()
        {
            // Initialization is a no-op once the toolkit is up, so the default
            // settings are safe to apply regardless of the test order.
            using var settings = PdfCryptoSettings.Create();
            PdfCryptoUtils.Initialize(settings);

            Assert.DoesNotThrow(() => PdfCryptoUtils.GetLegacyAlgorithmsAvailable());
        }

        [Test]
        public void TestMD5DigestKnownVector()
        {
            // RFC 1321 test suite: MD5("abc")
            using var digest = PdfMD5Digest.Create();
            using var data = PdfBuffer.CreateFromData(Encoding.ASCII.GetBytes("abc"));
            digest.Update(data);

            using var result = digest.Final();
            ClassicAssert.AreEqual("900150983cd24fb0d6963f7d28e17f72", ToHex(result.Data));
        }

        [Test]
        public void TestMD5DigestRejectsSecondFinal()
        {
            using var digest = PdfMD5Digest.Create();
            using var result = digest.Final();

            Assert.Throws<PdfParameterValueException>(() => digest.Final());
        }

        [Test]
        public void TestRC4CipherKnownVectorAndRoundTrip()
        {
            // Classic ARCFOUR vector: key "Key", plaintext "Plaintext"
            using var key = PdfBuffer.CreateFromData(Encoding.ASCII.GetBytes("Key"));
            using var plaintext = PdfBuffer.CreateFromData(Encoding.ASCII.GetBytes("Plaintext"));

            using var encryptor = PdfRC4Cipher.CreateFromKey(key);
            using var ciphertext = encryptor.Process(plaintext);
            ClassicAssert.AreEqual("bbf316e8d940af0ad3", ToHex(ciphertext.Data));

            using var decryptor = PdfRC4Cipher.CreateFromKey(key);
            using var decrypted = decryptor.Process(ciphertext);
            ClassicAssert.AreEqual("Plaintext", Encoding.ASCII.GetString(decrypted.Data));
        }

        private static string ToHex(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", "").ToLowerInvariant();
        }
    }
}
