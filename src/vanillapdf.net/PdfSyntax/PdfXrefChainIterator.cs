using System;
using System.Collections;
using System.Collections.Generic;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// A pointer to \ref PdfXref within \ref PdfXrefChain collection
    /// </summary>
    public class PdfXrefChainIterator : PdfUnknown, IEnumerator<PdfXref>
    {
        internal PdfXrefChainIteratorSafeHandle Handle { get; }

        internal PdfXrefChainIterator(PdfXrefChainIteratorSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get cross-reference section from current iterator position
        /// </summary>
        /// <returns>A handle to current \ref PdfXref on success, throws exception on failure</returns>
        public PdfXref GetValue()
        {
            UInt32 result = NativeMethods.XrefChainIterator_GetValue(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXref(data);
        }

        /// <summary>
        /// Advance the iterator to the next position
        /// </summary>
        public void Next()
        {
            UInt32 result = NativeMethods.XrefChainIterator_Next(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Check if the current iterator position is valid
        /// </summary>
        /// <returns>True if the current iterator position is valid, false if invalid, throws exception on failure</returns>
        public bool IsValid()
        {
            UInt32 result = NativeMethods.XrefChainIterator_IsValid(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        #region PdfUnknown

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #endregion

        #region IEnumerator

        private bool isFirst = true;

        object IEnumerator.Current => GetValue();

        /// <inheritdoc/>
        public PdfXref Current => GetValue();

        /// <inheritdoc/>
        public bool MoveNext()
        {
            if (!IsValid()) {
                return false;
            }

            // HACK: Skip Next() for the first item
            if (isFirst) {
                isFirst = false;
                return true;
            }

            Next();
            return IsValid();
        }

        /// <inheritdoc/>
        public void Reset()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
