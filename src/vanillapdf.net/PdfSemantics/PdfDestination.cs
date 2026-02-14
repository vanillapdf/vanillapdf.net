using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Base class for all PDF destinations.
    /// A destination defines a particular view of a document.
    /// </summary>
    public class PdfDestination : IDisposable
    {
        internal PdfDestinationSafeHandle Handle { get; }

        internal PdfDestination(PdfDestinationSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a destination from an array object.
        /// Array format: [page /Type ...]
        /// </summary>
        /// <param name="array">Array object containing destination parameters.</param>
        /// <returns>A new destination instance.</returns>
        public static PdfDestination CreateFromArray(PdfArrayObject array)
        {
            UInt32 result = NativeMethods.Destination_CreateFromArray(array.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDestination(data);
        }

        /// <summary>
        /// Create a destination from a dictionary object.
        /// Dictionary format: {/D [page /Type ...]}
        /// </summary>
        /// <param name="dictionary">Dictionary object containing /D entry with destination parameters.</param>
        /// <returns>A new destination instance.</returns>
        public static PdfDestination CreateFromDictionary(PdfDictionaryObject dictionary)
        {
            UInt32 result = NativeMethods.Destination_CreateFromDictionary(dictionary.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDestination(data);
        }

        /// <summary>
        /// Get the type of this destination.
        /// </summary>
        public PdfDestinationType DestinationType
        {
            get
            {
                UInt32 result = NativeMethods.Destination_GetDestinationType(Handle, out Int32 data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return EnumUtil<PdfDestinationType>.CheckedCast(data);
            }
        }

        /// <summary>
        /// Get the page number or reference for this destination.
        /// The returned object is either an IntegerObject (page index) or
        /// an IndirectReferenceObject (reference to page object).
        /// </summary>
        public PdfObject PageNumber
        {
            get
            {
                UInt32 result = NativeMethods.Destination_GetPageNumber(Handle, out var data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }

        /// <summary>
        /// Convert this destination to an XYZ destination if the type matches.
        /// </summary>
        /// <returns>XYZ destination or null if type doesn't match.</returns>
        public PdfXYZDestination AsXYZ()
        {
            if (DestinationType != PdfDestinationType.XYZ) return null;
            return new PdfXYZDestination((PdfXYZDestinationSafeHandle)Handle);
        }

        /// <summary>
        /// Convert this destination to a Fit destination if the type matches.
        /// </summary>
        /// <returns>Fit destination or null if type doesn't match.</returns>
        public PdfFitDestination AsFit()
        {
            if (DestinationType != PdfDestinationType.Fit) return null;
            return new PdfFitDestination((PdfFitDestinationSafeHandle)Handle);
        }

        /// <summary>
        /// Convert this destination to a FitHorizontal destination if the type matches.
        /// </summary>
        /// <returns>FitHorizontal destination or null if type doesn't match.</returns>
        public PdfFitHorizontalDestination AsFitHorizontal()
        {
            if (DestinationType != PdfDestinationType.FitHorizontal) return null;
            return new PdfFitHorizontalDestination((PdfFitHorizontalDestinationSafeHandle)Handle);
        }

        /// <summary>
        /// Convert this destination to a FitVertical destination if the type matches.
        /// </summary>
        /// <returns>FitVertical destination or null if type doesn't match.</returns>
        public PdfFitVerticalDestination AsFitVertical()
        {
            if (DestinationType != PdfDestinationType.FitVertical) return null;
            return new PdfFitVerticalDestination((PdfFitVerticalDestinationSafeHandle)Handle);
        }

        /// <summary>
        /// Convert this destination to a FitRectangle destination if the type matches.
        /// </summary>
        /// <returns>FitRectangle destination or null if type doesn't match.</returns>
        public PdfFitRectangleDestination AsFitRectangle()
        {
            if (DestinationType != PdfDestinationType.FitRectangle) return null;
            return new PdfFitRectangleDestination((PdfFitRectangleDestinationSafeHandle)Handle);
        }

        /// <summary>
        /// Convert this destination to a FitBoundingBox destination if the type matches.
        /// </summary>
        /// <returns>FitBoundingBox destination or null if type doesn't match.</returns>
        public PdfFitBoundingBoxDestination AsFitBoundingBox()
        {
            if (DestinationType != PdfDestinationType.FitBoundingBox) return null;
            return new PdfFitBoundingBoxDestination((PdfFitBoundingBoxDestinationSafeHandle)Handle);
        }

        /// <summary>
        /// Convert this destination to a FitBoundingBoxHorizontal destination if the type matches.
        /// </summary>
        /// <returns>FitBoundingBoxHorizontal destination or null if type doesn't match.</returns>
        public PdfFitBoundingBoxHorizontalDestination AsFitBoundingBoxHorizontal()
        {
            if (DestinationType != PdfDestinationType.FitBoundingBoxHorizontal) return null;
            return new PdfFitBoundingBoxHorizontalDestination((PdfFitBoundingBoxHorizontalDestinationSafeHandle)Handle);
        }

        /// <summary>
        /// Convert this destination to a FitBoundingBoxVertical destination if the type matches.
        /// </summary>
        /// <returns>FitBoundingBoxVertical destination or null if type doesn't match.</returns>
        public PdfFitBoundingBoxVerticalDestination AsFitBoundingBoxVertical()
        {
            if (DestinationType != PdfDestinationType.FitBoundingBoxVertical) return null;
            return new PdfFitBoundingBoxVerticalDestination((PdfFitBoundingBoxVerticalDestinationSafeHandle)Handle);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
