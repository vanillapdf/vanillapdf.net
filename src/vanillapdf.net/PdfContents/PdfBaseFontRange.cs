using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Base font ranges are used for mapping input codes to corresponding range of character codes or names
    /// </summary>
    public class PdfBaseFontRange : PdfUnknown
    {
        internal PdfBaseFontRangeSafeHandle Handle { get; }

        internal PdfBaseFontRange(PdfBaseFontRangeSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfBaseFontRange()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfBaseFontRangeSafeHandle).TypeHandle);
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

        protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static CreateDelgate BaseFontRange_Create = LibraryInstance.GetFunction<CreateDelgate>("BaseFontRange_Create");
            public static GetRangeLowDelgate BaseFontRange_GetRangeLow = LibraryInstance.GetFunction<GetRangeLowDelgate>("BaseFontRange_GetRangeLow");
            public static SetRangeLowDelgate BaseFontRange_SetRangeLow = LibraryInstance.GetFunction<SetRangeLowDelgate>("BaseFontRange_SetRangeLow");
            public static GetRangeHighDelgate BaseFontRange_GetRangeHigh = LibraryInstance.GetFunction<GetRangeHighDelgate>("BaseFontRange_GetRangeHigh");
            public static SetRangeHighDelgate BaseFontRange_SetRangeHigh = LibraryInstance.GetFunction<SetRangeHighDelgate>("BaseFontRange_SetRangeHigh");
            public static GetDestinationDelgate BaseFontRange_GetDestination = LibraryInstance.GetFunction<GetDestinationDelgate>("BaseFontRange_GetDestination");
            public static SetDestinationDelgate BaseFontRange_SetDestination = LibraryInstance.GetFunction<SetDestinationDelgate>("BaseFontRange_SetDestination");

            public static ContainsDelgate BaseFontRange_Contains = LibraryInstance.GetFunction<ContainsDelgate>("BaseFontRange_Contains");
            public static GetMappedValueDelgate BaseFontRange_GetMappedValue = LibraryInstance.GetFunction<GetMappedValueDelgate>("BaseFontRange_GetMappedValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfBaseFontRangeSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetRangeLowDelgate(PdfBaseFontRangeSafeHandle handle, out PdfHexadecimalStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetRangeLowDelgate(PdfBaseFontRangeSafeHandle handle, PdfHexadecimalStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetRangeHighDelgate(PdfBaseFontRangeSafeHandle handle, out PdfHexadecimalStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetRangeHighDelgate(PdfBaseFontRangeSafeHandle handle, PdfHexadecimalStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetDestinationDelgate(PdfBaseFontRangeSafeHandle handle, out PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetDestinationDelgate(PdfBaseFontRangeSafeHandle handle, PdfObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ContainsDelgate(PdfBaseFontRangeSafeHandle handle, PdfBufferSafeHandle data, out bool result);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetMappedValueDelgate(PdfBaseFontRangeSafeHandle handle, PdfBufferSafeHandle data, out PdfBufferSafeHandle result);
        }
    }
}
