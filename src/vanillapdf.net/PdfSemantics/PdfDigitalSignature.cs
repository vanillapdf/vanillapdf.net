using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
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
        /// Get the name of the person or authority signing the document (/Name entry).
        /// </summary>
        /// <returns>The <see cref="PdfStringObject"/> holding the name, or null if not present.</returns>
        public PdfStringObject GetName()
        {
            UInt32 result = NativeMethods.DigitalSignature_GetName(Handle, out PdfStringObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Get the reason for the signing (/Reason entry), such as "I agree...".
        /// </summary>
        /// <returns>The <see cref="PdfStringObject"/> holding the reason, or null if not present.</returns>
        public PdfStringObject GetReason()
        {
            UInt32 result = NativeMethods.DigitalSignature_GetReason(Handle, out PdfStringObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Get the CPU host name or physical location of the signing (/Location entry).
        /// </summary>
        /// <returns>The <see cref="PdfStringObject"/> holding the location, or null if not present.</returns>
        public PdfStringObject GetLocation()
        {
            UInt32 result = NativeMethods.DigitalSignature_GetLocation(Handle, out PdfStringObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Get the information provided by the signer to enable a recipient
        /// to contact the signer (/ContactInfo entry).
        /// </summary>
        /// <returns>The <see cref="PdfStringObject"/> holding the contact information, or null if not present.</returns>
        public PdfStringObject GetContactInfo()
        {
            UInt32 result = NativeMethods.DigitalSignature_GetContactInfo(Handle, out PdfStringObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Get the time of signing (/M entry).
        /// Depending on the signature handler, this may be a normal unverified computer time
        /// or a time generated in a verifiable way from a secure time server.
        /// </summary>
        /// <returns>The <see cref="PdfDate"/> holding the signing time, or null if not present.</returns>
        public PdfDate GetSigningTime()
        {
            UInt32 result = NativeMethods.DigitalSignature_GetSigningTime(Handle, out PdfDateSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDate(data);
        }

        /// <summary>
        /// Get the version of the signature handler that was used to create the signature (/R entry).
        /// </summary>
        /// <returns>The <see cref="PdfIntegerObject"/> holding the revision, or null if not present.</returns>
        public PdfIntegerObject GetRevision()
        {
            UInt32 result = NativeMethods.DigitalSignature_GetRevision(Handle, out PdfIntegerObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfIntegerObject(data);
        }

        /// <summary>
        /// Get the X.509 certificate chain used when signing (/Cert entry).
        /// The signing certificate appears first.
        /// </summary>
        /// <returns>The <see cref="PdfStringObject"/> holding the certificate data, or null if not present.</returns>
        public PdfStringObject GetCertificate()
        {
            UInt32 result = NativeMethods.DigitalSignature_GetCertificate(Handle, out PdfStringObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(data);
        }

        /// <summary>
        /// Get the signature value (/Contents entry).
        /// For public-key signatures this is a DER-encoded PKCS#1 or PKCS#7 binary data object.
        /// </summary>
        /// <returns>The <see cref="PdfHexadecimalStringObject"/> holding the signature value, or null if not present.</returns>
        public PdfHexadecimalStringObject GetContents()
        {
            UInt32 result = NativeMethods.DigitalSignature_GetContents(Handle, out PdfHexadecimalStringObjectSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        /// <summary>
        /// Get the exact byte ranges used for the digest calculation (/ByteRange entry).
        /// </summary>
        /// <returns>The <see cref="PdfByteRangeCollection"/>, or null if not present.</returns>
        public PdfByteRangeCollection GetByteRange()
        {
            UInt32 result = NativeMethods.DigitalSignature_GetByteRange(Handle, out PdfByteRangeCollectionSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfByteRangeCollection(data);
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
