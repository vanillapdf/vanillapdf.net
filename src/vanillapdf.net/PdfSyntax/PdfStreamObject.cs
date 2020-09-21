using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfStreamObject : PdfObject
    {
        internal PdfStreamObject(PdfStreamObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfStreamObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfDictionaryObject Header
        {
            get { return GetHeader(); }
            set { SetHeader(value); }
        }

        public PdfBuffer Body
        {
            get { return GetBody(); }
            set { SetBody(value); }
        }

        public static PdfStreamObject Create()
        {
            UInt32 result = NativeMethods.StreamObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStreamObject(data);
        }

        public PdfDictionaryObject GetHeader()
        {
            UInt32 result = NativeMethods.StreamObject_GetHeader(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObject(value);
        }

        public void SetHeader(PdfDictionaryObject data)
        {
            UInt32 result = NativeMethods.StreamObject_SetHeader(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public PdfBuffer GetBodyRaw()
        {
            UInt32 result = NativeMethods.StreamObject_GetBodyRaw(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(value);
        }

        public PdfBuffer GetBody()
        {
            UInt32 result = NativeMethods.StreamObject_GetBody(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(value);
        }

        public void SetBody(PdfBuffer data)
        {
            UInt32 result = NativeMethods.StreamObject_SetBody(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public static PdfStreamObject FromObject(PdfObject data)
        {
            return new PdfStreamObject(data.Handle);
        }

        private static class NativeMethods
        {
            public static CreateDelgate StreamObject_Create = LibraryInstance.GetFunction<CreateDelgate>("StreamObject_Create");
            public static GetHeaderDelgate StreamObject_GetHeader = LibraryInstance.GetFunction<GetHeaderDelgate>("StreamObject_GetHeader");
            public static SetHeaderDelgate StreamObject_SetHeader = LibraryInstance.GetFunction<SetHeaderDelgate>("StreamObject_SetHeader");

            public static GetBodyRawDelgate StreamObject_GetBodyRaw = LibraryInstance.GetFunction<GetBodyRawDelgate>("StreamObject_GetBodyRaw");
            public static GetBodyDelgate StreamObject_GetBody = LibraryInstance.GetFunction<GetBodyDelgate>("StreamObject_GetBody");
            public static SetBodyDelgate StreamObject_SetBody = LibraryInstance.GetFunction<SetBodyDelgate>("StreamObject_SetBody");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfStreamObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetHeaderDelgate(PdfStreamObjectSafeHandle handle, out PdfDictionaryObjectSafeHandle value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetHeaderDelgate(PdfStreamObjectSafeHandle handle, PdfDictionaryObjectSafeHandle value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetBodyRawDelgate(PdfStreamObjectSafeHandle handle, out PdfBufferSafeHandle value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetBodyDelgate(PdfStreamObjectSafeHandle handle, out PdfBufferSafeHandle value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetBodyDelgate(PdfStreamObjectSafeHandle handle, PdfBufferSafeHandle value);
        }
    }
}
