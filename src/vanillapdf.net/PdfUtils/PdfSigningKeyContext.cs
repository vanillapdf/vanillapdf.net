using System;
using System.Runtime.InteropServices;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Signing key context is used for transferring user data through Signature process
    /// </summary>
    public abstract class PdfSigningKeyContext : IDisposable
    {
        /// <summary>
        /// Allocates a new handle to be used as a callback parameter
        /// </summary>
        public PdfSigningKeyContext()
        {
            Handle = GCHandle.Alloc(this);
        }

        internal GCHandle Handle { get; }

        /// <summary>
        /// This function is called to seed and initialize signing engine
        /// </summary>
        /// <param name="digest">Hash algorithm to be using to compute signature</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the signature calculation is terminated</returns>
        public abstract UInt32 Initialize(PdfMessageDigestAlgorithmType digest);

        /// <summary>
        /// This function is called to sequentially to calculate hash of a potentially very long data content
        /// </summary>
        /// <param name="buffer">Additional data to be calculated hash from</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the signature calculation is terminated</returns>
        public abstract UInt32 Update(PdfBuffer buffer);

        /// <summary>
        /// Calculate the signature from the data provided by the \ref Update function
        /// </summary>
        /// <param name="buffer">Resulting signature data structure in the ASN.1 DER structure</param>
        /// <returns>PdfReturnValues.ERROR_SUCCESS on success, other value means error and the signature calculation is terminated</returns>
        public abstract UInt32 Final(out PdfBuffer buffer);

        /// <summary>
        /// Cleanup and release allocated resources required for signature calculation
        /// </summary>
        public abstract void Cleanup();

        private void ReleaseUnmanagedResources()
        {
            if (Handle.IsAllocated) {
                Handle.Free();
            }
        }

        /// <summary>
        /// Release all managed and unmanaged resources
        /// </summary>
        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~PdfSigningKeyContext()
        {
            ReleaseUnmanagedResources();
        }
    }
}
