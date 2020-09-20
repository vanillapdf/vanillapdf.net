using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfBooleanObject : PdfObject
    {
        internal PdfBooleanObject(PdfBooleanObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfBooleanObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public bool Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        public static PdfBooleanObject Create()
        {
            UInt32 result = NativeMethods.BooleanObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBooleanObject(data);
        }

        public bool GetValue()
        {
            UInt32 result = NativeMethods.BooleanObject_GetValue(Handle, out bool data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public void SetValue(bool data)
        {
            UInt32 result = NativeMethods.BooleanObject_SetValue(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private static class NativeMethods
        {
            public static CreateDelgate BooleanObject_Create = LibraryInstance.GetFunction<CreateDelgate>("BooleanObject_Create");
            public static GetValueDelgate BooleanObject_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("BooleanObject_GetValue");
            public static SetValueDelgate BooleanObject_SetValue = LibraryInstance.GetFunction<SetValueDelgate>("BooleanObject_SetValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfBooleanObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfBooleanObjectSafeHandle handle, out bool data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetValueDelgate(PdfBooleanObjectSafeHandle handle, bool data);
        }
    }
}
