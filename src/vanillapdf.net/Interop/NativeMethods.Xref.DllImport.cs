#if NETSTANDARD2_0

using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.Interop
{
    internal static partial class NativeMethods
    {
        #region Xref

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Xref_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Xref_ToUnknown(PdfXrefSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Xref_FromUnknown(PdfUnknownSafeHandle handle, out PdfXrefSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Xref_GetTrailerDictionary(PdfXrefSafeHandle handle, out PdfDictionaryObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Xref_GetLastXrefOffset(PdfXrefSafeHandle handle, out Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Xref_GetIterator(PdfXrefSafeHandle handle, out PdfXrefIteratorSafeHandle data);

        #endregion

        #region XrefIterator

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefIterator_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefIterator_ToUnknown(PdfXrefIteratorSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefIterator_FromUnknown(PdfUnknownSafeHandle handle, out PdfXrefIteratorSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefIterator_GetValue(PdfXrefIteratorSafeHandle handle, out PdfXrefEntrySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefIterator_Next(PdfXrefIteratorSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefIterator_IsValid(PdfXrefIteratorSafeHandle handle, out bool data);

        #endregion

        #region XrefChain

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefChain_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefChain_ToUnknown(PdfXrefChainSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefChain_FromUnknown(PdfUnknownSafeHandle handle, out PdfXrefChainSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefChain_GetIterator(PdfXrefChainSafeHandle handle, out PdfXrefChainIteratorSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefChain_GetCurrentXref(PdfXrefChainSafeHandle handle, out PdfXrefSafeHandle data);

        #endregion

        #region XrefChainIterator

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefChainIterator_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefChainIterator_ToUnknown(PdfXrefChainIteratorSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefChainIterator_FromUnknown(PdfUnknownSafeHandle handle, out PdfXrefChainIteratorSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefChainIterator_GetValue(PdfXrefChainIteratorSafeHandle handle, out PdfXrefSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefChainIterator_Next(PdfXrefChainIteratorSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefChainIterator_IsValid(PdfXrefChainIteratorSafeHandle handle, out bool data);

        #endregion

        #region XrefEntry

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefEntry_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefEntry_ToUnknown(PdfXrefEntrySafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefEntry_FromUnknown(PdfUnknownSafeHandle handle, out PdfXrefEntrySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefEntry_GetType(PdfXrefEntrySafeHandle handle, out PdfSyntax.PdfXrefEntryType data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefEntry_GetObjectNumber(PdfXrefEntrySafeHandle handle, out UInt64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefEntry_GetGenerationNumber(PdfXrefEntrySafeHandle handle, out UInt16 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefEntry_InUse(PdfXrefEntrySafeHandle handle, out bool data);

        #endregion

        #region XrefFreeEntry

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefFreeEntry_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefFreeEntry_ToEntry(PdfXrefFreeEntrySafeHandle handle, out PdfXrefEntrySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefFreeEntry_FromEntry(PdfXrefEntrySafeHandle handle, out PdfXrefFreeEntrySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefFreeEntry_GetNextFreeObjectNumber(PdfXrefFreeEntrySafeHandle handle, out UInt64 data);

        #endregion

        #region XrefUsedEntry

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefUsedEntry_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefUsedEntry_ToEntry(PdfXrefUsedEntrySafeHandle handle, out PdfXrefEntrySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefUsedEntry_FromEntry(PdfXrefEntrySafeHandle handle, out PdfXrefUsedEntrySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefUsedEntry_GetOffset(PdfXrefUsedEntrySafeHandle handle, out UInt64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefUsedEntry_GetReferencedObject(PdfXrefUsedEntrySafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefUsedEntry_SetReferencedObject(PdfXrefUsedEntrySafeHandle handle, PdfObjectSafeHandle data);

        #endregion

        #region XrefCompressedEntry

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefCompressedEntry_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefCompressedEntry_ToEntry(PdfXrefCompressedEntrySafeHandle handle, out PdfXrefEntrySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefCompressedEntry_FromEntry(PdfXrefEntrySafeHandle handle, out PdfXrefCompressedEntrySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefCompressedEntry_GetObjectStreamNumber(PdfXrefCompressedEntrySafeHandle handle, out UInt64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 XrefCompressedEntry_GetObjectIndexWithinStream(PdfXrefCompressedEntrySafeHandle handle, out UInt64 data);

        #endregion
    }
}

#endif
