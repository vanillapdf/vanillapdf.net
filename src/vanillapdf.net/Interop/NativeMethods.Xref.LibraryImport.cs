#if NET7_0_OR_GREATER

using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.Interop
{
    internal static partial class NativeMethods
    {
        #region Xref

        [LibraryImport(LibraryName)]
        public static partial UInt32 Xref_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Xref_ToUnknown(PdfXrefSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Xref_FromUnknown(PdfUnknownSafeHandle handle, out PdfXrefSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Xref_GetTrailerDictionary(PdfXrefSafeHandle handle, out PdfDictionaryObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Xref_GetLastXrefOffset(PdfXrefSafeHandle handle, out Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Xref_GetIterator(PdfXrefSafeHandle handle, out PdfXrefIteratorSafeHandle data);

        #endregion

        #region XrefIterator

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefIterator_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefIterator_ToUnknown(PdfXrefIteratorSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefIterator_FromUnknown(PdfUnknownSafeHandle handle, out PdfXrefIteratorSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefIterator_GetValue(PdfXrefIteratorSafeHandle handle, out PdfXrefEntrySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefIterator_Next(PdfXrefIteratorSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefIterator_IsValid(PdfXrefIteratorSafeHandle handle, [MarshalAs(UnmanagedType.U1)] out bool data);

        #endregion

        #region XrefChain

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefChain_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefChain_ToUnknown(PdfXrefChainSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefChain_FromUnknown(PdfUnknownSafeHandle handle, out PdfXrefChainSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefChain_GetIterator(PdfXrefChainSafeHandle handle, out PdfXrefChainIteratorSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefChain_GetCurrentXref(PdfXrefChainSafeHandle handle, out PdfXrefSafeHandle data);

        #endregion

        #region XrefChainIterator

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefChainIterator_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefChainIterator_ToUnknown(PdfXrefChainIteratorSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefChainIterator_FromUnknown(PdfUnknownSafeHandle handle, out PdfXrefChainIteratorSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefChainIterator_GetValue(PdfXrefChainIteratorSafeHandle handle, out PdfXrefSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefChainIterator_Next(PdfXrefChainIteratorSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefChainIterator_IsValid(PdfXrefChainIteratorSafeHandle handle, [MarshalAs(UnmanagedType.U1)] out bool data);

        #endregion

        #region XrefEntry

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefEntry_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefEntry_ToUnknown(PdfXrefEntrySafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefEntry_FromUnknown(PdfUnknownSafeHandle handle, out PdfXrefEntrySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefEntry_GetType(PdfXrefEntrySafeHandle handle, out PdfSyntax.PdfXrefEntryType data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefEntry_GetObjectNumber(PdfXrefEntrySafeHandle handle, out UInt64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefEntry_GetGenerationNumber(PdfXrefEntrySafeHandle handle, out UInt16 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefEntry_InUse(PdfXrefEntrySafeHandle handle, [MarshalAs(UnmanagedType.U1)] out bool data);

        #endregion

        #region XrefFreeEntry

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefFreeEntry_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefFreeEntry_ToEntry(PdfXrefFreeEntrySafeHandle handle, out PdfXrefEntrySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefFreeEntry_FromEntry(PdfXrefEntrySafeHandle handle, out PdfXrefFreeEntrySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefFreeEntry_GetNextFreeObjectNumber(PdfXrefFreeEntrySafeHandle handle, out UInt64 data);

        #endregion

        #region XrefUsedEntry

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefUsedEntry_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefUsedEntry_ToEntry(PdfXrefUsedEntrySafeHandle handle, out PdfXrefEntrySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefUsedEntry_FromEntry(PdfXrefEntrySafeHandle handle, out PdfXrefUsedEntrySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefUsedEntry_GetOffset(PdfXrefUsedEntrySafeHandle handle, out UInt64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefUsedEntry_GetReferencedObject(PdfXrefUsedEntrySafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefUsedEntry_SetReferencedObject(PdfXrefUsedEntrySafeHandle handle, PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefUsedEntry_GetReference(PdfXrefUsedEntrySafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefUsedEntry_SetReference(PdfXrefUsedEntrySafeHandle handle, PdfObjectSafeHandle data);

        #endregion

        #region XrefCompressedEntry

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefCompressedEntry_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefCompressedEntry_ToEntry(PdfXrefCompressedEntrySafeHandle handle, out PdfXrefEntrySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefCompressedEntry_FromEntry(PdfXrefEntrySafeHandle handle, out PdfXrefCompressedEntrySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefCompressedEntry_GetObjectStreamNumber(PdfXrefCompressedEntrySafeHandle handle, out UInt64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefCompressedEntry_GetObjectIndexWithinStream(PdfXrefCompressedEntrySafeHandle handle, out UInt64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefCompressedEntry_GetReference(PdfXrefCompressedEntrySafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefCompressedEntry_SetReference(PdfXrefCompressedEntrySafeHandle handle, PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 XrefCompressedEntry_GetIndex(PdfXrefCompressedEntrySafeHandle handle, out UIntPtr data);

        #endregion
    }
}

#endif
