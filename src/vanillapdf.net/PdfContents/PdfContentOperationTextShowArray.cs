using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Show one or more text strings, allowing individual glyph positioning.
    /// </summary>
    public class PdfContentOperationTextShowArray : PdfContentOperation
    {
        internal PdfContentOperationTextShowArray(PdfContentOperationTextShowArraySafeHandle handle) : base(handle)
        {
        }

        static PdfContentOperationTextShowArray()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperationTextShowArraySafeHandle).TypeHandle);
        }

        /// <summary>
        /// Set new text strings to be shown.
        /// 
        /// Each element of array shall be either a string or a number.
        /// If the element is a string, this operator shall show the string.
        /// If it is a number, the operator shall adjust the text position by that amount.
        /// </summary>
        public PdfArrayObject Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        private PdfArrayObject GetValue()
        {
            UInt32 result = NativeMethods.ContentOperationTextShow_GetValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfArrayObject(value);
        }

        private void SetValue(PdfArrayObject value)
        {
            UInt32 result = NativeMethods.ContentOperationTextShow_SetValue(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Convert content operation to text showing operation using array of parameters
        /// </summary>
        /// <param name="data">Handle to \ref PdfContentOperation to be converted</param>
        /// <returns>A new instance of \ref PdfContentOperationTextShowArray if the object can be converted, throws exception on failure</returns>
        public static PdfContentOperationTextShowArray FromContentOperation(PdfContentOperation data)
        {
            return new PdfContentOperationTextShowArray(data.Handle);
        }

        private static class NativeMethods
        {
            public static GetValueDelgate ContentOperationTextShow_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("ContentOperationTextShow_GetValue");
            public static SetValueDelgate ContentOperationTextShow_SetValue = LibraryInstance.GetFunction<SetValueDelgate>("ContentOperationTextShow_SetValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfContentInstructionSafeHandle handle, out PdfArrayObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetValueDelgate(PdfContentInstructionSafeHandle handle, PdfArrayObjectSafeHandle data);
        }
    }
}
