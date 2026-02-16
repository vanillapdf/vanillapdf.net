using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// An ink annotation represents a freehand "scribble" composed of
    /// one or more disjoint paths. When opened, it shall display a pop-up window
    /// containing the text of the associated note.
    /// </summary>
    public class PdfInkAnnotation : PdfAnnotation
    {
        internal new PdfInkAnnotationSafeHandle Handle { get; }

        internal PdfInkAnnotation(PdfInkAnnotationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new empty ink annotation.
        /// </summary>
        public static PdfInkAnnotation Create()
        {
            UInt32 result = NativeMethods.InkAnnotation_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfInkAnnotation(data);
        }

        /// <summary>
        /// Create a new ink annotation with the specified rectangle.
        /// </summary>
        /// <param name="rect">The bounding rectangle for the annotation.</param>
        public static PdfInkAnnotation CreateFromRect(PdfRectangle rect)
        {
            UInt32 result = NativeMethods.InkAnnotation_CreateFromRect(rect.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfInkAnnotation(data);
        }

        /// <summary>
        /// Create an ink annotation from a base annotation.
        /// </summary>
        /// <param name="annotation">The base annotation to convert.</param>
        public static PdfInkAnnotation FromAnnotation(PdfAnnotation annotation)
        {
            UInt32 result = NativeMethods.InkAnnotation_FromBaseAnnotation(annotation.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfInkAnnotation(data);
        }

        /// <summary>
        /// Get or set the ink list (array of stroked paths).
        /// </summary>
        public PdfArrayObject InkList
        {
            get
            {
                UInt32 result = NativeMethods.InkAnnotation_GetInkList(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfArrayObject(data);
            }
            set
            {
                UInt32 result = NativeMethods.InkAnnotation_SetInkList(Handle, value.Handle);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
        }

        /// <summary>
        /// Get or set the annotation author.
        /// </summary>
        public PdfStringObject Author
        {
            get
            {
                UInt32 result = NativeMethods.InkAnnotation_GetAuthor(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfStringObject(data);
            }
            set
            {
                UInt32 result = NativeMethods.InkAnnotation_SetAuthor(Handle, value.StringHandle);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
        }

        /// <summary>
        /// Get or set the annotation modification date.
        /// </summary>
        public PdfDate ModificationDate
        {
            get
            {
                UInt32 result = NativeMethods.InkAnnotation_GetModificationDate(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfDate(data);
            }
            set
            {
                UInt32 result = NativeMethods.InkAnnotation_SetModificationDate(Handle, value.Handle);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
        }

        /// <summary>
        /// Get or set the annotation creation date.
        /// </summary>
        public PdfDate CreationDate
        {
            get
            {
                UInt32 result = NativeMethods.InkAnnotation_GetCreationDate(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfDate(data);
            }
            set
            {
                UInt32 result = NativeMethods.InkAnnotation_SetCreationDate(Handle, value.Handle);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
