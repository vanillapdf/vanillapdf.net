using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;
using vanillapdf.net.Utils.SafeHandles;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfXrefFreeEntry : PdfXrefEntry
    {
        internal PdfXrefFreeEntry(PdfXrefFreeEntrySafeHandle handle) : base(handle)
        {
        }

        static PdfXrefFreeEntry()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public UInt64 GetNextFreeObjectNumber()
        {
            UInt32 result = NativeMethods.XrefFreeEntry_GetNextFreeObjectNumber(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static PdfXrefFreeEntry FromEntry(PdfXrefEntry entry)
        {
            return new PdfXrefFreeEntry(entry.Handle);
        }

        private static class NativeMethods
        {
            public static GetNextFreeObjectNumberDelgate XrefFreeEntry_GetNextFreeObjectNumber = LibraryInstance.GetFunction<GetNextFreeObjectNumberDelgate>("XrefFreeEntry_GetNextFreeObjectNumber");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetNextFreeObjectNumberDelgate(PdfXrefEntrySafeHandle handle, out UInt64 data);
        }
    }
}
