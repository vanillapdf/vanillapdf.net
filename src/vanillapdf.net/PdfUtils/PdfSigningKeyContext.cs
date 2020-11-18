using System;
using System.Runtime.InteropServices;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Signing key context is used for transferring user data through Signature process
    /// </summary>
    public abstract class PdfSigningKeyContext : IDisposable
    {
        public PdfSigningKeyContext()
        {
            Handle = GCHandle.Alloc(this);
        }

        internal GCHandle Handle { get; }

        public abstract UInt32 Initialize(PdfMessageDigestAlgorithmType digest);
        public abstract UInt32 Update(PdfBuffer buffer);
        public abstract UInt32 Final(out PdfBuffer buffer);
        public abstract void Cleanup();

        private void ReleaseUnmanagedResources()
        {
            if (Handle.IsAllocated) {
                Handle.Free();
            }
        }

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
