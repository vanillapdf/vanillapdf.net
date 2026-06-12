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
        /// Get the byte offset where this range starts.
        /// </summary>
        /// <returns>The <see cref="PdfIntegerObject"/> holding the offset.</returns>
        public PdfIntegerObject GetOffset()
        {
            UInt32 result = NativeMethods.ByteRange_GetOffset(Handle, out PdfIntegerObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfIntegerObject(data);
        }

        /// <summary>
        /// Get the length of this range in bytes.
        /// </summary>
        /// <returns>The <see cref="PdfIntegerObject"/> holding the length.</returns>
        public PdfIntegerObject GetLength()
        {
            UInt32 result = NativeMethods.ByteRange_GetLength(Handle, out PdfIntegerObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfIntegerObject(data);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
