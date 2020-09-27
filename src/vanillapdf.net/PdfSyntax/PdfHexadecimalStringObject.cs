using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfHexadecimalStringObject : PdfStringObject
    {
        internal PdfHexadecimalStringObject(PdfHexadecimalStringObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfHexadecimalStringObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public static PdfHexadecimalStringObject Create()
        {
            UInt32 result = NativeMethods.HexadecimalStringObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        public static PdfHexadecimalStringObject CreateFromEncodedBuffer(PdfBuffer value)
        {
            UInt32 result = NativeMethods.HexadecimalStringObject_CreateFromEncodedBuffer(value.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        public static PdfHexadecimalStringObject CreateFromEncodedString(string value)
        {
            UInt32 result = NativeMethods.HexadecimalStringObject_CreateFromEncodedString(value, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        public static PdfHexadecimalStringObject CreateFromDecodedBuffer(PdfBuffer value)
        {
            UInt32 result = NativeMethods.HexadecimalStringObject_CreateFromDecodedBuffer(value.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        public static PdfHexadecimalStringObject CreateFromDecodedString(string value)
        {
            UInt32 result = NativeMethods.HexadecimalStringObject_CreateFromDecodedString(value, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        public static implicit operator PdfHexadecimalStringObject(string value)
        {
            return CreateFromDecodedString(value);
        }

        public static PdfHexadecimalStringObject FromString(PdfStringObject data)
        {
            return new PdfHexadecimalStringObject(data.Handle);
        }

        private static class NativeMethods
        {
            public static CreateDelgate HexadecimalStringObject_Create = LibraryInstance.GetFunction<CreateDelgate>("HexadecimalStringObject_Create");

            public static CreateFromEncodedBufferDelgate HexadecimalStringObject_CreateFromEncodedBuffer = LibraryInstance.GetFunction<CreateFromEncodedBufferDelgate>("HexadecimalStringObject_CreateFromEncodedBuffer");
            public static CreateFromEncodedStringDelgate HexadecimalStringObject_CreateFromEncodedString = LibraryInstance.GetFunction<CreateFromEncodedStringDelgate>("HexadecimalStringObject_CreateFromEncodedString");

            public static CreateFromDecodedBufferDelgate HexadecimalStringObject_CreateFromDecodedBuffer = LibraryInstance.GetFunction<CreateFromDecodedBufferDelgate>("HexadecimalStringObject_CreateFromDecodedBuffer");
            public static CreateFromDecodedStringDelgate HexadecimalStringObject_CreateFromDecodedString = LibraryInstance.GetFunction<CreateFromDecodedStringDelgate>("HexadecimalStringObject_CreateFromDecodedString");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfHexadecimalStringObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromEncodedBufferDelgate(PdfBufferSafeHandle data, out PdfHexadecimalStringObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromEncodedStringDelgate(string data, out PdfHexadecimalStringObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromDecodedBufferDelgate(PdfBufferSafeHandle data, out PdfHexadecimalStringObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromDecodedStringDelgate(string data, out PdfHexadecimalStringObjectSafeHandle handle);
        }
    }
}
