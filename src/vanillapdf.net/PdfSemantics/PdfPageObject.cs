using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    public class PdfPageObject : PdfUnknown
    {
        internal PdfPageObject(PdfPageObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfPageObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfPageContents GetContents()
        {
            UInt32 result = NativeMethods.PageObject_GetContents(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfPageContents(data);
        }

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
