using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfXrefCompressedEntry : PdfXrefEntry
    {
        internal PdfXrefCompressedEntry(PdfXrefUsedEntrySafeHandle handle) : base(handle)
        {
        }

        static PdfXrefCompressedEntry()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfObject GetReference()
        {
            UInt32 result = NativeMethods.XrefCompressedEntry_GetReference(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        public UInt64 GetIndex()
        {
            UInt32 result = NativeMethods.XrefCompressedEntry_GetIndex(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        public UInt64 GetObjectStreamNumber()
        {
            UInt32 result = NativeMethods.XrefCompressedEntry_GetObjectStreamNumber(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static PdfXrefCompressedEntry FromEntry(PdfXrefEntry entry)
        {
            return new PdfXrefCompressedEntry(entry.Handle);
        }

        private static class NativeMethods
        {
            public static GetReferenceDelgate XrefCompressedEntry_GetReference = LibraryInstance.GetFunction<GetReferenceDelgate>("XrefCompressedEntry_GetReference");
            public static GetIndexDelgate XrefCompressedEntry_GetIndex = LibraryInstance.GetFunction<GetIndexDelgate>("XrefCompressedEntry_GetIndex");
            public static GetObjectStreamNumberDelgate XrefCompressedEntry_GetObjectStreamNumber = LibraryInstance.GetFunction<GetObjectStreamNumberDelgate>("XrefCompressedEntry_GetObjectStreamNumber");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetReferenceDelgate(PdfXrefEntrySafeHandle handle, out PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetIndexDelgate(PdfXrefEntrySafeHandle handle, out UIntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetObjectStreamNumberDelgate(PdfXrefEntrySafeHandle handle, out UInt64 data);
        }
    }
}
