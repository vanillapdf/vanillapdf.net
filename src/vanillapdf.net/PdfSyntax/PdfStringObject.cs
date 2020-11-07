using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;
using vanillapdf.net.Utils.SafeHandles;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfStringObject : PdfObject
    {
        internal PdfStringObject(PdfStringObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfStringObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfBuffer Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        public PdfStringType GetStringType()
        {
            UInt32 result = NativeMethods.StringObject_GetType(Handle, out PdfStringType data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfStringType>.CheckedCast(data);
        }

        public PdfBuffer GetValue()
        {
            UInt32 result = NativeMethods.StringObject_GetValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(value);
        }

        public void SetValue(PdfBuffer value)
        {
            UInt32 result = NativeMethods.StringObject_SetValue(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public static PdfStringObject FromObject(PdfObject data)
        {
            return new PdfStringObject(data.Handle);
        }

        private static class NativeMethods
        {
            public static GetTypeDelgate StringObject_GetType = LibraryInstance.GetFunction<GetTypeDelgate>("StringObject_GetType");
            public static GetValueDelgate StringObject_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("StringObject_GetValue");
            public static SetValueDelgate StringObject_SetValue = LibraryInstance.GetFunction<SetValueDelgate>("StringObject_SetValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetTypeDelgate(PdfStringObjectSafeHandle handle, out PdfStringType data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfStringObjectSafeHandle handle, out PdfBufferSafeHandle value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetValueDelgate(PdfStringObjectSafeHandle handle, PdfBufferSafeHandle value);
        }
    }
}
