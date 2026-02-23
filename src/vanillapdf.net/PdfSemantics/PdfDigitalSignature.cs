using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents a digital signature value attached to a PDF signature field.
    /// </summary>
    public class PdfDigitalSignature : IDisposable
    {
        internal PdfDigitalSignatureSafeHandle Handle { get; }

        internal PdfDigitalSignature(PdfDigitalSignatureSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Verify this digital signature against the given document and trust store.
        /// </summary>
        /// <param name="document">The open PDF document that contains this signature.</param>
        /// <param name="store">Trusted certificate store for chain validation.</param>
        /// <param name="settings">Verification settings (optional, null for defaults).</param>
        /// <returns>Verification result with status and details.</returns>
        public SignatureVerificationResult Verify(PdfDocument document, TrustedCertificateStore store, SignatureVerificationSettings settings = null)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            if (store == null) throw new ArgumentNullException(nameof(store));

            var settingsHandle = settings?.Handle ?? new SignatureVerificationSettingsSafeHandle();

            UInt32 result = NativeMethods.DigitalSignatureExtensions_Verify(
                Handle,
                document.Handle,
                store.Handle,
                settingsHandle,
                out SignatureVerificationResultSafeHandle resultHandle);

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new SignatureVerificationResult(resultHandle);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
