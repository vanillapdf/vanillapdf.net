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
            UInt32 result = NativeMethods.Annotation_GetAnnotationType(AnnotationHandle, out Int32 data);
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

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            AnnotationHandle?.Dispose();
        }
    }
}
