using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Extension methods for digital signature verification
    /// </summary>
    public static class DigitalSignatureExtensions
    {
        /// <summary>
        /// Verify a digital signature from a PDF document
        /// </summary>
        /// <param name="signedData">The raw bytes that were signed (from ByteRange)</param>
        /// <param name="signatureContents">The PKCS#7 signature blob (Contents entry)</param>
        /// <param name="trustedStore">Trusted certificate store</param>
        /// <param name="settings">Optional verification settings</param>
        /// <returns>Verification result with status and details</returns>
        public static SignatureVerificationResult Verify(
            PdfBuffer signedData,
            PdfBuffer signatureContents,
            TrustedCertificateStore trustedStore,
            SignatureVerificationSettings settings = null)
        {
            return SignatureVerifier.Verify(signedData, signatureContents, trustedStore, settings);
        }
    }
}
