using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents a single contiguous byte range (offset and length) within a PDF file,
    /// used for digital signature digest calculation.
    /// </summary>
    public class PdfByteRange : IDisposable
    {
        internal PdfByteRangeSafeHandle Handle { get; }

        internal PdfByteRange(PdfByteRangeSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// The byte offset where this range starts.
        /// </summary>
        public PdfIntegerObject Offset => GetOffset();

        /// <summary>
        /// The length of this range in bytes.
        /// </summary>
        public PdfIntegerObject Length => GetLength();

        #region Private Methods

        private PdfIntegerObject GetOffset()
        {
            UInt32 result = NativeMethods.ByteRange_GetOffset(Handle, out PdfIntegerObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfIntegerObject(data);
        }

        private PdfIntegerObject GetLength()
        {
            UInt32 result = NativeMethods.ByteRange_GetLength(Handle, out PdfIntegerObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfIntegerObject(data);
        }

        #endregion

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
