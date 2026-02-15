using System;
using vanillapdf.net.Interop;
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
    public class SignatureVerificationResult : IDisposable
    {
        internal SignatureVerificationResultSafeHandle Handle { get; }

        internal SignatureVerificationResult(SignatureVerificationResultSafeHandle handle)
        {
            Handle = handle;
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

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
