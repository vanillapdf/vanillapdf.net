using System;
using System.Collections;
using System.Collections.Generic;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// The cross-reference table contains information that permits random access to indirect objects within the file
    /// </summary>
    public class PdfXref : PdfUnknown, IEnumerable<PdfXrefEntry>
    {
        internal PdfXrefSafeHandle Handle { get; }

        internal PdfXref(PdfXrefSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get cross-reference table meta-data dictionary
        /// </summary>
        /// <returns>Handle to dictionary associated with cross-reference section on success, throws exception on failure</returns>
        public PdfDictionaryObject GetTrailerDictionary()
        {
            UInt32 result = NativeMethods.Xref_GetTrailerDictionary(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObject(value);
        }

        /// <summary>
        /// Get byte offset in the decoded stream from the beginning of the file to the beginning of the xref keyword in the last cross-reference section.
        /// </summary>
        /// <returns>Offset of the cross-reference section on success, throws exception on failure</returns>
        public Int64 GetLastXrefOffset()
        {
            UInt32 result = NativeMethods.Xref_GetLastXrefOffset(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return value;
        }

        /// <summary>
        /// Get cross-reference entry iterator
        /// </summary>
        /// <returns>Handle to iterator for enumerating cross-reference entries on success, throws exception on failure</returns>
        public PdfXrefIterator GetIterator()
        {
            UInt32 result = NativeMethods.Xref_GetIterator(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXrefIterator(value);
        }

        #region PdfUnknown

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #endregion

        #region IEnumerable<PdfXref>

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        public IEnumerator<PdfXrefEntry> GetEnumerator()
        {
            return GetIterator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetIterator();
        }

        #endregion
    }
}
