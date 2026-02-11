using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Base font ranges are used for mapping input codes to corresponding range of character codes or names
    /// </summary>
    public class PdfBaseFontRange : IDisposable
    {
        internal PdfBaseFontRangeSafeHandle Handle { get; }

        internal PdfBaseFontRange(PdfBaseFontRangeSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// The lower boundary of the font range
        /// </summary>
        public PdfHexadecimalStringObject RangeLow
        {
            get { return GetRangeLow(); }
            set { SetRangeLow(value); }
        }

        /// <summary>
        /// The upper boundary of the font range
        /// </summary>
        public PdfHexadecimalStringObject RangeHigh
        {
            get { return GetRangeHigh(); }
            set { SetRangeHigh(value); }
        }

        /// <summary>
        /// The mapped value that is associated with the range boundaries
        /// </summary>
        public PdfObject Destination
        {
            get { return GetDestination(); }
            set { SetDestination(value); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfBaseFontRange with default value
        /// </summary>
        /// <returns>New instance of \ref PdfBaseFontRange on success, throws exception on failure</returns>
        public static PdfBaseFontRange Create()
        {
            UInt32 result = NativeMethods.BaseFontRange_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBaseFontRange(data);
        }

        private PdfHexadecimalStringObject GetRangeLow()
        {
            UInt32 result = NativeMethods.BaseFontRange_GetRangeLow(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(value);
        }

        private void SetRangeLow(PdfHexadecimalStringObject value)
        {
            UInt32 result = NativeMethods.BaseFontRange_SetRangeLow(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfHexadecimalStringObject GetRangeHigh()
        {
            UInt32 result = NativeMethods.BaseFontRange_GetRangeHigh(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(value);
        }

        private void SetRangeHigh(PdfHexadecimalStringObject value)
        {
            UInt32 result = NativeMethods.BaseFontRange_SetRangeHigh(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfObject GetDestination()
        {
            UInt32 result = NativeMethods.BaseFontRange_GetDestination(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(value);
        }

        private void SetDestination(PdfObject value)
        {
            UInt32 result = NativeMethods.BaseFontRange_SetDestination(Handle, value.ObjectHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Check if the corresponding value for parameter <paramref name="value"/> is present in the mapping table
        /// </summary>
        /// <param name="value">The key to be checked in the mapping table</param>
        /// <returns>Boolean value on success, throws exception on failure</returns>
        public bool Contains(PdfBuffer value)
        {
            UInt32 result = NativeMethods.BaseFontRange_Contains(Handle, value.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Get the corresponding value for parameter <paramref name="value"/> in the mapping table
        /// </summary>
        /// <param name="value">The key to be extracted from the mapping table</param>
        /// <returns>New instance of \ref PdfBuffer on success, throws exception on failure</returns>
        public PdfBuffer GetMappedValue(PdfBuffer value)
        {
            UInt32 result = NativeMethods.BaseFontRange_GetMappedValue(Handle, value.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
