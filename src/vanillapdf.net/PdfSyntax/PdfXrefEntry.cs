using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Cross-reference entry represents item within \ref PdfXref
    /// </summary>
    public class PdfXrefEntry : PdfUnknown
    {
        internal PdfXrefEntrySafeHandle BaseEntryHandle { get; }

        internal PdfXrefEntry(PdfXrefEntrySafeHandle handle) : base(handle)
        {
            BaseEntryHandle = handle;
        }

        static PdfXrefEntry()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefEntrySafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfXrefEntryType GetEntryType()
        {
            UInt32 result = NativeMethods.XrefEntry_GetType(BaseEntryHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfXrefEntryType>.CheckedCast(data);
        }

        /// <summary>
        /// Get indirect object's object number
        /// </summary>
        /// <returns>Object number this entry is referring to on success, throws exception on failure</returns>
        public UInt64 GetObjectNumber()
        {
            UInt32 result = NativeMethods.XrefEntry_GetObjectNumber(BaseEntryHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Get indirect object's generation number
        /// </summary>
        /// <returns>Generation number this entry is referring to on success, throws exception on failure</returns>
        public UInt16 GetGenerationNumber()
        {
            UInt32 result = NativeMethods.XrefEntry_GetGenerationNumber(BaseEntryHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Quick check, if the entry is used or compressed
        /// </summary>
        /// <returns>True if the entry type is used or compressed, false if the entry is free, throws exception on failure</returns>
        public bool InUse()
        {
            UInt32 result = NativeMethods.XrefEntry_InUse(BaseEntryHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        #region PdfUnknown

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            BaseEntryHandle?.Dispose();
        }

        #endregion

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
