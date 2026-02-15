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
        internal PdfAnnotationSafeHandle Handle { get; }

        internal PdfAnnotation(PdfAnnotationSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfAnnotationType GetAnnotationType()
        {
            UInt32 result = NativeMethods.Annotation_GetAnnotationType(Handle, out Int32 data);
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
                UInt32 result = NativeMethods.Annotation_GetRect(Handle, out var data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfRectangle(data);
            }
            set
            {
                UInt32 result = NativeMethods.Annotation_SetRect(Handle, value.Handle);
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
                UInt32 result = NativeMethods.Annotation_GetContents(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfLiteralStringObject(data);
            }
            set
            {
                UInt32 result = NativeMethods.Annotation_SetContents(Handle, value.Handle);
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
                UInt32 result = NativeMethods.Annotation_GetColor(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfColor(data);
            }
            set
            {
                UInt32 result = NativeMethods.Annotation_SetColor(Handle, value.Handle);
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
                UInt32 result = NativeMethods.Annotation_GetFlags(Handle, out Int32 data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return (PdfAnnotationFlags)data;
            }
            set
            {
                UInt32 result = NativeMethods.Annotation_SetFlags(Handle, (Int32)value);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
