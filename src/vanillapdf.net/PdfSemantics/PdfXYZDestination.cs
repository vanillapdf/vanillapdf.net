using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// XYZ destination with optional left, top, and zoom parameters.
    /// Display the page with coordinates (left, top) at upper-left corner
    /// and contents magnified by zoom factor.
    /// </summary>
    public class PdfXYZDestination : PdfDestination
    {
        internal new PdfXYZDestinationSafeHandle Handle { get; }

        internal PdfXYZDestination(PdfXYZDestinationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get the left coordinate (null means no change from current position).
        /// </summary>
        public PdfObject Left
        {
            get
            {
                UInt32 result = NativeMethods.XYZDestination_GetLeft(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }

        /// <summary>
        /// Get the top coordinate (null means no change from current position).
        /// </summary>
        public PdfObject Top
        {
            get
            {
                UInt32 result = NativeMethods.XYZDestination_GetTop(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }

        /// <summary>
        /// Get the zoom factor (null means no change from current zoom).
        /// </summary>
        public PdfObject Zoom
        {
            get
            {
                UInt32 result = NativeMethods.XYZDestination_GetZoom(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }
    }
}
