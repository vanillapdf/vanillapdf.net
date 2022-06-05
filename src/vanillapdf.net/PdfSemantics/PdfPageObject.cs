using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
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
        internal PdfPageObjectSafeHandle Handle { get; }

        internal PdfPageObject(PdfPageObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfPageObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageObjectSafeHandle).TypeHandle);
        }

        public PdfRectangle MediaBox
        {
            get { return GetMediaBox(); }
            set { SetMediaBox(value); }
        }

        public PdfDictionaryObject BaseObject
        {
            get => GetBaseObject();
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

        /// <summary>
        /// A dictionary containing any resources required by the page(see 7.8.3, "Resource Dictionaries").
        /// </summary>
        /// <returns>Handle to \ref PdfResourceDictionary object on success, throws exception on failure</returns>
        public PdfResourceDictionary GetResources()
        {
            UInt32 result = NativeMethods.PageObject_GetResources(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfResourceDictionary(data);
        }

        private PdfRectangle GetMediaBox()
        {
            UInt32 result = NativeMethods.PageObject_GetMediaBox(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfRectangle(data);
        }

        private void SetMediaBox(PdfRectangle data)
        {
            UInt32 result = NativeMethods.PageObject_SetMediaBox(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfDictionaryObject GetBaseObject()
        {
            UInt32 result = NativeMethods.PageObject_GetBaseObject(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObject(data);
        }

        protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static GetContentsDelgate PageObject_GetContents = LibraryInstance.GetFunction<GetContentsDelgate>("PageObject_GetContents");
            public static GetAnnotationsDelgate PageObject_GetAnnotations = LibraryInstance.GetFunction<GetAnnotationsDelgate>("PageObject_GetAnnotations");
            public static GetResourcesDelgate PageObject_GetResources = LibraryInstance.GetFunction<GetResourcesDelgate>("PageObject_GetResources");
            public static GetMediaBoxDelgate PageObject_GetMediaBox = LibraryInstance.GetFunction<GetMediaBoxDelgate>("PageObject_GetMediaBox");
            public static SetMediaBoxDelgate PageObject_SetMediaBox = LibraryInstance.GetFunction<SetMediaBoxDelgate>("PageObject_SetMediaBox");
            public static GetBaseObjectDelgate PageObject_GetBaseObject = LibraryInstance.GetFunction<GetBaseObjectDelgate>("PageObject_GetBaseObject");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetContentsDelgate(PdfPageObjectSafeHandle handle, out PdfPageContentsSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetAnnotationsDelgate(PdfPageObjectSafeHandle handle, out PdfPageAnnotationsSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetResourcesDelgate(PdfPageObjectSafeHandle handle, out PdfResourceDictionarySafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetMediaBoxDelgate(PdfPageObjectSafeHandle handle, out PdfRectangleSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetMediaBoxDelgate(PdfPageObjectSafeHandle handle, PdfRectangleSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetBaseObjectDelgate(PdfPageObjectSafeHandle handle, out PdfDictionaryObjectSafeHandle data);
        }
    }
}
