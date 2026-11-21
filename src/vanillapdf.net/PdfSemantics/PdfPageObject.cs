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
        /// The crop box defining the visible region of default user space; the page contents are clipped to it.
        /// Only the page's own entry is read, an inherited value is not resolved. Returns null if absent.
        /// </summary>
        public PdfRectangle CropBox
        {
            get { return GetCropBox(); }
            set { SetCropBox(value); }
        }

        /// <summary>
        /// The bleed box defining the region to which the page contents are clipped in a production environment.
        /// Returns null if absent, in which case the crop box applies.
        /// </summary>
        public PdfRectangle BleedBox
        {
            get { return GetBleedBox(); }
            set { SetBleedBox(value); }
        }

        /// <summary>
        /// The trim box defining the intended dimensions of the finished page after trimming.
        /// Returns null if absent, in which case the crop box applies.
        /// </summary>
        public PdfRectangle TrimBox
        {
            get { return GetTrimBox(); }
            set { SetTrimBox(value); }
        }

        /// <summary>
        /// The art box defining the extent of the page's meaningful content.
        /// Returns null if absent, in which case the crop box applies.
        /// </summary>
        public PdfRectangle ArtBox
        {
            get { return GetArtBox(); }
            set { SetArtBox(value); }
        }

        /// <summary>
        /// The group attributes dictionary specifying the attributes of the page's page group
        /// for use in the transparent imaging model. Returns null if absent.
        /// </summary>
        public PdfTransparencyGroup Group
        {
            get { return GetGroup(); }
            set { SetGroup(value); }
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

        /// <summary>
        /// Set the content stream describing the contents of this page.
        /// </summary>
        /// <remarks>
        /// The page stores an indirect reference, see <see cref="PdfPageContents.CreateFromDocument"/>.
        /// </remarks>
        /// <param name="contents">The page contents to set.</param>
        public void SetContents(PdfPageContents contents)
        {
            if (contents == null) {
                throw new ArgumentNullException(nameof(contents));
            }

            UInt32 result = NativeMethods.PageObject_SetContents(Handle, contents.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Set the array of annotations associated with the page (see 12.5, "Annotations").
        /// </summary>
        /// <param name="annotations">The page annotations to set.</param>
        public void SetAnnotations(PdfPageAnnotations annotations)
        {
            if (annotations == null) {
                throw new ArgumentNullException(nameof(annotations));
            }

            UInt32 result = NativeMethods.PageObject_SetAnnotations(Handle, annotations.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Set the dictionary containing the resources required by the page.
        /// </summary>
        /// <param name="resources">The resource dictionary to set.</param>
        public void SetResources(PdfResourceDictionary resources)
        {
            if (resources == null) {
                throw new ArgumentNullException(nameof(resources));
            }

            UInt32 result = NativeMethods.PageObject_SetResources(Handle, resources.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
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

        private PdfRectangle GetCropBox()
        {
            UInt32 result = NativeMethods.PageObject_GetCropBox(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfRectangle(data);
        }

        private void SetCropBox(PdfRectangle data)
        {
            if (data == null) {
                throw new ArgumentNullException(nameof(data));
            }

            UInt32 result = NativeMethods.PageObject_SetCropBox(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfRectangle GetBleedBox()
        {
            UInt32 result = NativeMethods.PageObject_GetBleedBox(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfRectangle(data);
        }

        private void SetBleedBox(PdfRectangle data)
        {
            if (data == null) {
                throw new ArgumentNullException(nameof(data));
            }

            UInt32 result = NativeMethods.PageObject_SetBleedBox(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfRectangle GetTrimBox()
        {
            UInt32 result = NativeMethods.PageObject_GetTrimBox(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfRectangle(data);
        }

        private void SetTrimBox(PdfRectangle data)
        {
            if (data == null) {
                throw new ArgumentNullException(nameof(data));
            }

            UInt32 result = NativeMethods.PageObject_SetTrimBox(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfRectangle GetArtBox()
        {
            UInt32 result = NativeMethods.PageObject_GetArtBox(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfRectangle(data);
        }

        private void SetArtBox(PdfRectangle data)
        {
            if (data == null) {
                throw new ArgumentNullException(nameof(data));
            }

            UInt32 result = NativeMethods.PageObject_SetArtBox(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Get the number of degrees by which the page shall be rotated clockwise when displayed or printed.
        /// The stored value is returned unmodified, interpreting it is left to the caller.
        /// Only the page's own entry is read, an inherited value is not resolved.
        /// </summary>
        /// <returns>The value, or null if the entry is absent, in which case the page is not rotated.</returns>
        public Int64? GetRotate()
        {
            UInt32 result = NativeMethods.PageObject_GetRotate(Handle, out Int64 data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Set the number of degrees by which the page shall be rotated clockwise when displayed or printed.
        /// The entry is written on the page itself; a value that is not a multiple of 90 is rejected.
        /// </summary>
        /// <param name="data">The value to set.</param>
        public void SetRotate(Int64 data)
        {
            UInt32 result = NativeMethods.PageObject_SetRotate(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Get the size of default user space units, in multiples of 1/72 inch.
        /// </summary>
        /// <returns>The value, or null if the entry is absent, in which case one unit is 1/72 inch.</returns>
        public double? GetUserUnit()
        {
            UInt32 result = NativeMethods.PageObject_GetUserUnit(Handle, out double data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Set the size of default user space units, in multiples of 1/72 inch. The value shall be positive.
        /// </summary>
        /// <param name="data">The value to set.</param>
        public void SetUserUnit(double data)
        {
            UInt32 result = NativeMethods.PageObject_SetUserUnit(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfTransparencyGroup GetGroup()
        {
            UInt32 result = NativeMethods.PageObject_GetGroup(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfTransparencyGroup(data);
        }

        private void SetGroup(PdfTransparencyGroup data)
        {
            if (data == null) {
                throw new ArgumentNullException(nameof(data));
            }

            UInt32 result = NativeMethods.PageObject_SetGroup(Handle, data.Handle);
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

        /// <inheritdoc/>

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
