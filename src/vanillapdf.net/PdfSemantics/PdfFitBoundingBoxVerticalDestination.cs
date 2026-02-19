using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// FitBV destination with horizontal coordinate at left of bounding box.
    /// Display the page with horizontal coordinate left at left edge
    /// and contents magnified to fit bounding box height within window.
    /// </summary>
    public class PdfFitBoundingBoxVerticalDestination : PdfDestination
    {
        internal new PdfFitBoundingBoxVerticalDestinationSafeHandle Handle { get; }

        internal PdfFitBoundingBoxVerticalDestination(PdfFitBoundingBoxVerticalDestinationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Convert from base destination type.
        /// </summary>
        public static PdfFitBoundingBoxVerticalDestination FromDestination(PdfDestination data)
        {
            return new PdfFitBoundingBoxVerticalDestination(data.Handle);
        }

        /// <summary>
        /// Get the left coordinate (null means no change from current position).
        /// </summary>
        public PdfObject Left
        {
            get
            {
                UInt32 result = NativeMethods.FitBoundingBoxVerticalDestination_GetLeft(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }
    }
}
