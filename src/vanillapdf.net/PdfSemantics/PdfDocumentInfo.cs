using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents the document information dictionary containing metadata about the PDF document.
    /// </summary>
    public class PdfDocumentInfo : PdfUnknown
    {
        internal PdfDocumentInfoSafeHandle Handle { get; }

        internal PdfDocumentInfo(PdfDocumentInfoSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfDocumentInfo()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDocumentInfoSafeHandle).TypeHandle);
        }

        // TODO: Add setters when native API is available

        /// <summary>
        /// The document's title.
        /// </summary>
        public PdfStringObject Title => GetTitle();

        /// <summary>
        /// The name of the person who created the document.
        /// </summary>
        public PdfStringObject Author => GetAuthor();

        /// <summary>
        /// The subject of the document.
        /// </summary>
        public PdfStringObject Subject => GetSubject();

        /// <summary>
        /// Keywords associated with the document.
        /// </summary>
        public PdfStringObject Keywords => GetKeywords();

        /// <summary>
        /// The name of the application that created the original document.
        /// </summary>
        public PdfStringObject Creator => GetCreator();

        /// <summary>
        /// The name of the application that produced the PDF.
        /// </summary>
        public PdfStringObject Producer => GetProducer();

        /// <summary>
        /// The date and time the document was created.
        /// </summary>
        public PdfDate CreationDate => GetCreationDate();

        /// <summary>
        /// The date and time the document was most recently modified.
        /// </summary>
        public PdfDate ModificationDate => GetModificationDate();

        #region Private Methods

        private PdfStringObject GetTitle()
        {
            UInt32 result = NativeMethods.DocumentInfo_GetTitle(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfStringObject(data);
        }

        private PdfStringObject GetAuthor()
        {
            UInt32 result = NativeMethods.DocumentInfo_GetAuthor(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfStringObject(data);
        }

        private PdfStringObject GetSubject()
        {
            UInt32 result = NativeMethods.DocumentInfo_GetSubject(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfStringObject(data);
        }

        private PdfStringObject GetKeywords()
        {
            UInt32 result = NativeMethods.DocumentInfo_GetKeywords(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfStringObject(data);
        }

        private PdfStringObject GetCreator()
        {
            UInt32 result = NativeMethods.DocumentInfo_GetCreator(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfStringObject(data);
        }

        private PdfStringObject GetProducer()
        {
            UInt32 result = NativeMethods.DocumentInfo_GetProducer(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfStringObject(data);
        }

        private PdfDate GetCreationDate()
        {
            UInt32 result = NativeMethods.DocumentInfo_GetCreationDate(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfDate(data);
        }

        private PdfDate GetModificationDate()
        {
            UInt32 result = NativeMethods.DocumentInfo_GetModificationDate(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfDate(data);
        }

        #endregion

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static GetTitleDelegate DocumentInfo_GetTitle = LibraryInstance.GetFunction<GetTitleDelegate>("DocumentInfo_GetTitle");
            public static GetAuthorDelegate DocumentInfo_GetAuthor = LibraryInstance.GetFunction<GetAuthorDelegate>("DocumentInfo_GetAuthor");
            public static GetSubjectDelegate DocumentInfo_GetSubject = LibraryInstance.GetFunction<GetSubjectDelegate>("DocumentInfo_GetSubject");
            public static GetKeywordsDelegate DocumentInfo_GetKeywords = LibraryInstance.GetFunction<GetKeywordsDelegate>("DocumentInfo_GetKeywords");
            public static GetCreatorDelegate DocumentInfo_GetCreator = LibraryInstance.GetFunction<GetCreatorDelegate>("DocumentInfo_GetCreator");
            public static GetProducerDelegate DocumentInfo_GetProducer = LibraryInstance.GetFunction<GetProducerDelegate>("DocumentInfo_GetProducer");
            public static GetCreationDateDelegate DocumentInfo_GetCreationDate = LibraryInstance.GetFunction<GetCreationDateDelegate>("DocumentInfo_GetCreationDate");
            public static GetModificationDateDelegate DocumentInfo_GetModificationDate = LibraryInstance.GetFunction<GetModificationDateDelegate>("DocumentInfo_GetModificationDate");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetTitleDelegate(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetAuthorDelegate(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetSubjectDelegate(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetKeywordsDelegate(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetCreatorDelegate(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetProducerDelegate(PdfDocumentInfoSafeHandle handle, out PdfStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetCreationDateDelegate(PdfDocumentInfoSafeHandle handle, out PdfDateSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetModificationDateDelegate(PdfDocumentInfoSafeHandle handle, out PdfDateSafeHandle data);
        }
    }
}
