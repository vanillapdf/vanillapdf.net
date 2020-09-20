using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfXrefUsedEntry : PdfXrefEntry
    {
        internal PdfXrefUsedEntry(PdfXrefUsedEntrySafeHandle handle) : base(handle)
        {
        }

        static PdfXrefUsedEntry()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public Int64 GetOffset()
        {
            UInt32 result = NativeMethods.XrefUsedEntry_GetOffset(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public PdfObject GetReference()
        {
            UInt32 result = NativeMethods.XrefUsedEntry_GetReference(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        public static PdfXrefUsedEntry FromEntry(PdfXrefEntry entry)
        {
            return new PdfXrefUsedEntry(entry.Handle);
        }

        private static class NativeMethods
        {
            public static GetOffsetDelgate XrefUsedEntry_GetOffset = LibraryInstance.GetFunction<GetOffsetDelgate>("XrefUsedEntry_GetOffset");
            public static GetReferenceDelgate XrefUsedEntry_GetReference = LibraryInstance.GetFunction<GetReferenceDelgate>("XrefUsedEntry_GetReference");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOffsetDelgate(PdfXrefEntrySafeHandle handle, out Int64 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetReferenceDelgate(PdfXrefEntrySafeHandle handle, out PdfObjectSafeHandle data);
        }
    }
}
