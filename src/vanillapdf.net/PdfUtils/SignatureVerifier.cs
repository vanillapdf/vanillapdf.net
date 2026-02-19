using System;
using vanillapdf.net.Interop;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Static utility class for PKCS#7 signature verification
    /// </summary>
    public static class SignatureVerifier
    {
        /// <summary>
        /// Verify digital signature (low-level API)
        /// </summary>
        /// <param name="signedData">The raw bytes that were signed</param>
        /// <param name="signatureContents">The PKCS#7 signature blob</param>
        /// <param name="trustedStore">Trusted certificate store (required)</param>
        /// <param name="settings">Verification settings (optional, null for defaults)</param>
        /// <returns>Verification result with status and details</returns>
        /// <remarks>
        /// This is a low-level API that operates on raw bytes extracted from PDF.
        /// For verifying signatures in PDF documents, use DigitalSignatureExtensions.Verify instead.
        /// </remarks>
        public static SignatureVerificationResult Verify(
            PdfBuffer signedData,
            PdfBuffer signatureContents,
            TrustedCertificateStore trustedStore,
            SignatureVerificationSettings settings = null)
        {
            if (signedData == null) throw new ArgumentNullException(nameof(signedData));
            if (signatureContents == null) throw new ArgumentNullException(nameof(signatureContents));
            if (trustedStore == null) throw new ArgumentNullException(nameof(trustedStore));

            var settingsHandle = settings?.Handle ?? new SignatureVerificationSettingsSafeHandle();

            UInt32 result = NativeMethods.SignatureVerifier_Verify(
                signedData.Handle,
                signatureContents.Handle,
                trustedStore.Handle,
                settingsHandle,
                out var resultHandle);

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new SignatureVerificationResult(resultHandle);
        }
    }
}
