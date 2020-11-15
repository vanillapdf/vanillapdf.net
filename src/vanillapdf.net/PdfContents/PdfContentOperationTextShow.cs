using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public class PdfContentOperationTextShow : PdfContentOperation
    {
        internal PdfContentOperationTextShow(PdfContentOperationTextShowSafeHandle handle) : base(handle)
        {
        }

        static PdfContentOperationTextShow()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfStringObject Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        public PdfStringObject GetValue()
        {
            UInt32 result = NativeMethods.ContentOperationTextShow_GetValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(value);
        }

        public void SetValue(PdfStringObject value)
        {
            UInt32 result = NativeMethods.ContentOperationTextShow_SetValue(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public static PdfContentOperationTextShow FromContentOperation(PdfContentOperation data)
        {
            return new PdfContentOperationTextShow(data.Handle);
        }

        private static class NativeMethods
        {
            public static GetValueDelgate ContentOperationTextShow_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("ContentOperationTextShow_GetValue");
            public static SetValueDelgate ContentOperationTextShow_SetValue = LibraryInstance.GetFunction<SetValueDelgate>("ContentOperationTextShow_SetValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
			public delegate UInt32 GetValueDelgate(PdfContentOperationTextShowSafeHandle handle, out PdfStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetValueDelgate(PdfContentOperationTextShowSafeHandle handle, PdfStringObjectSafeHandle data);
        }
    }
}
