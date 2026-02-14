using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Iterator for traversing all entries in a destination name tree.
    /// </summary>
    public class PdfDestinationNameTreeIterator : IDisposable
    {
        internal PdfDestinationNameTreeIteratorSafeHandle Handle { get; }

        internal PdfDestinationNameTreeIterator(PdfDestinationNameTreeIteratorSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get the key (name) at the current iterator position.
        /// </summary>
        public PdfStringObject Key
        {
            get
            {
                UInt32 result = NativeMethods.DestinationNameTreeIterator_GetKey(Handle, out var data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfStringObject(data);
            }
        }

        /// <summary>
        /// Get the value (destination) at the current iterator position.
        /// </summary>
        public PdfDestination Value
        {
            get
            {
                UInt32 result = NativeMethods.DestinationNameTreeIterator_GetValue(Handle, out var data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfDestination(data);
            }
        }

        /// <summary>
        /// Determine if the current position is valid.
        /// Invalid position may mean the iterator moved past the end
        /// or the collection was modified.
        /// </summary>
        public bool IsValid
        {
            get
            {
                UInt32 result = NativeMethods.DestinationNameTreeIterator_IsValid(Handle, out bool valid);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return valid;
            }
        }

        /// <summary>
        /// Advance the iterator to the next position.
        /// </summary>
        public void Next()
        {
            UInt32 result = NativeMethods.DestinationNameTreeIterator_Next(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
