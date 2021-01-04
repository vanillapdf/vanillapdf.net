using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Free entry means, that this object is not used in the document.
    /// It can be reused in the new cross-reference section.
    /// </summary>
    public class PdfXrefFreeEntry : PdfXrefEntry
    {
        internal PdfXrefFreeEntry(PdfXrefFreeEntrySafeHandle handle) : base(handle)
        {
        }

        static PdfXrefFreeEntry()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefFreeEntrySafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get object number of the next free object
        /// </summary>
        /// <returns>Object number of the next free object on success, throws exception on failure</returns>
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
