using System;
using vanillapdf.net.Interop;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Class representing PKCS#12 private encryption/signature key
    /// </summary>
    public class PdfPKCS12Key : PdfUnknown
    {
        internal PdfPKCS12KeySafeHandle Handle { get; }

        internal PdfPKCS12Key(PdfPKCS12KeySafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new instance of \ref PdfPKCS12Key from specified file
        /// </summary>
        /// <param name="filename">Path to file containing PKCS#12 structure</param>
        /// <param name="password">Additional password to be used to decrypt file, NULL if no password required</param>
        /// <returns>New instance of \ref PdfPKCS12Key on success, throws exception on failure</returns>
        public static PdfPKCS12Key CreateFromFile(string filename, string password)
        {
            UInt32 result = NativeMethods.PKCS12Key_CreateFromFile(filename, password, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfPKCS12Key(data);
        }

        /// <summary>
        /// Create a new instance of \ref PdfPKCS12Key from binary data
        /// </summary>
        /// <param name="buffer">Binary data containing PKCS#12 structure</param>
        /// <param name="password">Additional password to be used to decrypt file, NULL if no password required</param>
        /// <returns>New instance of \ref PdfPKCS12Key on success, throws exception on failure</returns>
        public static PdfPKCS12Key CreateFromBuffer(PdfBuffer buffer, string password)
        {
            UInt32 result = NativeMethods.PKCS12Key_CreateFromBuffer(buffer.Handle, password, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfPKCS12Key(data);
        }

        /// <summary>
        /// Convert to \ref PdfSigningKey
        /// </summary>
        /// <param name="data">Handle to \ref PdfPKCS12Key to be converted</param>
        public static implicit operator PdfSigningKey(PdfPKCS12Key data)
        {
            return new PdfSigningKey(data.Handle);
        }

        /// <summary>
        /// Convert to \ref PdfPKCS12Key
        /// </summary>
        /// <param name="data">Handle to \ref PdfSigningKey to be converted</param>
        public static explicit operator PdfPKCS12Key(PdfSigningKey data)
        {
            return new PdfPKCS12Key(data.Handle);
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
