using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A free text annotation displays text directly on the page.
    /// Unlike an ordinary text annotation, a free text annotation has no
    /// open or closed state; instead, the text shall always be visible.
    /// </summary>
    public class PdfFreeTextAnnotation : PdfAnnotation
    {
        internal new PdfFreeTextAnnotationSafeHandle Handle { get; }

        internal PdfFreeTextAnnotation(PdfFreeTextAnnotationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new free text annotation.
        /// </summary>
        /// <param name="rect">The bounding rectangle for the annotation.</param>
        /// <param name="contents">The text contents of the annotation.</param>
        /// <param name="defaultAppearance">The default appearance string (font and size).</param>
        public static PdfFreeTextAnnotation Create(PdfRectangle rect, PdfLiteralStringObject contents, PdfLiteralStringObject defaultAppearance)
        {
            UInt32 result = NativeMethods.FreeTextAnnotation_Create(rect.Handle, contents.Handle, defaultAppearance.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfFreeTextAnnotation(data);
        }

        /// <summary>
        /// Create a free text annotation from a base annotation.
        /// </summary>
        /// <param name="annotation">The base annotation to convert.</param>
        public static PdfFreeTextAnnotation FromAnnotation(PdfAnnotation annotation)
        {
            UInt32 result = NativeMethods.FreeTextAnnotation_FromBaseAnnotation(annotation.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfFreeTextAnnotation(data);
        }

        /// <summary>
        /// Get or set the default appearance string.
        /// </summary>
        public PdfLiteralStringObject DefaultAppearance
        {
            get
            {
                UInt32 result = NativeMethods.FreeTextAnnotation_GetDefaultAppearance(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfLiteralStringObject(data);
            }
            set
            {
                UInt32 result = NativeMethods.FreeTextAnnotation_SetDefaultAppearance(Handle, value.Handle);
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
                UInt32 result = NativeMethods.FreeTextAnnotation_GetAuthor(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfStringObject(data);
            }
            set
            {
                UInt32 result = NativeMethods.FreeTextAnnotation_SetAuthor(Handle, value.StringHandle);
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
                UInt32 result = NativeMethods.FreeTextAnnotation_GetModificationDate(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfDate(data);
            }
            set
            {
                UInt32 result = NativeMethods.FreeTextAnnotation_SetModificationDate(Handle, value.Handle);
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
                UInt32 result = NativeMethods.FreeTextAnnotation_GetCreationDate(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return new PdfDate(data);
            }
            set
            {
                UInt32 result = NativeMethods.FreeTextAnnotation_SetCreationDate(Handle, value.Handle);
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
