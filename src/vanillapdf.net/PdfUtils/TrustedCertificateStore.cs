using System;
using vanillapdf.net.Interop;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Collection of trusted certificates for signature verification
    /// </summary>
    public class TrustedCertificateStore : PdfUnknown
    {
        internal TrustedCertificateStoreSafeHandle Handle { get; }

        internal TrustedCertificateStore(TrustedCertificateStoreSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create an empty trusted certificate store
        /// </summary>
        /// <returns>New instance of TrustedCertificateStore</returns>
        public static TrustedCertificateStore Create()
        {
            UInt32 result = NativeMethods.TrustedCertificateStore_Create(out var handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new TrustedCertificateStore(handle);
        }

        /// <summary>
        /// Add a certificate from PEM format
        /// </summary>
        /// <param name="pemData">PEM-encoded certificate data</param>
        public void AddCertificateFromPEM(PdfBuffer pemData)
        {
            UInt32 result = NativeMethods.TrustedCertificateStore_AddCertificateFromPEM(Handle, pemData.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Add a certificate from DER format
        /// </summary>
        /// <param name="derData">DER-encoded certificate data</param>
        public void AddCertificateFromDER(PdfBuffer derData)
        {
            UInt32 result = NativeMethods.TrustedCertificateStore_AddCertificateFromDER(Handle, derData.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Load certificates from directory (e.g., /etc/ssl/certs)
        /// </summary>
        /// <param name="directoryPath">Path to directory containing certificates</param>
        public void LoadFromDirectory(string directoryPath)
        {
            UInt32 result = NativeMethods.TrustedCertificateStore_LoadFromDirectory(Handle, directoryPath);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Load system default trusted certificates.
        /// On Windows: Uses Windows Certificate Store.
        /// On Linux/macOS: Uses OpenSSL default paths.
        /// </summary>
        public void LoadSystemDefaults()
        {
            UInt32 result = NativeMethods.TrustedCertificateStore_LoadSystemDefaults(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
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
