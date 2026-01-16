using System;
using vanillapdf.net.Interop;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Input stream can read and interpret input from sequences of characters
    /// </summary>
    public class PdfInputStream : PdfUnknown
    {
        internal PdfInputStreamSafeHandle Handle { get; }

        internal PdfInputStream(PdfInputStreamSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Current offset position in the input stream
        /// </summary>
        public Int64 InputPosition
        {
            get { return GetInputPosition(); }
            set { SetInputPosition(value); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfInputStream from specified file
        /// </summary>
        /// <param name="filename">Path to file to be read</param>
        /// <returns>New instance of \ref PdfInputStream on success, throws exception on failure</returns>
        public static PdfInputStream CreateFromFile(string filename)
        {
            UInt32 result = NativeMethods.InputStream_CreateFromFile(filename, out PdfInputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfInputStream(data);
        }

        /// <summary>
        /// Create a new instance of \ref PdfInputStream from specified data
        /// </summary>
        /// <param name="buffer">Binary data to be used as a source of input stream</param>
        /// <returns>New instance of \ref PdfInputStream on success, throws exception on failure</returns>
        public static PdfInputStream CreateFromBuffer(PdfBuffer buffer)
        {
            UInt32 result = NativeMethods.InputStream_CreateFromBuffer(buffer.Handle, out PdfInputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfInputStream(data);
        }

        /// <summary>
        /// Reads all data from the input stream and returns them as a single large buffer.
        /// This method is not recommended for large files, as the process might not have enough memory.
        /// </summary>
        /// <returns>A new instance of \ref PdfBuffer containing all the data from current instance of the input stream</returns>
        public PdfBuffer ToBuffer()
        {
            UInt32 result = NativeMethods.InputStream_ToBuffer(Handle, out PdfBufferSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        private Int64 GetInputPosition()
        {
            UInt32 result = NativeMethods.InputStream_GetInputPosition(Handle, out Int64 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetInputPosition(Int64 data)
        {
            UInt32 result = NativeMethods.InputStream_SetInputPosition(Handle, data);
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
