using System;
using vanillapdf.net.Interop;
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
    public class PdfPageObject : IDisposable
    {
        internal PdfPageObjectSafeHandle Handle { get; }

        internal PdfPageObject(PdfPageObjectSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// The media box defining the visible region of the page.
        /// </summary>
        public PdfRectangle MediaBox
        {
            get { return GetMediaBox(); }
            set { SetMediaBox(value); }
        }

        /// <summary>
        /// Underlying page dictionary object.
        /// </summary>
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
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

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

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
