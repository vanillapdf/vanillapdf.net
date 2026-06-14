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
        /// The name of the person or authority signing the document (/Name entry),
        /// or null if not present.
        /// </summary>
        public PdfStringObject Name => GetName();

        /// <summary>
        /// The reason for the signing (/Reason entry), such as "I agree...",
        /// or null if not present.
        /// </summary>
        public PdfStringObject Reason => GetReason();

        /// <summary>
        /// The CPU host name or physical location of the signing (/Location entry),
        /// or null if not present.
        /// </summary>
        public PdfStringObject Location => GetLocation();

        /// <summary>
        /// The information provided by the signer to enable a recipient
        /// to contact the signer (/ContactInfo entry), or null if not present.
        /// </summary>
        public PdfStringObject ContactInfo => GetContactInfo();

        /// <summary>
        /// The time of signing (/M entry), or null if not present.
        /// Depending on the signature handler, this may be a normal unverified computer time
        /// or a time generated in a verifiable way from a secure time server.
        /// </summary>
        public PdfDate SigningTime => GetSigningTime();

        /// <summary>
        /// The version of the signature handler that was used to create the signature (/R entry),
        /// or null if not present.
        /// </summary>
        public PdfIntegerObject Revision => GetRevision();

        /// <summary>
        /// The X.509 certificate chain used when signing (/Cert entry),
        /// or null if not present. The signing certificate appears first.
        /// </summary>
        public PdfStringObject Certificate => GetCertificate();

        /// <summary>
        /// The signature value (/Contents entry), or null if not present.
        /// For public-key signatures this is a DER-encoded PKCS#1 or PKCS#7 binary data object.
        /// </summary>
        public PdfHexadecimalStringObject Contents => GetContents();

        /// <summary>
        /// The exact byte ranges used for the digest calculation (/ByteRange entry),
        /// or null if not present.
        /// </summary>
        public PdfByteRangeCollection ByteRange => GetByteRange();

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

        #region Private Methods

        private PdfStringObject GetName()
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

        private PdfStringObject GetReason()
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

        private PdfStringObject GetLocation()
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

        private PdfStringObject GetContactInfo()
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

        private PdfDate GetSigningTime()
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

        private PdfIntegerObject GetRevision()
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

        private PdfStringObject GetCertificate()
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

        private PdfHexadecimalStringObject GetContents()
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

        private PdfByteRangeCollection GetByteRange()
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

        #endregion

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
