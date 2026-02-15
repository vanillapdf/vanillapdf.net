using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A strikeout annotation appears as a strikethrough line across text in the document.
    /// When opened, it shall display a pop-up window containing the text of the associated note.
    /// </summary>
    public class PdfStrikeOutAnnotation : PdfAnnotation
    {
        internal new PdfStrikeOutAnnotationSafeHandle Handle { get; }

        internal PdfStrikeOutAnnotation(PdfStrikeOutAnnotationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new empty strikeout annotation.
        /// </summary>
        public static PdfStrikeOutAnnotation Create()
        {
            UInt32 result = NativeMethods.StrikeOutAnnotation_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfStrikeOutAnnotation(data);
        }

        /// <summary>
        /// Create a new strikeout annotation with the specified rectangle.
        /// </summary>
        /// <param name="rect">The bounding rectangle for the annotation.</param>
        public static PdfStrikeOutAnnotation CreateFromRect(PdfRectangle rect)
        {
            UInt32 result = NativeMethods.StrikeOutAnnotation_CreateFromRect(rect.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfStrikeOutAnnotation(data);
        }

        /// <summary>
        /// Create a strikeout annotation from a base annotation.
        /// </summary>
        /// <param name="annotation">The base annotation to convert.</param>
        public static PdfStrikeOutAnnotation FromAnnotation(PdfAnnotation annotation)
        {
            UInt32 result = NativeMethods.StrikeOutAnnotation_FromBaseAnnotation(annotation.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfStrikeOutAnnotation(data);
        }

        /// <summary>
        /// Get or set the quad points array for text markup.
        /// </summary>
        public PdfArrayObject QuadPoints
        {
            get
            {
                UInt32 result = NativeMethods.StrikeOutAnnotation_GetQuadPoints(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfArrayObject(data);
            }
            set
            {
                UInt32 result = NativeMethods.StrikeOutAnnotation_SetQuadPoints(Handle, value.Handle);
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
                UInt32 result = NativeMethods.StrikeOutAnnotation_GetAuthor(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfStringObject(data);
            }
            set
            {
                UInt32 result = NativeMethods.StrikeOutAnnotation_SetAuthor(Handle, value.StringHandle);
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
                UInt32 result = NativeMethods.StrikeOutAnnotation_GetModificationDate(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfDate(data);
            }
            set
            {
                UInt32 result = NativeMethods.StrikeOutAnnotation_SetModificationDate(Handle, value.Handle);
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
                UInt32 result = NativeMethods.StrikeOutAnnotation_GetCreationDate(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfDate(data);
            }
            set
            {
                UInt32 result = NativeMethods.StrikeOutAnnotation_SetCreationDate(Handle, value.Handle);
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
