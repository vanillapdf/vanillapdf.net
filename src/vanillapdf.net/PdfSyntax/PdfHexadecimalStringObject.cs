using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// A hexadecimal string is preferable for arbitrary binary data
    /// </summary>
    public class PdfHexadecimalStringObject : PdfStringObject
    {
        internal PdfHexadecimalStringObjectSafeHandle Handle { get; }

        internal PdfHexadecimalStringObject(PdfHexadecimalStringObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfHexadecimalStringObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfHexadecimalStringObjectSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Create a new instance of \ref PdfHexadecimalStringObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfHexadecimalStringObject on success, throws exception on failure</returns>
        public static PdfHexadecimalStringObject Create()
        {
            UInt32 result = NativeMethods.HexadecimalStringObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        /// <summary>
        /// Create a new instance of \ref PdfLiteralStringObject from specified data in PDF format
        /// </summary>
        /// <param name="value">Handle to \ref PdfBuffer containing data in PDF format</param>
        /// <returns>New instance of \ref PdfHexadecimalStringObject on success, throws exception on failure</returns>
        public static PdfHexadecimalStringObject CreateFromEncodedBuffer(PdfBuffer value)
        {
            UInt32 result = NativeMethods.HexadecimalStringObject_CreateFromEncodedBuffer(value.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        /// <summary>
        /// Create a new instance of \ref PdfLiteralStringObject from specified data in PDF format
        /// </summary>
        /// <param name="value">A string containing data in PDF format</param>
        /// <returns>New instance of \ref PdfHexadecimalStringObject on success, throws exception on failure</returns>
        public static PdfHexadecimalStringObject CreateFromEncodedString(string value)
        {
            UInt32 result = NativeMethods.HexadecimalStringObject_CreateFromEncodedString(value, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        /// <summary>
        /// Create a new instance of \ref PdfLiteralStringObject from specified data in PDF format
        /// </summary>
        /// <param name="value">Handle to \ref PdfBuffer containing raw data after PDF parsing</param>
        /// <returns>New instance of \ref PdfHexadecimalStringObject on success, throws exception on failure</returns>
        public static PdfHexadecimalStringObject CreateFromDecodedBuffer(PdfBuffer value)
        {
            UInt32 result = NativeMethods.HexadecimalStringObject_CreateFromDecodedBuffer(value.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        /// <summary>
        /// Create a new instance of \ref PdfLiteralStringObject from specified data in PDF format
        /// </summary>
        /// <param name="value">A string containing raw data after PDF parsing</param>
        /// <returns>New instance of \ref PdfHexadecimalStringObject on success, throws exception on failure</returns>
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

        public override PdfObject ConvertTo<T>()
        {
            if (typeof(T) == typeof(PdfHexadecimalStringObject)) {
                return this;
            }

            return base.ConvertTo<T>();
        }

        /// <summary>
        /// Convert string object to hexadecimal string object
        /// </summary>
        /// <param name="data">Handle to \ref PdfStringObject to be converted</param>
        /// <returns>A new instance of \ref PdfHexadecimalStringObject if the object can be converted, throws exception on failure</returns>
        public static PdfHexadecimalStringObject FromString(PdfStringObject data)
        {
            return new PdfHexadecimalStringObject(data.StringHandle);
        }

        protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
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
