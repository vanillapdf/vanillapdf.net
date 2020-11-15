using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfXref : PdfUnknown, IEnumerable<PdfXrefEntry>
    {
        internal PdfXref(PdfXrefSafeHandle handle) : base(handle)
        {
        }

        static PdfXref()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfDictionaryObject GetTrailerDictionary()
        {
            UInt32 result = NativeMethods.Xref_GetTrailerDictionary(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObject(value);
        }

        public Int64 GetLastXrefOffset()
        {
            UInt32 result = NativeMethods.Xref_GetLastXrefOffset(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return value;
        }

        public PdfXrefIterator GetIterator()
        {
            UInt32 result = NativeMethods.Xref_GetIterator(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXrefIterator(value);
        }

        #region IEnumerable<PdfXref>

        IEnumerator<PdfXrefEntry> IEnumerable<PdfXrefEntry>.GetEnumerator()
        {
            return GetIterator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetIterator();
        }

        #endregion

        private static class NativeMethods
        {
            public static GetTrailerDictionaryDelgate Xref_GetTrailerDictionary = LibraryInstance.GetFunction<GetTrailerDictionaryDelgate>("Xref_GetTrailerDictionary");
            public static GetLastXrefOffsetDelgate Xref_GetLastXrefOffset = LibraryInstance.GetFunction<GetLastXrefOffsetDelgate>("Xref_GetLastXrefOffset");
            public static IteratorDelgate Xref_GetIterator = LibraryInstance.GetFunction<IteratorDelgate>("Xref_GetIterator");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetTrailerDictionaryDelgate(PdfXrefSafeHandle handle, out PdfDictionaryObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetLastXrefOffsetDelgate(PdfXrefSafeHandle handle, out Int64 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IteratorDelgate(PdfXrefSafeHandle handle, out PdfXrefIteratorSafeHandle data);
        }
    }
}
