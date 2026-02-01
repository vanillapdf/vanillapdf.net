using System;
using vanillapdf.net.Interop;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Configuration settings for signature verification
    /// </summary>
    public class SignatureVerificationSettings : PdfUnknown
    {
        internal SignatureVerificationSettingsSafeHandle Handle { get; }

        internal SignatureVerificationSettings(SignatureVerificationSettingsSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create new signature verification settings with default values
        /// </summary>
        /// <returns>New instance of SignatureVerificationSettings</returns>
        public static SignatureVerificationSettings Create()
        {
            UInt32 result = NativeMethods.SignatureVerificationSettings_Create(out var handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new SignatureVerificationSettings(handle);
        }

        /// <summary>
        /// Gets or sets whether to skip certificate validation.
        /// When enabled, signature verification will skip X509 certificate chain validation.
        /// The cryptographic signature is still verified, but the certificate chain is not
        /// validated against the trust store.
        /// </summary>
        /// <remarks>
        /// This is a security bypass intended for testing/debugging only.
        /// In production, certificates should be properly added to the trust store.
        /// </remarks>
        public bool SkipCertificateValidation
        {
            get
            {
                UInt32 result = NativeMethods.SignatureVerificationSettings_GetSkipCertificateValidation(Handle, out var value);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return value != 0;
            }
            set
            {
                UInt32 result = NativeMethods.SignatureVerificationSettings_SetSkipCertificateValidation(Handle, value ? 1 : 0);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
        }

        /// <summary>
        /// Gets or sets whether to validate certificate chain at signing time instead of current time
        /// </summary>
        public bool CheckSigningTime
        {
            get
            {
                UInt32 result = NativeMethods.SignatureVerificationSettings_GetCheckSigningTimeFlag(Handle, out var value);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return value != 0;
            }
            set
            {
                UInt32 result = NativeMethods.SignatureVerificationSettings_SetCheckSigningTimeFlag(Handle, value ? 1 : 0);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
        }

        /// <summary>
        /// Gets or sets whether to allow weak algorithms (MD5, SHA-1, RSA less than 2048 bits)
        /// </summary>
        public bool AllowWeakAlgorithms
        {
            get
            {
                UInt32 result = NativeMethods.SignatureVerificationSettings_GetAllowWeakAlgorithmsFlag(Handle, out var value);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return value != 0;
            }
            set
            {
                UInt32 result = NativeMethods.SignatureVerificationSettings_SetAllowWeakAlgorithmsFlag(Handle, value ? 1 : 0);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
        }

        #region PdfUnknown

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #endregion
    }
}
