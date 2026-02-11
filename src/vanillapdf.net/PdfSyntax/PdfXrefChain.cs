using System;
using System.Collections;
using System.Collections.Generic;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// An ordered collection of all \ref PdfXref within the PDF file.
    /// </summary>
    public class PdfXrefChain : IDisposable, IEnumerable<PdfXref>
    {
        internal PdfXrefChainSafeHandle Handle { get; }

        internal PdfXrefChain(PdfXrefChainSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get cross-reference section iterator
        /// </summary>
        /// <returns>Handle to iterator for enumerating cross-reference sections on success, throws exception on failure</returns>
        public PdfXrefChainIterator GetIterator()
        {
            UInt32 result = NativeMethods.XrefChain_GetIterator(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXrefChainIterator(value);
        }

        public void Dispose()
        {
            Handle?.Dispose();
        }

        #region IEnumerable<PdfXref>

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        public IEnumerator<PdfXref> GetEnumerator()
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
