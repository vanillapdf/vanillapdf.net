using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The pages of a document are accessed through a structure known as the page tree,
    /// which defines the ordering of pages in the document.
    /// </summary>
    public class PdfPageTree : PdfUnknown
    {
        internal PdfPageTreeSafeHandle Handle { get; }

        internal PdfPageTree(PdfPageTreeSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfPageTree()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageTreeSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get number of pages in the current \ref PdfPageTree
        /// </summary>
        /// <returns>Number of pages on success, throws exception on failure</returns>
        public UInt64 GetPageCount()
        {
            UInt32 result = NativeMethods.PageTree_GetPageCount(Handle, out UIntPtr count);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return count.ToUInt64();
        }

        /// <summary>
        /// Get \ref PdfPageObject at index in the current \ref PdfPageTree
        /// </summary>
        /// <param name="index">Index of \ref PdfPageObject to be returned</param>
        /// <returns>Handle to \ref PdfPageObject at <p>index</p> on success, throws exception on failure</returns>
        public PdfPageObject GetPage(UInt64 index)
        {
            UInt32 result = NativeMethods.PageTree_GetPage(Handle, new UIntPtr(index), out PdfPageObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfPageObject(data);
        }

        public void InsertPage(UInt64 index, PdfPageObject data)
        {
            UInt32 result = NativeMethods.PageTree_InsertPage(Handle, new UIntPtr(index), data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void AppendPage(PdfPageObject data)
        {
            UInt32 result = NativeMethods.PageTree_AppendPage(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void RemovePage(UInt64 index)
        {
            UInt32 result = NativeMethods.PageTree_RemovePage(Handle, new UIntPtr(index));
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static PageTreeGetPageDelgate PageTree_GetPage = LibraryInstance.GetFunction<PageTreeGetPageDelgate>("PageTree_GetPage");
            public static PageTreeGetPageCountDelgate PageTree_GetPageCount = LibraryInstance.GetFunction<PageTreeGetPageCountDelgate>("PageTree_GetPageCount");
            public static InsertPageDelgate PageTree_InsertPage = LibraryInstance.GetFunction<InsertPageDelgate>("PageTree_InsertPage");
            public static AppendPageDelgate PageTree_AppendPage = LibraryInstance.GetFunction<AppendPageDelgate>("PageTree_AppendPage");
            public static RemovePageDelgate PageTree_RemovePage = LibraryInstance.GetFunction<RemovePageDelgate>("PageTree_RemovePage");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 PageTreeGetPageDelgate(PdfPageTreeSafeHandle handle, UIntPtr at, out PdfPageObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 PageTreeGetPageCountDelgate(PdfPageTreeSafeHandle handle, out UIntPtr count);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 InsertPageDelgate(PdfPageTreeSafeHandle handle, UIntPtr at, PdfPageObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 AppendPageDelgate(PdfPageTreeSafeHandle handle, PdfPageObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 RemovePageDelgate(PdfPageTreeSafeHandle handle, UIntPtr at);
        }
    }
}
