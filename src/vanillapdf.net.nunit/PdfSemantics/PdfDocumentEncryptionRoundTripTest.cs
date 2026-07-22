using System.IO;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.nunit.PdfSemantics
{
    /// <summary>
    /// Exercises the managed encryption binding path end to end, entirely in memory:
    /// apply encryption settings, save to an in-memory stream, reopen, and decrypt.
    /// This guards the interop wiring (password buffer marshaling, AddEncryption /
    /// SaveFile / SetEncryptionPassword return handling and SafeHandle lifetime)
    /// rather than the native crypto correctness, which is covered in the native library.
    /// </summary>
    [TestFixture]
    public class PdfDocumentEncryptionRoundTripTest
    {
        private const string UserPassword = "user-secret";
        private const string OwnerPassword = "owner-secret";

        [TestCase(PdfEncryptionAlgorithmType.AES, 128)]
        [TestCase(PdfEncryptionAlgorithmType.AES, 256)]
        [TestCase(PdfEncryptionAlgorithmType.RC4, 128)]
        public void EncryptedDocument_ReopensAsEncrypted_AndDecryptsWithUserPassword(
            PdfEncryptionAlgorithmType algorithm, int keyLength)
        {
            using var stream = PdfInputOutputStream.CreateFromMemory();
            EncryptToStream(stream, algorithm, keyLength);

            using var file = PdfFile.OpenStream(stream, "encryptedStream");
            file.Initialize();

            ClassicAssert.IsTrue(file.Encrypted);
            ClassicAssert.IsTrue(file.SetEncryptionPassword(UserPassword));

            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();
            ClassicAssert.IsNotNull(catalog);
        }

        [TestCase(PdfEncryptionAlgorithmType.AES, 128)]
        [TestCase(PdfEncryptionAlgorithmType.AES, 256)]
        [TestCase(PdfEncryptionAlgorithmType.RC4, 128)]
        public void EncryptedDocument_DecryptsWithOwnerPassword(
            PdfEncryptionAlgorithmType algorithm, int keyLength)
        {
            using var stream = PdfInputOutputStream.CreateFromMemory();
            EncryptToStream(stream, algorithm, keyLength);

            using var file = PdfFile.OpenStream(stream, "encryptedStream");
            file.Initialize();

            ClassicAssert.IsTrue(file.SetEncryptionPassword(OwnerPassword));

            using var document = PdfDocument.OpenFile(file);
            using var catalog = document.GetCatalog();
            ClassicAssert.IsNotNull(catalog);
        }

        [TestCase(PdfEncryptionAlgorithmType.AES, 128)]
        [TestCase(PdfEncryptionAlgorithmType.AES, 256)]
        [TestCase(PdfEncryptionAlgorithmType.RC4, 128)]
        public void EncryptedDocument_RejectsWrongPassword(
            PdfEncryptionAlgorithmType algorithm, int keyLength)
        {
            using var stream = PdfInputOutputStream.CreateFromMemory();
            EncryptToStream(stream, algorithm, keyLength);

            using var file = PdfFile.OpenStream(stream, "encryptedStream");
            file.Initialize();

            ClassicAssert.IsTrue(file.Encrypted);
            ClassicAssert.IsFalse(file.SetEncryptionPassword("definitely-wrong-password"));
        }

        private static void EncryptToStream(PdfInputOutputStream destinationStream, PdfEncryptionAlgorithmType algorithm, int keyLength)
        {
            string source = Path.Combine("Resources", "19005-1_FAQ.PDF");

            using var file = PdfFile.Open(source);
            file.Initialize();
            using var document = PdfDocument.OpenFile(file);
            using var destinationFile = PdfFile.CreateStream(destinationStream, "encryptedStream");

            // The password buffers must stay alive until after the save, since the native
            // settings hold onto them while the document is being written.
            using var userPassword = PdfBuffer.CreateFromData(Encoding.ASCII.GetBytes(UserPassword));
            using var ownerPassword = PdfBuffer.CreateFromData(Encoding.ASCII.GetBytes(OwnerPassword));

            using var settings = PdfDocumentEncryptionSettings.Create();
            settings.EncryptionAlgorithmType = algorithm;
            settings.KeyLength = keyLength;
            settings.UserPassword = userPassword;
            settings.OwnerPassword = ownerPassword;

            document.AddEncryption(settings);
            document.SaveFile(destinationFile);
        }
    }
}
