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
        /// Resolve any PDF object (array, name, or string) into a destination.
        /// This handles named destinations, explicit destination arrays, and
        /// dictionary-based destinations.
        /// </summary>
        /// <param name="obj">A PDF object representing a destination.</param>
        /// <returns>A new resolved destination instance.</returns>
        public static PdfDestination Resolve(PdfObject obj)
        {
            UInt32 result = NativeMethods.Destination_Resolve(obj.ObjectHandle, out var data);
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

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
