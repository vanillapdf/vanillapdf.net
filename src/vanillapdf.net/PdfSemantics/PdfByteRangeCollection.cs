using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// An ordered collection of byte ranges describing the exact portions
    /// of a PDF file covered by a digital signature digest.
    /// </summary>
    public class PdfByteRangeCollection : IDisposable
    {
        internal PdfByteRangeCollectionSafeHandle Handle { get; }

        internal PdfByteRangeCollection(PdfByteRangeCollectionSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get the number of byte ranges in the collection.
        /// </summary>
        /// <returns>The byte range count.</returns>
        public UInt64 GetSize()
        {
            UInt32 result = NativeMethods.ByteRangeCollection_GetSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get the byte range at the specified index.
        /// </summary>
        /// <param name="index">Zero-based index of the byte range to retrieve.</param>
        /// <returns>The <see cref="PdfByteRange"/> at <paramref name="index"/>.</returns>
        public PdfByteRange GetValueAt(UInt64 index)
        {
            UInt32 result = NativeMethods.ByteRangeCollection_GetValue(Handle, new UIntPtr(index), out PdfByteRangeSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfByteRange(data);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
