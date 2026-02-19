using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// FitR destination that fits specified rectangle within window.
    /// Display the page with contents magnified to fit the rectangle
    /// specified by (left, bottom, right, top) within window.
    /// </summary>
    public class PdfFitRectangleDestination : PdfDestination
    {
        internal new PdfFitRectangleDestinationSafeHandle Handle { get; }

        internal PdfFitRectangleDestination(PdfFitRectangleDestinationSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Convert from base destination type.
        /// </summary>
        public static PdfFitRectangleDestination FromDestination(PdfDestination data)
        {
            return new PdfFitRectangleDestination(data.Handle);
        }

        /// <summary>
        /// Get the left coordinate of the rectangle.
        /// </summary>
        public PdfObject Left
        {
            get
            {
                UInt32 result = NativeMethods.FitRectangleDestination_GetLeft(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }

        /// <summary>
        /// Get the bottom coordinate of the rectangle.
        /// </summary>
        public PdfObject Bottom
        {
            get
            {
                UInt32 result = NativeMethods.FitRectangleDestination_GetBottom(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }

        /// <summary>
        /// Get the right coordinate of the rectangle.
        /// </summary>
        public PdfObject Right
        {
            get
            {
                UInt32 result = NativeMethods.FitRectangleDestination_GetRight(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }

        /// <summary>
        /// Get the top coordinate of the rectangle.
        /// </summary>
        public PdfObject Top
        {
            get
            {
                UInt32 result = NativeMethods.FitRectangleDestination_GetTop(Handle, out var data);
                if (result == PdfReturnValues.ERROR_OBJECT_MISSING) return null;
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }
    }
}
