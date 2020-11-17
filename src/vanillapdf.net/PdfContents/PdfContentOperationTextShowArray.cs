using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    public class PdfContentOperationTextShowArray : PdfContentOperation
    {
        internal PdfContentOperationTextShowArray(PdfContentOperationTextShowArraySafeHandle handle) : base(handle)
        {
        }

        static PdfContentOperationTextShowArray()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

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
