using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Interactive forms use widget annotations (PDF 1.2) to represent
    /// the appearance of fields and to manage user interactions.
    /// </summary>
    public class PdfWidgetAnnotation : PdfAnnotation
    {
        internal PdfWidgetAnnotationSafeHandle Handle { get; }

        internal PdfWidgetAnnotation(PdfWidgetAnnotationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get or set the appearance characteristics dictionary (MK entry).
        /// Returns null if the entry is not present.
        /// </summary>
        public PdfAppearanceCharacteristics AppearanceCharacteristics
        {
            get => GetAppearanceCharacteristics();
            set => SetAppearanceCharacteristics(value);
        }

        /// <summary>
        /// Create a new widget annotation registered as an indirect object within the document.
        /// </summary>
        /// <remarks>
        /// The new widget carries an empty rectangle; set it with <see cref="PdfAnnotation.Rect"/>.
        /// Attach it to a field with <see cref="PdfField.AddWidget"/> and to a page through
        /// <see cref="PdfPageAnnotations"/>.
        /// </remarks>
        /// <param name="document">The document that owns the new annotation.</param>
        /// <returns>A new <see cref="PdfWidgetAnnotation"/> instance.</returns>
        public static PdfWidgetAnnotation CreateFromDocument(PdfDocument document)
        {
            UInt32 result = NativeMethods.WidgetAnnotation_CreateFromDocument(document.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfWidgetAnnotation(data);
        }

        /// <summary>
        /// Create a widget annotation from a base annotation.
        /// </summary>
        /// <param name="annotation">The base annotation to convert.</param>
        /// <returns>A new <see cref="PdfWidgetAnnotation"/> instance.</returns>
        public static PdfWidgetAnnotation FromAnnotation(PdfAnnotation annotation)
        {
            UInt32 result = NativeMethods.WidgetAnnotation_FromBaseAnnotation(annotation.AnnotationHandle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfWidgetAnnotation(data);
        }

        private PdfAppearanceCharacteristics GetAppearanceCharacteristics()
        {
            UInt32 result = NativeMethods.WidgetAnnotation_GetAppearanceCharacteristics(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfAppearanceCharacteristics(data);
        }

        private void SetAppearanceCharacteristics(PdfAppearanceCharacteristics value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            UInt32 result = NativeMethods.WidgetAnnotation_SetAppearanceCharacteristics(Handle, value.Handle);
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
