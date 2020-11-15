using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfXrefChainIterator : PdfUnknown , IEnumerator<PdfXref>
    {
        internal PdfXrefChainIterator(PdfXrefChainIteratorSafeHandle handle) : base(handle)
        {
        }

        static PdfXrefChainIterator()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfXref GetValue()
        {
            UInt32 result = NativeMethods.XrefChainIterator_GetValue(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXref(data);
        }

        public void Next()
        {
            UInt32 result = NativeMethods.XrefChainIterator_Next(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public bool IsValid()
        {
            UInt32 result = NativeMethods.XrefChainIterator_IsValid(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        #region IEnumerator

        private bool isFirst = true;

        object IEnumerator.Current => GetValue();
        PdfXref IEnumerator<PdfXref>.Current => GetValue();

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
            public static GetValueDelgate XrefChainIterator_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("XrefChainIterator_GetValue");
            public static NextDelgate XrefChainIterator_Next = LibraryInstance.GetFunction<NextDelgate>("XrefChainIterator_Next");
            public static IsValidDelgate XrefChainIterator_IsValid = LibraryInstance.GetFunction<IsValidDelgate>("XrefChainIterator_IsValid");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfXrefChainIteratorSafeHandle handle, out PdfXrefSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 NextDelgate(PdfXrefChainIteratorSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IsValidDelgate(PdfXrefChainIteratorSafeHandle handle, out bool data);
        }
    }
}
