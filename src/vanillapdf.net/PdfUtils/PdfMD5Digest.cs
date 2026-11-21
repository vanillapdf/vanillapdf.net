using System;
using vanillapdf.net.Interop;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// MD5 message digest (RFC 1321), used by the PDF standard security handler.
    /// </summary>
    /// <remarks>
    /// MD5 is cryptographically broken and provided solely for PDF compatibility; do not use it for anything else.
    /// Feed the data with <see cref="Update"/>, then obtain the digest with <see cref="Final"/>.
    /// The digest cannot be updated afterwards.
    /// </remarks>
    public class PdfMD5Digest : IDisposable
    {
        internal PdfMD5DigestSafeHandle Handle { get; }

        internal PdfMD5Digest(PdfMD5DigestSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new empty digest.
        /// </summary>
        /// <returns>A new <see cref="PdfMD5Digest"/> instance.</returns>
        public static PdfMD5Digest Create()
        {
            UInt32 result = NativeMethods.MD5Digest_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfMD5Digest(data);
        }

        /// <summary>
        /// Append data to the hashed message.
        /// </summary>
        /// <param name="data">The data to append.</param>
        public void Update(PdfBuffer data)
        {
            UInt32 result = NativeMethods.MD5Digest_Update(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Complete the digest. Fails when the digest has already been finalized.
        /// </summary>
        /// <returns>The 16 bytes of the digest.</returns>
        public PdfBuffer Final()
        {
            UInt32 result = NativeMethods.MD5Digest_Final(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
