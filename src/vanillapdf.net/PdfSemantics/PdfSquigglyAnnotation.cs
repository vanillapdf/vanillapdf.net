using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A squiggly annotation appears as a jagged underline beneath text in the document.
    /// When opened, it shall display a pop-up window containing the text of the associated note.
    /// </summary>
    public class PdfSquigglyAnnotation : PdfAnnotation
    {
        internal new PdfSquigglyAnnotationSafeHandle Handle { get; }

        internal PdfSquigglyAnnotation(PdfSquigglyAnnotationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new empty squiggly annotation.
        /// </summary>
        public static PdfSquigglyAnnotation Create()
        {
            UInt32 result = NativeMethods.SquigglyAnnotation_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfSquigglyAnnotation(data);
        }

        /// <summary>
        /// Create a new squiggly annotation with the specified rectangle.
        /// </summary>
        /// <param name="rect">The bounding rectangle for the annotation.</param>
        public static PdfSquigglyAnnotation CreateFromRect(PdfRectangle rect)
        {
            UInt32 result = NativeMethods.SquigglyAnnotation_CreateFromRect(rect.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfSquigglyAnnotation(data);
        }

        /// <summary>
        /// Create a squiggly annotation from a base annotation.
        /// </summary>
        /// <param name="annotation">The base annotation to convert.</param>
        public static PdfSquigglyAnnotation FromAnnotation(PdfAnnotation annotation)
        {
            UInt32 result = NativeMethods.SquigglyAnnotation_FromBaseAnnotation(annotation.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfSquigglyAnnotation(data);
        }

        /// <summary>
        /// Get or set the quad points array for text markup.
        /// </summary>
        public PdfArrayObject QuadPoints
        {
            get
            {
                UInt32 result = NativeMethods.SquigglyAnnotation_GetQuadPoints(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfArrayObject(data);
            }
            set
            {
                UInt32 result = NativeMethods.SquigglyAnnotation_SetQuadPoints(Handle, value.Handle);
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
                UInt32 result = NativeMethods.SquigglyAnnotation_GetAuthor(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfStringObject(data);
            }
            set
            {
                UInt32 result = NativeMethods.SquigglyAnnotation_SetAuthor(Handle, value.StringHandle);
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
                UInt32 result = NativeMethods.SquigglyAnnotation_GetModificationDate(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfDate(data);
            }
            set
            {
                UInt32 result = NativeMethods.SquigglyAnnotation_SetModificationDate(Handle, value.Handle);
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
                UInt32 result = NativeMethods.SquigglyAnnotation_GetCreationDate(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfDate(data);
            }
            set
            {
                UInt32 result = NativeMethods.SquigglyAnnotation_SetCreationDate(Handle, value.Handle);
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
