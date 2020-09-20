using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfXrefEntry : PdfUnknown
    {
        internal PdfXrefEntry(PdfXrefEntrySafeHandle handle) : base(handle)
        {
        }

        static PdfXrefEntry()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfXrefEntryType GetEntryType()
        {
            UInt32 result = NativeMethods.XrefEntry_GetType(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfXrefEntryType>.CheckedCast(data);
        }

        public UInt64 GetObjectNumber()
        {
            UInt32 result = NativeMethods.XrefEntry_GetObjectNumber(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public UInt16 GetGenerationNumber()
        {
            UInt32 result = NativeMethods.XrefEntry_GetGenerationNumber(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public bool InUse()
        {
            UInt32 result = NativeMethods.XrefEntry_InUse(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private static class NativeMethods
        {
            public static GetTypeDelgate XrefEntry_GetType = LibraryInstance.GetFunction<GetTypeDelgate>("XrefEntry_GetType");
            public static GetObjectNumberDelgate XrefEntry_GetObjectNumber = LibraryInstance.GetFunction<GetObjectNumberDelgate>("XrefEntry_GetObjectNumber");
            public static GetGenerationNumberDelgate XrefEntry_GetGenerationNumber = LibraryInstance.GetFunction<GetGenerationNumberDelgate>("XrefEntry_GetGenerationNumber");
            public static InUseDelgate XrefEntry_InUse = LibraryInstance.GetFunction<InUseDelgate>("XrefEntry_InUse");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetTypeDelgate(PdfXrefEntrySafeHandle handle, out PdfXrefEntryType data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetObjectNumberDelgate(PdfXrefEntrySafeHandle handle, out UInt64 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetGenerationNumberDelgate(PdfXrefEntrySafeHandle handle, out UInt16 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 InUseDelgate(PdfXrefEntrySafeHandle handle, out bool data);
        }
    }
}
