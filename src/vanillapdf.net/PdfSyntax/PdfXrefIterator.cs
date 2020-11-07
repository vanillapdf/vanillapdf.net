using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;
using vanillapdf.net.Utils.SafeHandles;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfXrefIterator : PdfUnknown, IEnumerator<PdfXrefEntry>
    {
        internal PdfXrefIterator(PdfXrefIteratorSafeHandle handle) : base(handle)
        {
        }

        static PdfXrefIterator()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfXrefEntry GetValue()
        {
            UInt32 result = NativeMethods.XrefIterator_GetValue(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXrefEntry(data);
        }

        public void Next()
        {
            UInt32 result = NativeMethods.XrefIterator_Next(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public bool IsValid()
        {
            UInt32 result = NativeMethods.XrefIterator_IsValid(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        #region IEnumerator

        private bool isFirst = true;

        object IEnumerator.Current => GetValue();
        PdfXrefEntry IEnumerator<PdfXrefEntry>.Current => GetValue();

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
            public static GetValueDelgate XrefIterator_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("XrefIterator_GetValue");
            public static NextDelgate XrefIterator_Next = LibraryInstance.GetFunction<NextDelgate>("XrefIterator_Next");
            public static IsValidDelgate XrefIterator_IsValid = LibraryInstance.GetFunction<IsValidDelgate>("XrefIterator_IsValid");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfXrefIteratorSafeHandle handle, out PdfXrefEntrySafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 NextDelgate(PdfXrefIteratorSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IsValidDelgate(PdfXrefIteratorSafeHandle handle, out bool data);
        }
    }
}
