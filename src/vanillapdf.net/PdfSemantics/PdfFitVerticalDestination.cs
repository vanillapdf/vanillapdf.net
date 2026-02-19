using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// FitV destination with horizontal coordinate at left edge.
    /// Display the page with horizontal coordinate left at left edge
    /// and contents magnified to fit entire height within window.
    /// </summary>
    public class PdfFitVerticalDestination : PdfDestination
    {
        internal new PdfFitVerticalDestinationSafeHandle Handle { get; }

        internal PdfFitVerticalDestination(PdfFitVerticalDestinationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Convert from base destination type.
        /// </summary>
        public static PdfFitVerticalDestination FromDestination(PdfDestination data)
        {
            return new PdfFitVerticalDestination(data.Handle);
        }

        /// <summary>
        /// Get the left coordinate (null means no change from current position).
        /// </summary>
        public PdfObject Left
        {
            get
            {
                UInt32 result = NativeMethods.FitVerticalDestination_GetLeft(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }
    }
}
