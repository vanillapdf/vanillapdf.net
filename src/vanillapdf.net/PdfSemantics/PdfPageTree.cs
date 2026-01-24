using System;
using vanillapdf.net.Interop;
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

        /// <summary>
        /// Insert a page into the tree at the given index.
        /// </summary>
        /// <param name="index">Zero based position where the page should be inserted.</param>
        /// <param name="data">Page object to insert.</param>
        public void InsertPage(UInt64 index, PdfPageObject data)
        {
            UInt32 result = NativeMethods.PageTree_InsertPage(Handle, new UIntPtr(index), data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Append a page to the end of the page tree.
        /// </summary>
        /// <param name="data">Page object to append.</param>
        public void AppendPage(PdfPageObject data)
        {
            UInt32 result = NativeMethods.PageTree_AppendPage(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Remove the page at the specified index from the tree.
        /// </summary>
        /// <param name="index">Zero based index of the page to remove.</param>
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
    }
}
