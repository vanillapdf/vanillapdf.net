using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;
using vanillapdf.net.Utils.SafeHandles;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfIntegerObject : PdfObject
    {
        internal PdfIntegerObject(PdfIntegerObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfIntegerObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public Int64 IntegerValue
        {
            get { return GetIntegerValue(); }
            set { SetIntegerValue(value); }
        }

        public UInt64 UnsignedIntegerValue
        {
            get { return GetUnsignedIntegerValue(); }
            set { SetUnsignedIntegerValue(value); }
        }

        public static PdfIntegerObject Create()
        {
            UInt32 result = NativeMethods.IntegerObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfIntegerObject(data);
        }

        public Int64 GetIntegerValue()
        {
            UInt32 result = NativeMethods.IntegerObject_GetIntegerValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return value;
        }

        public void SetIntegerValue(Int64 value)
        {
            UInt32 result = NativeMethods.IntegerObject_SetIntegerValue(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public UInt64 GetUnsignedIntegerValue()
        {
            UInt32 result = NativeMethods.IntegerObject_GetUnsignedIntegerValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return value;
        }

        public void SetUnsignedIntegerValue(UInt64 value)
        {
            UInt32 result = NativeMethods.IntegerObject_SetUnsignedIntegerValue(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public static PdfIntegerObject FromObject(PdfObject data)
        {
            return new PdfIntegerObject(data.Handle);
        }

        private static class NativeMethods
        {
            public static CreateDelgate IntegerObject_Create = LibraryInstance.GetFunction<CreateDelgate>("IntegerObject_Create");
            public static GetIntegerValueDelgate IntegerObject_GetIntegerValue = LibraryInstance.GetFunction<GetIntegerValueDelgate>("IntegerObject_GetIntegerValue");
            public static SetIntegerValueDelgate IntegerObject_SetIntegerValue = LibraryInstance.GetFunction<SetIntegerValueDelgate>("IntegerObject_SetIntegerValue");

            public static GetUnsignedIntegerValueDelgate IntegerObject_GetUnsignedIntegerValue = LibraryInstance.GetFunction<GetUnsignedIntegerValueDelgate>("IntegerObject_GetUnsignedIntegerValue");
            public static SetUnsignedIntegerValueDelgate IntegerObject_SetUnsignedIntegerValue = LibraryInstance.GetFunction<SetUnsignedIntegerValueDelgate>("IntegerObject_SetUnsignedIntegerValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfIntegerObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetIntegerValueDelgate(PdfIntegerObjectSafeHandle handle, out Int64 value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetIntegerValueDelgate(PdfIntegerObjectSafeHandle handle, Int64 value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetUnsignedIntegerValueDelgate(PdfIntegerObjectSafeHandle handle, out UInt64 value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetUnsignedIntegerValueDelgate(PdfIntegerObjectSafeHandle handle, UInt64 value);
        }
    }
}
