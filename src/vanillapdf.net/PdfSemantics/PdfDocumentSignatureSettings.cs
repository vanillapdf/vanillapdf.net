using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Class for specifying document signature details
    /// </summary>
    public class PdfDocumentSignatureSettings : PdfUnknown
    {
        internal PdfDocumentSignatureSettingsSafeHandle Handle { get; }

        internal PdfDocumentSignatureSettings(PdfDocumentSignatureSettingsSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Message digest algorithm for calculation hash of the data to be signed
        /// </summary>
        public PdfMessageDigestAlgorithmType Digest
        {
            get { return GetDigest(); }
            set { SetDigest(value); }
        }

        /// <summary>
        /// Handle to \ref PdfSigningKey to perform the signature
        /// </summary>
        public PdfSigningKey SigningKey
        {
            get { return GetSigningKey(); }
            set { SetSigningKey(value); }
        }

        /// <summary>
        /// Set the name of the person or authority signing the document.
        /// </summary>
        public PdfLiteralStringObject Name
        {
            get { return GetName(); }
            set { SetName(value); }
        }

        /// <summary>
        /// Get the CPU host name or physical location of the signing.
        /// </summary>
        public PdfLiteralStringObject Location
        {
            get { return GetLocation(); }
            set { SetLocation(value); }
        }

        /// <summary>
        /// Set the reason for the signing, such as (I agree...).
        /// </summary>
        public PdfLiteralStringObject Reason
        {
            get { return GetReason(); }
            set { SetReason(value); }
        }

        /// <summary>
        /// The time of signing.
        /// 
        /// Depending on the signature handler, this may be a normal unverified computer time or
        /// a time generated in a verifiable way from a secure time server.
        /// This value should be used only when the time of signing is not available in the signature.
        /// </summary>
        public PdfDate SigningTime
        {
            get { return GetSigningTime(); }
            set { SetSigningTime(value); }
        }

        /// <summary>
        /// An array of byte strings that shall represent the X.509 certificate chain
        /// used when signing and verifying signatures that use public-key cryptography,
        /// or a byte string if the chain has only one entry.
        /// </summary>
        public PdfHexadecimalStringObject Certificate
        {
            get { return GetCertificate(); }
            set { SetCertificate(value); }
        }

        /// <summary>
        /// Creates a new blank instance of \ref PdfDocumentSignatureSettings with default values
        /// </summary>
        /// <returns>A new \ref PdfDocumentSignatureSettings instance on success, throws exception on failure</returns>
        public static PdfDocumentSignatureSettings Create()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDocumentSignatureSettings(data);
        }

        private PdfMessageDigestAlgorithmType GetDigest()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetDigest(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfMessageDigestAlgorithmType>.CheckedCast(data);
        }

        private void SetDigest(PdfMessageDigestAlgorithmType data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetDigest(Handle, (Int32)data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfSigningKey GetSigningKey()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetSigningKey(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfSigningKey(data);
        }

        private void SetSigningKey(PdfSigningKey data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetSigningKey(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfLiteralStringObject GetName()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetName(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        private void SetName(PdfLiteralStringObject data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetName(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfLiteralStringObject GetLocation()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetLocation(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        private void SetLocation(PdfLiteralStringObject data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetLocation(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfLiteralStringObject GetReason()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetReason(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        private void SetReason(PdfLiteralStringObject data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetReason(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfDate GetSigningTime()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetSigningTime(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDate(data);
        }

        private void SetSigningTime(PdfDate data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetSigningTime(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfHexadecimalStringObject GetCertificate()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetCertificate(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        private void SetCertificate(PdfHexadecimalStringObject data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetCertificate(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }
    }
}
