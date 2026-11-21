using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A form XObject is a content stream that is a self-contained description of any
    /// sequence of graphics objects. It may be painted multiple times and is also the
    /// carrier of annotation appearance streams.
    /// </summary>
    public class PdfFormXObject : PdfXObject
    {
        internal PdfFormXObjectSafeHandle Handle { get; }

        internal PdfFormXObject(PdfFormXObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get or set the bounding box (BBox entry) in form coordinate space.
        /// Returns null if the entry is not present.
        /// </summary>
        public PdfRectangle BoundingBox
        {
            get => GetBoundingBox();
            set => SetBoundingBox(value);
        }

        /// <summary>
        /// Get or set the transformation matrix (Matrix entry) mapping form space into user space.
        /// Returns null if the entry is not present.
        /// </summary>
        public PdfMatrix Matrix
        {
            get => GetMatrix();
            set => SetMatrix(value);
        }

        /// <summary>
        /// Get or set the resource dictionary (Resources entry) of the form.
        /// Returns null if the entry is not present.
        /// </summary>
        public PdfResourceDictionary Resources
        {
            get => GetResources();
            set => SetResources(value);
        }

        /// <summary>
        /// Get or set the group attributes dictionary (Group entry) that makes the form
        /// a transparency group XObject. Returns null if the entry is not present.
        /// </summary>
        public PdfTransparencyGroup Group
        {
            get => GetGroup();
            set => SetGroup(value);
        }

        /// <summary>
        /// Create an empty form XObject backed by a new content stream registered
        /// as an indirect object within the document.
        /// </summary>
        /// <param name="document">The document that owns the new form XObject.</param>
        /// <returns>A new <see cref="PdfFormXObject"/> instance.</returns>
        public static PdfFormXObject CreateFromDocument(PdfDocument document)
        {
            UInt32 result = NativeMethods.FormXObject_CreateFromDocument(document.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFormXObject(data);
        }

        /// <summary>
        /// Convert a base XObject to a form XObject.
        /// </summary>
        /// <param name="xobject">The XObject to convert. Must have type <see cref="PdfXObjectType.Form"/>.</param>
        /// <returns>A new <see cref="PdfFormXObject"/> instance.</returns>
        public static PdfFormXObject FromXObject(PdfXObject xobject)
        {
            UInt32 result = NativeMethods.FormXObject_FromBaseXObject(xobject.XObjectHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFormXObject(data);
        }

        private PdfRectangle GetBoundingBox()
        {
            UInt32 result = NativeMethods.FormXObject_GetBoundingBox(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfRectangle(data);
        }

        private void SetBoundingBox(PdfRectangle value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.FormXObject_SetBoundingBox(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfMatrix GetMatrix()
        {
            UInt32 result = NativeMethods.FormXObject_GetMatrix(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfMatrix(data);
        }

        private void SetMatrix(PdfMatrix value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.FormXObject_SetMatrix(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfResourceDictionary GetResources()
        {
            UInt32 result = NativeMethods.FormXObject_GetResources(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfResourceDictionary(data);
        }

        private void SetResources(PdfResourceDictionary value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.FormXObject_SetResources(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfTransparencyGroup GetGroup()
        {
            UInt32 result = NativeMethods.FormXObject_GetGroup(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfTransparencyGroup(data);
        }

        private void SetGroup(PdfTransparencyGroup value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.FormXObject_SetGroup(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            Handle?.Dispose();
        }
    }
}
