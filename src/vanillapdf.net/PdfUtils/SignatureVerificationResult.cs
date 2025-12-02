using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Overall status of signature verification
    /// </summary>
    public enum SignatureVerificationStatus
    {
        /// <summary>Status not set (uninitialized)</summary>
        Undefined = 0,
        /// <summary>Signature is cryptographically valid</summary>
        Valid,
        /// <summary>Signature verification failed</summary>
        Invalid,
        /// <summary>Certificate has expired</summary>
        CertificateExpired,
        /// <summary>Certificate not yet valid</summary>
        CertificateNotYetValid,
        /// <summary>Certificate has been revoked</summary>
        CertificateRevoked,
        /// <summary>Certificate chain not trusted</summary>
        CertificateUntrusted,
        /// <summary>Document modified after signing</summary>
        DocumentModified,
        /// <summary>Weak digest algorithm (MD5, SHA1)</summary>
        WeakAlgorithm,
        /// <summary>Signature missing certificate</summary>
        MissingCertificate,
        /// <summary>Unable to determine status</summary>
        Unknown
    }

    /// <summary>
    /// Result of signature verification operation
    /// </summary>
    public class SignatureVerificationResult : PdfUnknown
    {
        internal SignatureVerificationResultSafeHandle Handle { get; }

        internal SignatureVerificationResult(SignatureVerificationResultSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static SignatureVerificationResult()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(SignatureVerificationResultSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get overall verification status
        /// </summary>
        public SignatureVerificationStatus Status
        {
            get
            {
                UInt32 result = NativeMethods.SignatureVerificationResult_GetStatus(Handle, out var status);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return (SignatureVerificationStatus)status;
            }
        }

        /// <summary>
        /// Get human-readable message describing verification result
        /// </summary>
        public string Message
        {
            get
            {
                UInt32 result = NativeMethods.SignatureVerificationResult_GetMessage(Handle, out var bufferHandle);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                using (var buffer = new PdfBuffer(bufferHandle)) {
                    return buffer.StringData;
                }
            }
        }

        /// <summary>
        /// Check if signature is cryptographically valid
        /// </summary>
        public bool IsSignatureValid
        {
            get
            {
                UInt32 result = NativeMethods.SignatureVerificationResult_IsSignatureValid(Handle, out var value);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return value != 0;
            }
        }

        /// <summary>
        /// Check if document bytes are intact (ByteRange matches)
        /// </summary>
        public bool IsDocumentIntact
        {
            get
            {
                UInt32 result = NativeMethods.SignatureVerificationResult_IsDocumentIntact(Handle, out var value);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return value != 0;
            }
        }

        /// <summary>
        /// Check if certificate chain is trusted
        /// </summary>
        public bool IsCertificateTrusted
        {
            get
            {
                UInt32 result = NativeMethods.SignatureVerificationResult_IsCertificateTrusted(Handle, out var value);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return value != 0;
            }
        }

        /// <summary>
        /// Get signer's certificate (DER-encoded)
        /// </summary>
        public PdfBuffer GetSignerCertificate()
        {
            UInt32 result = NativeMethods.SignatureVerificationResult_GetSignerCertificate(Handle, out var bufferHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(bufferHandle);
        }

        /// <summary>
        /// Get number of certificates in the chain
        /// </summary>
        public int CertificateChainCount
        {
            get
            {
                UInt32 result = NativeMethods.SignatureVerificationResult_GetCertificateChainCount(Handle, out var count);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return (int)count;
            }
        }

        /// <summary>
        /// Get certificate from chain at index (DER-encoded)
        /// </summary>
        /// <param name="index">Certificate index (0 = signer, 1+ = intermediate/root)</param>
        /// <returns>DER-encoded certificate</returns>
        public PdfBuffer GetCertificateChainAt(int index)
        {
            UInt32 result = NativeMethods.SignatureVerificationResult_GetCertificateChainAt(Handle, (UIntPtr)index, out var bufferHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(bufferHandle);
        }

        /// <summary>
        /// Get signer's common name from certificate
        /// </summary>
        public string SignerCommonName
        {
            get
            {
                UInt32 result = NativeMethods.SignatureVerificationResult_GetSignerCommonName(Handle, out var bufferHandle);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                using (var buffer = new PdfBuffer(bufferHandle)) {
                    return buffer.StringData;
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

        private static class NativeMethods
        {
            public static GetStatusDelegate SignatureVerificationResult_GetStatus = LibraryInstance.GetFunction<GetStatusDelegate>("SignatureVerificationResult_GetStatus");
            public static GetMessageDelegate SignatureVerificationResult_GetMessage = LibraryInstance.GetFunction<GetMessageDelegate>("SignatureVerificationResult_GetMessage");
            public static IsSignatureValidDelegate SignatureVerificationResult_IsSignatureValid = LibraryInstance.GetFunction<IsSignatureValidDelegate>("SignatureVerificationResult_IsSignatureValid");
            public static IsDocumentIntactDelegate SignatureVerificationResult_IsDocumentIntact = LibraryInstance.GetFunction<IsDocumentIntactDelegate>("SignatureVerificationResult_IsDocumentIntact");
            public static IsCertificateTrustedDelegate SignatureVerificationResult_IsCertificateTrusted = LibraryInstance.GetFunction<IsCertificateTrustedDelegate>("SignatureVerificationResult_IsCertificateTrusted");
            public static GetSignerCertificateDelegate SignatureVerificationResult_GetSignerCertificate = LibraryInstance.GetFunction<GetSignerCertificateDelegate>("SignatureVerificationResult_GetSignerCertificate");
            public static GetCertificateChainCountDelegate SignatureVerificationResult_GetCertificateChainCount = LibraryInstance.GetFunction<GetCertificateChainCountDelegate>("SignatureVerificationResult_GetCertificateChainCount");
            public static GetCertificateChainAtDelegate SignatureVerificationResult_GetCertificateChainAt = LibraryInstance.GetFunction<GetCertificateChainAtDelegate>("SignatureVerificationResult_GetCertificateChainAt");
            public static GetSignerCommonNameDelegate SignatureVerificationResult_GetSignerCommonName = LibraryInstance.GetFunction<GetSignerCommonNameDelegate>("SignatureVerificationResult_GetSignerCommonName");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetStatusDelegate(SignatureVerificationResultSafeHandle handle, out int status);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetMessageDelegate(SignatureVerificationResultSafeHandle handle, out PdfBufferSafeHandle buffer);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IsSignatureValidDelegate(SignatureVerificationResultSafeHandle handle, out int value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IsDocumentIntactDelegate(SignatureVerificationResultSafeHandle handle, out int value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IsCertificateTrustedDelegate(SignatureVerificationResultSafeHandle handle, out int value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetSignerCertificateDelegate(SignatureVerificationResultSafeHandle handle, out PdfBufferSafeHandle buffer);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetCertificateChainCountDelegate(SignatureVerificationResultSafeHandle handle, out UIntPtr count);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetCertificateChainAtDelegate(SignatureVerificationResultSafeHandle handle, UIntPtr index, out PdfBufferSafeHandle buffer);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetSignerCommonNameDelegate(SignatureVerificationResultSafeHandle handle, out PdfBufferSafeHandle buffer);
        }
    }
}
