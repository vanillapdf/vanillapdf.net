using System;
using vanillapdf.net.Interop;
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

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
