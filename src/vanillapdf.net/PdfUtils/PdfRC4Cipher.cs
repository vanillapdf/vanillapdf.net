using System;
using vanillapdf.net.Interop;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// RC4 (ARCFOUR) stream cipher, used by the PDF standard security handler.
    /// </summary>
    /// <remarks>
    /// RC4 is cryptographically broken and provided solely for PDF compatibility; do not use it for anything else.
    /// The cipher is symmetric, <see cref="Process"/> both encrypts and decrypts. The keystream continues
    /// across calls, so processing data in several parts yields the same output as processing it at once.
    /// </remarks>
    public class PdfRC4Cipher : IDisposable
    {
        internal PdfRC4CipherSafeHandle Handle { get; }

        internal PdfRC4Cipher(PdfRC4CipherSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new cipher initialized with the key.
        /// </summary>
        /// <param name="key">The key, 1 to 256 bytes long.</param>
        /// <returns>A new <see cref="PdfRC4Cipher"/> instance.</returns>
        public static PdfRC4Cipher CreateFromKey(PdfBuffer key)
        {
            UInt32 result = NativeMethods.RC4Cipher_CreateFromKey(key.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfRC4Cipher(data);
        }

        /// <summary>
        /// Encrypt or decrypt data with the following bytes of the keystream.
        /// </summary>
        /// <param name="data">The input data.</param>
        /// <returns>The output data, of the same length as <paramref name="data"/>.</returns>
        public PdfBuffer Process(PdfBuffer data)
        {
            UInt32 result = NativeMethods.RC4Cipher_Process(Handle, data.Handle, out var output);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(output);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
