using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Iterator over the key-value pairs of a PDF dictionary object.
    /// </summary>
    public class PdfDictionaryObjectIterator : PdfUnknown, IEnumerator<KeyValuePair<PdfNameObject, PdfObject>>
    {
        internal PdfDictionaryObjectIteratorSafeHandle Handle { get; }

        internal PdfDictionaryObjectIterator(PdfDictionaryObjectIteratorSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfDictionaryObjectIterator()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDictionaryObjectIteratorSafeHandle).TypeHandle);
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

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
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

        private static class NativeMethods
        {
            public static GetKeyDelgate DictionaryObjectIterator_GetKey = LibraryInstance.GetFunction<GetKeyDelgate>("DictionaryObjectIterator_GetKey");
            public static GetValueDelgate DictionaryObjectIterator_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("DictionaryObjectIterator_GetValue");
            public static NextDelgate DictionaryObjectIterator_Next = LibraryInstance.GetFunction<NextDelgate>("DictionaryObjectIterator_Next");
            public static IsValidDelgate DictionaryObjectIterator_IsValid = LibraryInstance.GetFunction<IsValidDelgate>("DictionaryObjectIterator_IsValid");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetKeyDelgate(PdfDictionaryObjectIteratorSafeHandle handle, out PdfNameObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfDictionaryObjectIteratorSafeHandle handle, out PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 NextDelgate(PdfDictionaryObjectIteratorSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IsValidDelgate(PdfDictionaryObjectIteratorSafeHandle handle, out bool data);
        }
    }
}
