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
        internal PdfPageTree(PdfPageTreeSafeHandle handle) : base(handle)
        {
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

        private static class NativeMethods
        {
            public static PageTreeGetPageDelgate PageTree_GetPage = LibraryInstance.GetFunction<PageTreeGetPageDelgate>("PageTree_GetPage");
            public static PageTreeGetPageCountDelgate PageTree_GetPageCount = LibraryInstance.GetFunction<PageTreeGetPageCountDelgate>("PageTree_GetPageCount");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 PageTreeGetPageDelgate(PdfPageTreeSafeHandle handle, UIntPtr at, out PdfPageObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 PageTreeGetPageCountDelgate(PdfPageTreeSafeHandle handle, out UIntPtr count);
        }
    }
}
