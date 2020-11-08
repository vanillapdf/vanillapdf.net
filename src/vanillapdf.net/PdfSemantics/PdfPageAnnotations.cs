using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;
using vanillapdf.net.Utils.SafeHandles;

namespace vanillapdf.net
{
    public class PdfPageAnnotations : PdfUnknown
    {
        internal PdfPageAnnotations(PdfPageAnnotationsSafeHandle handle) : base(handle)
        {
        }

        static PdfPageAnnotations()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public UInt64 GetSize()
        {
            UInt32 result = NativeMethods.PageAnnotations_GetSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        public PdfAnnotation At(UInt64 index)
        {
            UInt32 result = NativeMethods.PageAnnotations_At(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfAnnotation(data);
        }

        private static class NativeMethods
        {
            public static PageAnnotationsSizeDelgate PageAnnotations_GetSize = LibraryInstance.GetFunction<PageAnnotationsSizeDelgate>("PageAnnotations_GetSize");
            public static PageAnnotationsAtDelgate PageAnnotations_At = LibraryInstance.GetFunction<PageAnnotationsAtDelgate>("PageAnnotations_At");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 PageAnnotationsSizeDelgate(PdfPageAnnotationsSafeHandle handle, out UIntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 PageAnnotationsAtDelgate(PdfPageAnnotationsSafeHandle handle, UIntPtr index, out PdfAnnotationSafeHandle data);
        }
    }
}
