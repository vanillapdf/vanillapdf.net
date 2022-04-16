using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfDictionaryObjectIterator : PdfUnknown , IEnumerator<KeyValuePair<PdfNameObject, PdfObject>>
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

        public PdfNameObject GetKey()
        {
            UInt32 result = NativeMethods.DictionaryObjectIterator_GetKey(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(data);
        }

        public PdfObject GetValue()
        {
            UInt32 result = NativeMethods.DictionaryObjectIterator_GetValue(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        public void Next()
        {
            UInt32 result = NativeMethods.DictionaryObjectIterator_Next(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public bool IsValid()
        {
            UInt32 result = NativeMethods.DictionaryObjectIterator_IsValid(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        protected override void DisposeCustomHandle()
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
        KeyValuePair<PdfNameObject, PdfObject> IEnumerator<KeyValuePair<PdfNameObject, PdfObject>>.Current => GetCurrent();

        bool IEnumerator.MoveNext()
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

        void IEnumerator.Reset()
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
