using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Used entry means that an indirect object is allocated within the PDF file and this entry points to it's offset.
    /// </summary>
    public class PdfXrefUsedEntry : PdfXrefEntry
    {
        internal PdfXrefUsedEntrySafeHandle Handle { get; }

        internal PdfXrefUsedEntry(PdfXrefUsedEntrySafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfXrefUsedEntry()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefUsedEntrySafeHandle).TypeHandle);
        }

        /// <summary>
        /// Number of bytes from the beginning of the file to the beginning of the referenced object.
        /// </summary>
        /// <returns>Offset of the object in the source document on success, throws exception on failure</returns>
        public Int64 GetOffset()
        {
            UInt32 result = NativeMethods.XrefUsedEntry_GetOffset(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Get reference to the object represented by this entry
        /// </summary>
        /// <returns>Reference to the object represented by this entry on success, throws exception on failure</returns>
        public PdfObject GetReference()
        {
            UInt32 result = NativeMethods.XrefUsedEntry_GetReference(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        /// <summary>
        /// Convert object to compressed stream entry
        /// </summary>
        /// <param name="data">Handle to \ref PdfXrefEntry to be converted</param>
        /// <returns>A new instance of \ref PdfXrefUsedEntry if the object can be converted, throws exception on failure</returns>
        public static PdfXrefUsedEntry FromEntry(PdfXrefEntry entry)
        {
            return new PdfXrefUsedEntry(entry.BaseEntryHandle);
        }

        #region PdfUnknown

        protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #endregion

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
