using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfXref : PdfUnknown
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
            UInt32 result = NativeMethods.Xref_TrailerDictionary(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObject(value);
        }

        public Int64 GetLastXrefOffset()
        {
            UInt32 result = NativeMethods.Xref_LastXrefOffset(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return value;
        }

        public PdfXrefIterator GetIterator()
        {
            UInt32 result = NativeMethods.Xref_Iterator(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXrefIterator(value);
        }

        public bool IsIteratorValid(PdfXrefIterator iterator)
        {
            UInt32 result = NativeMethods.Xref_IsIteratorValid(Handle, iterator.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private static class NativeMethods
        {
            public static XrefTrailerDictionaryDelgate Xref_TrailerDictionary = LibraryInstance.GetFunction<XrefTrailerDictionaryDelgate>("Xref_TrailerDictionary");
            public static LastXrefOffsetDelgate Xref_LastXrefOffset = LibraryInstance.GetFunction<LastXrefOffsetDelgate>("Xref_LastXrefOffset");
            public static IteratorDelgate Xref_Iterator = LibraryInstance.GetFunction<IteratorDelgate>("Xref_Iterator");
            public static IsIteratorValidDelgate Xref_IsIteratorValid = LibraryInstance.GetFunction<IsIteratorValidDelgate>("Xref_IsIteratorValid");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 XrefTrailerDictionaryDelgate(PdfXrefSafeHandle handle, out PdfDictionaryObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LastXrefOffsetDelgate(PdfXrefSafeHandle handle, out Int64 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IteratorDelgate(PdfXrefSafeHandle handle, out PdfXrefIteratorSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IsIteratorValidDelgate(PdfXrefSafeHandle handle, PdfXrefIteratorSafeHandle iterator_handle, out bool data);
        }
    }
}
