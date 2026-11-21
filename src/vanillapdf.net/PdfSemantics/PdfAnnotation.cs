using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// An annotation associates an object such as a note, sound, or movie
    /// with a location on a page of a PDF document, or provides a way
    /// to interact with the user by means of the mouse and keyboard.
    /// </summary>
    public class PdfAnnotation : IDisposable
    {
        internal PdfAnnotationSafeHandle AnnotationHandle { get; }

        internal PdfAnnotation(PdfAnnotationSafeHandle handle)
        {
            AnnotationHandle = handle;
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfAnnotationType GetAnnotationType()
        {
            UInt32 result = NativeMethods.Annotation_GetAnnotationType(AnnotationHandle, out PdfAnnotationType data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfAnnotationType>.CheckedCast(data);
        }

        /// <summary>
        /// Get or set the annotation rectangle.
        /// </summary>
        public PdfRectangle Rect
        {
            get
            {
                UInt32 result = NativeMethods.Annotation_GetRect(AnnotationHandle, out var data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfRectangle(data);
            }
            set
            {
                UInt32 result = NativeMethods.Annotation_SetRect(AnnotationHandle, value.Handle);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
        }

        /// <summary>
        /// Get or set the annotation contents (text).
        /// </summary>
        public PdfLiteralStringObject Contents
        {
            get
            {
                UInt32 result = NativeMethods.Annotation_GetContents(AnnotationHandle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfLiteralStringObject(data);
            }
            set
            {
                UInt32 result = NativeMethods.Annotation_SetContents(AnnotationHandle, value.Handle);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
        }

        /// <summary>
        /// Get or set the annotation color.
        /// </summary>
        public PdfColor Color
        {
            get
            {
                UInt32 result = NativeMethods.Annotation_GetColor(AnnotationHandle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfColor(data);
            }
            set
            {
                UInt32 result = NativeMethods.Annotation_SetColor(AnnotationHandle, value.Handle);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
        }

        /// <summary>
        /// Get or set the annotation flags.
        /// </summary>
        public PdfAnnotationFlags Flags
        {
            get
            {
                UInt32 result = NativeMethods.Annotation_GetFlags(AnnotationHandle, out Int32 data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return EnumUtil<PdfAnnotationFlags>.FlagsCast(data);
            }
            set
            {
                UInt32 result = NativeMethods.Annotation_SetFlags(AnnotationHandle, (Int32)value);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
        }

        /// <summary>
        /// Get or set the appearance state (AS entry) - the key selecting the current
        /// stream out of an appearance slot's state subdictionary.
        /// Returns null if the entry is not present.
        /// </summary>
        public PdfNameObject AppearanceState
        {
            get => GetAppearanceState();
            set => SetAppearanceState(value);
        }

        /// <summary>
        /// Underlying annotation dictionary object.
        /// </summary>
        public PdfDictionaryObject BaseObject
        {
            get => GetBaseObject();
        }

        /// <summary>
        /// Get the appearance currently in effect for the specified slot of the appearance dictionary (AP entry).
        /// </summary>
        /// <remarks>
        /// When the slot holds a subdictionary of appearance states, the stream named
        /// by <see cref="AppearanceState"/> is returned.
        /// </remarks>
        /// <param name="type">The appearance slot to read.</param>
        /// <returns>
        /// The appearance stream, or null when the annotation has no appearance dictionary,
        /// the slot is absent, or none of its appearance states is selected.
        /// </returns>
        public PdfFormXObject GetAppearance(PdfAppearanceType type)
        {
            UInt32 result = NativeMethods.Annotation_GetAppearance(AnnotationHandle, (Int32)type, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFormXObject(data);
        }

        /// <summary>
        /// Set a single appearance stream for the specified slot of the appearance dictionary (AP entry).
        /// </summary>
        /// <remarks>
        /// The form XObject has to be registered within the document, see <see cref="PdfFormXObject.CreateFromDocument"/>.
        /// A slot that already holds appearance states cannot be replaced.
        /// </remarks>
        /// <param name="type">The appearance slot to write.</param>
        /// <param name="value">The form XObject defining the annotation's visual presentation.</param>
        public void SetAppearance(PdfAppearanceType type, PdfFormXObject value)
        {
            UInt32 result = NativeMethods.Annotation_SetAppearance(AnnotationHandle, (Int32)type, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfNameObject GetAppearanceState()
        {
            UInt32 result = NativeMethods.Annotation_GetAppearanceState(AnnotationHandle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(data);
        }

        private void SetAppearanceState(PdfNameObject value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.Annotation_SetAppearanceState(AnnotationHandle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfDictionaryObject GetBaseObject()
        {
            UInt32 result = NativeMethods.Annotation_GetBaseObject(AnnotationHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObject(data);
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            AnnotationHandle?.Dispose();
        }
    }
}
