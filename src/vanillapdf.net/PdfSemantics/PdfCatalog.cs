using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The root of a document's object hierarchy.
    /// </summary>
    public class PdfCatalog : PdfUnknown
    {
        internal PdfCatalogSafeHandle Handle { get; }

        internal PdfCatalog(PdfCatalogSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfCatalog()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfCatalogSafeHandle).TypeHandle);
        }

        /// <summary>
        /// The version of the PDF specification to which the document conforms
        /// if later than the version specified in the file's header.
        /// 
        /// If the header specifies a later version, or if this entry is absent,
        /// the document shall conform to the version specified in the header.
        /// </summary>
        public PdfVersion? Version
        {
            get { return GetVersion(); }
        }

        private PdfVersion? GetVersion()
        {
            UInt32 result = NativeMethods.Catalog_GetVersion(Handle, out int data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfVersion>.CheckedCast(data);
        }

        /// <summary>
        /// The root of the document's page tree (see 7.7.3, "Page Tree").
        /// </summary>
        /// <returns>Handle to a \ref PdfPageTree on success, throws exception on failure</returns>
        public PdfPageTree GetPages()
        {
            UInt32 result = NativeMethods.Catalog_GetPages(Handle, out PdfPageTreeSafeHandle data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfPageTree(data);
        }

        /// <summary>
        /// Retrieve the outline hierarchy of the document.
        /// </summary>
        /// <returns>The root <see cref="PdfOutline"/> object or <c>null</c> if none exists.</returns>
        public PdfOutline GetOutlines()
        {
            UInt32 result = NativeMethods.Catalog_GetOutlines(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfOutline(data);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static CatalogGetPagesDelgate Catalog_GetPages = LibraryInstance.GetFunction<CatalogGetPagesDelgate>("Catalog_GetPages");
            public static CatalogGetVersionDelgate Catalog_GetVersion = LibraryInstance.GetFunction<CatalogGetVersionDelgate>("Catalog_GetVersion");
            public static GetOutlinesDelgate Catalog_GetOutlines = LibraryInstance.GetFunction<GetOutlinesDelgate>("Catalog_GetOutlines");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CatalogGetPagesDelgate(PdfCatalogSafeHandle handle, out PdfPageTreeSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CatalogGetVersionDelgate(PdfCatalogSafeHandle handle, out int data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOutlinesDelgate(PdfCatalogSafeHandle handle, out PdfOutlineSafeHandle data);
        }
    }
}
