using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfNameObject : PdfObject
    {
        internal PdfNameObject(PdfNameObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfNameObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfBuffer Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        public static PdfNameObject Create()
        {
            UInt32 result = NativeMethods.NameObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(data);
        }

        public PdfBuffer GetValue()
        {
            UInt32 result = NativeMethods.NameObject_GetValue(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        public void SetValue(PdfBuffer data)
        {
            UInt32 result = NativeMethods.NameObject_SetValue(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public bool Equals(PdfNameObject other)
        {
            UInt32 result = NativeMethods.NameObject_Equals(Handle, other.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private static class NativeMethods
        {
            public static CreateDelgate NameObject_Create = LibraryInstance.GetFunction<CreateDelgate>("NameObject_Create");
            public static GetValueDelgate NameObject_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("NameObject_GetValue");
            public static SetValueDelgate NameObject_SetValue = LibraryInstance.GetFunction<SetValueDelgate>("NameObject_SetValue");
            public static EqualsDelgate NameObject_Equals = LibraryInstance.GetFunction<EqualsDelgate>("NameObject_Equals");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfNameObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfNameObjectSafeHandle handle, out PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetValueDelgate(PdfNameObjectSafeHandle handle, PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 EqualsDelgate(PdfNameObjectSafeHandle handle, PdfNameObjectSafeHandle other, out bool data);
        }
    }
}
