using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A link annotation represents a hypertext link to a destination
    /// elsewhere in the document or to an action to be performed.
    /// </summary>
    public class PdfLinkAnnotation : IDisposable
    {
        internal PdfLinkAnnotationSafeHandle Handle { get; }

        internal PdfLinkAnnotation(PdfLinkAnnotationSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a link annotation from a base annotation.
        /// </summary>
        /// <param name="annotation">The base annotation to convert.</param>
        /// <returns>A link annotation instance.</returns>
        public static PdfLinkAnnotation FromAnnotation(PdfAnnotation annotation)
        {
            UInt32 result = NativeMethods.LinkAnnotation_FromBaseAnnotation(annotation.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfLinkAnnotation(data);
        }

        /// <summary>
        /// Get the destination associated with this link annotation.
        /// </summary>
        public PdfDestination Destination
        {
            get
            {
                UInt32 result = NativeMethods.LinkAnnotation_GetDestination(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfDestination(data);
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
