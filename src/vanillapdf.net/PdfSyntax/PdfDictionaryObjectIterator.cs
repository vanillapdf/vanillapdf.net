using System;
using System.Collections;
using System.Collections.Generic;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Iterator over the key-value pairs of a PDF dictionary object.
    /// </summary>
    public class PdfDictionaryObjectIterator : IDisposable, IEnumerator<KeyValuePair<PdfNameObject, PdfObject>>
    {
        internal PdfDictionaryObjectIteratorSafeHandle Handle { get; }

        internal PdfDictionaryObjectIterator(PdfDictionaryObjectIteratorSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get the key of the current dictionary entry.
        /// </summary>
        /// <returns><see cref="PdfNameObject"/> representing the key.</returns>
        public PdfNameObject GetKey()
        {
            UInt32 result = NativeMethods.DictionaryObjectIterator_GetKey(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(data);
        }

        /// <summary>
        /// Get the value of the current dictionary entry.
        /// </summary>
        /// <returns>The current entry value.</returns>
        public PdfObject GetValue()
        {
            UInt32 result = NativeMethods.DictionaryObjectIterator_GetValue(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        /// <summary>
        /// Advance the iterator to the next dictionary entry.
        /// </summary>
        public void Next()
        {
            UInt32 result = NativeMethods.DictionaryObjectIterator_Next(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Determine whether the iterator points to a valid entry.
        /// </summary>
        /// <returns><c>true</c> when the iterator is valid.</returns>
        public bool IsValid()
        {
            UInt32 result = NativeMethods.DictionaryObjectIterator_IsValid(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <inheritdoc/>

        public void Dispose()
        {
            Handle?.Dispose();
        }

        #region IEnumerator

        private bool isFirst = true;

        private KeyValuePair<PdfNameObject, PdfObject> GetCurrent()
        {
            return new KeyValuePair<PdfNameObject, PdfObject>(GetKey(), GetValue());
        }

        object IEnumerator.Current => GetCurrent();

        /// <inheritdoc/>
        public KeyValuePair<PdfNameObject, PdfObject> Current => GetCurrent();

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
