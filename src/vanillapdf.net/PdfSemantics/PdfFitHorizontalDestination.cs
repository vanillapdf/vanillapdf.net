using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// FitH destination with vertical coordinate at top edge.
    /// Display the page with vertical coordinate top at top edge
    /// and contents magnified to fit entire width within window.
    /// </summary>
    public class PdfFitHorizontalDestination : PdfDestination
    {
        internal new PdfFitHorizontalDestinationSafeHandle Handle { get; }

        internal PdfFitHorizontalDestination(PdfFitHorizontalDestinationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Convert from base destination type.
        /// </summary>
        public static PdfFitHorizontalDestination FromDestination(PdfDestination data)
        {
            return new PdfFitHorizontalDestination(data.Handle);
        }

        /// <summary>
        /// Get the top coordinate (null means no change from current position).
        /// </summary>
        public PdfObject Top
        {
            get
            {
                UInt32 result = NativeMethods.FitHorizontalDestination_GetTop(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }
    }
}
