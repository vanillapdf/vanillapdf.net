using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// FitBH destination with vertical coordinate at top of bounding box.
    /// Display the page with vertical coordinate top at top edge
    /// and contents magnified to fit bounding box width within window.
    /// </summary>
    public class PdfFitBoundingBoxHorizontalDestination : PdfDestination
    {
        internal new PdfFitBoundingBoxHorizontalDestinationSafeHandle Handle { get; }

        internal PdfFitBoundingBoxHorizontalDestination(PdfFitBoundingBoxHorizontalDestinationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get the top coordinate (null means no change from current position).
        /// </summary>
        public PdfObject Top
        {
            get
            {
                UInt32 result = NativeMethods.FitBoundingBoxHorizontalDestination_GetTop(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }
    }
}
