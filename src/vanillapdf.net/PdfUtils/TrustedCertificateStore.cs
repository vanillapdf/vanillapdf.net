using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

        static TrustedCertificateStore()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(TrustedCertificateStoreSafeHandle).TypeHandle);
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

        private static class NativeMethods
        {
            public static CreateDelegate TrustedCertificateStore_Create = LibraryInstance.GetFunction<CreateDelegate>("TrustedCertificateStore_Create");
            public static AddCertificateFromPEMDelegate TrustedCertificateStore_AddCertificateFromPEM = LibraryInstance.GetFunction<AddCertificateFromPEMDelegate>("TrustedCertificateStore_AddCertificateFromPEM");
            public static AddCertificateFromDERDelegate TrustedCertificateStore_AddCertificateFromDER = LibraryInstance.GetFunction<AddCertificateFromDERDelegate>("TrustedCertificateStore_AddCertificateFromDER");
            public static LoadFromDirectoryDelegate TrustedCertificateStore_LoadFromDirectory = LibraryInstance.GetFunction<LoadFromDirectoryDelegate>("TrustedCertificateStore_LoadFromDirectory");
            public static LoadSystemDefaultsDelegate TrustedCertificateStore_LoadSystemDefaults = LibraryInstance.GetFunction<LoadSystemDefaultsDelegate>("TrustedCertificateStore_LoadSystemDefaults");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelegate(out TrustedCertificateStoreSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 AddCertificateFromPEMDelegate(TrustedCertificateStoreSafeHandle handle, PdfBufferSafeHandle pemData);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 AddCertificateFromDERDelegate(TrustedCertificateStoreSafeHandle handle, PdfBufferSafeHandle derData);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LoadFromDirectoryDelegate(TrustedCertificateStoreSafeHandle handle, string directoryPath);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LoadSystemDefaultsDelegate(TrustedCertificateStoreSafeHandle handle);
        }
    }
}
