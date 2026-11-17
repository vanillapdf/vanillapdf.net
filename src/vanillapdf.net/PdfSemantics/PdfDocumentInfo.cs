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

        /// <summary>
        /// The document's title.
        /// </summary>
        public PdfStringObject Title
        {
            get => GetTitle();
            set => SetTitle(value);
        }

        /// <summary>
        /// The name of the person who created the document.
        /// </summary>
        public PdfStringObject Author
        {
            get => GetAuthor();
            set => SetAuthor(value);
        }

        /// <summary>
        /// The subject of the document.
        /// </summary>
        public PdfStringObject Subject
        {
            get => GetSubject();
            set => SetSubject(value);
        }

        /// <summary>
        /// Keywords associated with the document.
        /// </summary>
        public PdfStringObject Keywords
        {
            get => GetKeywords();
            set => SetKeywords(value);
        }

        /// <summary>
        /// The name of the application that created the original document.
        /// </summary>
        public PdfStringObject Creator
        {
            get => GetCreator();
            set => SetCreator(value);
        }

        /// <summary>
        /// The name of the application that produced the PDF.
        /// </summary>
        public PdfStringObject Producer
        {
            get => GetProducer();
            set => SetProducer(value);
        }

        /// <summary>
        /// The date and time the document was created.
        /// </summary>
        public PdfDate CreationDate
        {
            get => GetCreationDate();
            set => SetCreationDate(value);
        }

        /// <summary>
        /// The date and time the document was most recently modified.
        /// </summary>
        public PdfDate ModificationDate
        {
            get => GetModificationDate();
            set => SetModificationDate(value);
        }

        /// <summary>
        /// Create an empty document information dictionary registered as an indirect object within the document.
        /// </summary>
        /// <remarks>
        /// The dictionary does not take effect until it is attached with <see cref="PdfDocument.SetDocumentInfo"/>.
        /// </remarks>
        /// <param name="document">The document that owns the new dictionary.</param>
        /// <returns>A new <see cref="PdfDocumentInfo"/> instance.</returns>
        public static PdfDocumentInfo CreateFromDocument(PdfDocument document)
        {
            UInt32 result = NativeMethods.DocumentInfo_CreateFromDocument(document.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDocumentInfo(data);
        }

        /// <summary>
        /// Get whether the document has been modified to include trapping information.
        /// </summary>
        /// <returns>The trapping state, or null if the entry is not present.</returns>
        public PdfDocumentTrappedType? GetTrapped()
        {
            UInt32 result = NativeMethods.DocumentInfo_GetTrapped(Handle, out PdfDocumentTrappedType data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfDocumentTrappedType>.CheckedCast(data);
        }

        /// <summary>
        /// Set whether the document has been modified to include trapping information.
        /// </summary>
        /// <param name="value">The trapping state.</param>
        public void SetTrapped(PdfDocumentTrappedType value)
        {
            UInt32 result = NativeMethods.DocumentInfo_SetTrapped(Handle, (Int32)value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

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

        private void SetTitle(PdfStringObject value)
        {
            UInt32 result = NativeMethods.DocumentInfo_SetTitle(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private void SetAuthor(PdfStringObject value)
        {
            UInt32 result = NativeMethods.DocumentInfo_SetAuthor(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private void SetSubject(PdfStringObject value)
        {
            UInt32 result = NativeMethods.DocumentInfo_SetSubject(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private void SetKeywords(PdfStringObject value)
        {
            UInt32 result = NativeMethods.DocumentInfo_SetKeywords(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private void SetCreator(PdfStringObject value)
        {
            UInt32 result = NativeMethods.DocumentInfo_SetCreator(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private void SetProducer(PdfStringObject value)
        {
            UInt32 result = NativeMethods.DocumentInfo_SetProducer(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private void SetCreationDate(PdfDate value)
        {
            UInt32 result = NativeMethods.DocumentInfo_SetCreationDate(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private void SetModificationDate(PdfDate value)
        {
            UInt32 result = NativeMethods.DocumentInfo_SetModificationDate(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        #endregion

        /// <inheritdoc/>

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
