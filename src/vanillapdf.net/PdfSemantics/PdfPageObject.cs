using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The leaves of the page tree are page objects,
    /// each of which is a dictionary specifying the
    /// attributes of a single page of the document.
    /// </summary>
    public class PdfPageObject : PdfUnknown
    {
        internal PdfPageObject(PdfPageObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfPageObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageObjectSafeHandle).TypeHandle);
        }

        /// <summary>
        /// A content stream (see 7.8.2, "Content Streams") that shall describe
        /// the contents of this page. If this entry is absent, the page shall be empty.
        /// </summary>
        /// <returns>Handle to \ref PdfPageContents object on success, throws exception on failure</returns>
        public PdfPageContents GetContents()
        {
            UInt32 result = NativeMethods.PageObject_GetContents(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfPageContents(data);
        }

        /// <summary>
        /// An array of annotation dictionaries that shall contain
        /// indirect references to all annotations associated
        /// with the page (see 12.5, "Annotations").
        /// </summary>
        /// <returns>Handle to \ref PdfPageAnnotations object on success, throws exception on failure</returns>
        public PdfPageAnnotations GetAnnotations()
        {
            UInt32 result = NativeMethods.PageObject_GetAnnotations(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfPageAnnotations(data);
        }

        private static class NativeMethods
        {
            public static PageObjectGetContentsDelgate PageObject_GetContents = LibraryInstance.GetFunction<PageObjectGetContentsDelgate>("PageObject_GetContents");
            public static PageObjectGetAnnotationsDelgate PageObject_GetAnnotations = LibraryInstance.GetFunction<PageObjectGetAnnotationsDelgate>("PageObject_GetAnnotations");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 PageObjectGetContentsDelgate(PdfPageObjectSafeHandle handle, out PdfPageContentsSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 PageObjectGetAnnotationsDelgate(PdfPageObjectSafeHandle handle, out PdfPageAnnotationsSafeHandle data);
        }
    }
}
