﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// An array of annotation dictionaries that shall contain indirect
    /// references to all \ref PdfAnnotation associated with the page.
    /// </summary>
    public class PdfPageAnnotations : PdfUnknown
    {
        internal PdfPageAnnotationsSafeHandle Handle { get; }

        internal PdfPageAnnotations(PdfPageAnnotationsSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfPageAnnotations()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageAnnotationsSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get number of page annotations in the current \ref PdfPageObject
        /// </summary>
        /// <returns>Number of page annotations on success, throws exception on failure</returns>
        public UInt64 GetSize()
        {
            UInt32 result = NativeMethods.PageAnnotations_GetSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get page annotation at index in the current \ref PdfPageObject
        /// </summary>
        /// <param name="index">Index of page annotation to be returned</param>
        /// <returns>Handle to page annotation object at <p>index</p> on success, throws exception on failure</returns>
        public PdfAnnotation At(UInt64 index)
        {
            UInt32 result = NativeMethods.PageAnnotations_At(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfAnnotation(data);
        }

        protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
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
