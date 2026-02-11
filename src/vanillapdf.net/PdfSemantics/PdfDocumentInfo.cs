using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents the document information dictionary containing metadata about the PDF document.
    /// </summary>
    public class PdfDocumentInfo : IDisposable
    {
        internal PdfDocumentInfoSafeHandle Handle { get; }

        internal PdfDocumentInfo(PdfDocumentInfoSafeHandle handle)
        {
            Handle = handle;
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

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
