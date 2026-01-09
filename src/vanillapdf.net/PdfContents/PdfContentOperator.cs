using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// An operator is a PDF keyword specifying some action that shall be performed, such as painting a graphical shape on the page.
    /// </summary>
    public class PdfContentOperator : PdfUnknown
    {
        internal PdfContentOperatorSafeHandle Handle { get; }

        private PdfContentOperatorType? _cachedOperatorType;

        internal PdfContentOperator(PdfContentOperatorSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfContentOperator()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperatorSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfContentOperatorType GetOperatorType()
        {
            if (_cachedOperatorType.HasValue) {
                return _cachedOperatorType.Value;
            }

            UInt32 result = NativeMethods.ContentOperator_GetOperatorType(Handle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            _cachedOperatorType = EnumUtil<PdfContentOperatorType>.CheckedCast(data);
            return _cachedOperatorType.Value;
        }

        /// <summary>
        /// Get data buffer containing text representation of the content operator
        /// </summary>
        /// <returns>Handle to a data buffer on success, throws exception on failure</returns>
        public PdfBuffer GetValue()
        {
            UInt32 result = NativeMethods.ContentOperator_GetValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(value);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var operatorType = GetOperatorType();
            return operatorType.ToString();
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static GetOperatorTypeDelgate ContentOperator_GetOperatorType = LibraryInstance.GetFunction<GetOperatorTypeDelgate>("ContentOperator_GetOperatorType");
            public static GetValueDelgate ContentOperator_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("ContentOperator_GetValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOperatorTypeDelgate(PdfContentOperatorSafeHandle handle, out Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfContentOperatorSafeHandle handle, out PdfBufferSafeHandle value);
        }
    }
}
