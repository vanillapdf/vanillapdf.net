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
    /// A pointer to \ref PdfXrefEntry within \ref PdfXref collection
    /// </summary>
    public class PdfXrefIterator : PdfUnknown, IEnumerator<PdfXrefEntry>
    {
        internal PdfXrefIterator(PdfXrefIteratorSafeHandle handle) : base(handle)
        {
        }

        static PdfXrefIterator()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefIteratorSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get cross-reference entry from current iterator position
        /// </summary>
        /// <returns>A handle to current \ref PdfXref on success, throws exception on failure</returns>
        public PdfXrefEntry GetValue()
        {
            UInt32 result = NativeMethods.XrefIterator_GetValue(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXrefEntry(data);
        }

        /// <summary>
        /// Advance the iterator to the next position
        /// </summary>
        public void Next()
        {
            UInt32 result = NativeMethods.XrefIterator_Next(Handle);
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
