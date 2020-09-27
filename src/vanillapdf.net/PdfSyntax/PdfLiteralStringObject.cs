using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfLiteralStringObject : PdfStringObject
    {
        internal PdfLiteralStringObject(PdfLiteralStringObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfLiteralStringObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public static PdfLiteralStringObject Create()
        {
            UInt32 result = NativeMethods.LiteralStringObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        public static PdfLiteralStringObject CreateFromEncodedBuffer(PdfBuffer value)
        {
            UInt32 result = NativeMethods.LiteralStringObject_CreateFromEncodedBuffer(value.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        public static PdfLiteralStringObject CreateFromEncodedString(string value)
        {
            UInt32 result = NativeMethods.LiteralStringObject_CreateFromEncodedString(value, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        public static PdfLiteralStringObject CreateFromDecodedBuffer(PdfBuffer value)
        {
            UInt32 result = NativeMethods.LiteralStringObject_CreateFromDecodedBuffer(value.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        public static PdfLiteralStringObject CreateFromDecodedString(string value)
        {
            UInt32 result = NativeMethods.LiteralStringObject_CreateFromDecodedString(value, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        public static implicit operator PdfLiteralStringObject(string value)
        {
            return CreateFromDecodedString(value);
        }

        public static PdfLiteralStringObject FromString(PdfStringObject data)
        {
            return new PdfLiteralStringObject(data.Handle);
        }

        private static class NativeMethods
        {
            public static CreateDelgate LiteralStringObject_Create = LibraryInstance.GetFunction<CreateDelgate>("LiteralStringObject_Create");

            public static CreateFromEncodedBufferDelgate LiteralStringObject_CreateFromEncodedBuffer = LibraryInstance.GetFunction<CreateFromEncodedBufferDelgate>("LiteralStringObject_CreateFromEncodedBuffer");
            public static CreateFromEncodedStringDelgate LiteralStringObject_CreateFromEncodedString = LibraryInstance.GetFunction<CreateFromEncodedStringDelgate>("LiteralStringObject_CreateFromEncodedString");

            public static CreateFromDecodedBufferDelgate LiteralStringObject_CreateFromDecodedBuffer = LibraryInstance.GetFunction<CreateFromDecodedBufferDelgate>("LiteralStringObject_CreateFromDecodedBuffer");
            public static CreateFromDecodedStringDelgate LiteralStringObject_CreateFromDecodedString = LibraryInstance.GetFunction<CreateFromDecodedStringDelgate>("LiteralStringObject_CreateFromDecodedString");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfLiteralStringObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromEncodedBufferDelgate(PdfBufferSafeHandle data, out PdfLiteralStringObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromEncodedStringDelgate(string data, out PdfLiteralStringObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromDecodedBufferDelgate(PdfBufferSafeHandle data, out PdfLiteralStringObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromDecodedStringDelgate(string data, out PdfLiteralStringObjectSafeHandle handle);
        }
    }
}
