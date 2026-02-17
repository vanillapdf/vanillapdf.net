using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A URI action causes a URI to be resolved.
    /// </summary>
    public class PdfURIAction : PdfAction
    {
        internal new PdfURIActionSafeHandle Handle { get; }

        internal PdfURIAction(PdfURIActionSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// The uniform resource identifier to resolve.
        /// </summary>
        public PdfLiteralStringObject URI
        {
            get
            {
                UInt32 result = NativeMethods.URIAction_GetURI(Handle, out var data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfLiteralStringObject(data);
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
