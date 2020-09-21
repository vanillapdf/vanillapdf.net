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

        public static PdfHexadecimalStringObject FromString(PdfStringObject data)
        {
            return new PdfHexadecimalStringObject(data.Handle);
        }

        private static class NativeMethods
        {
            public static CreateDelgate HexadecimalStringObject_Create = LibraryInstance.GetFunction<CreateDelgate>("HexadecimalStringObject_Create");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfHexadecimalStringObjectSafeHandle handle);
        }
    }
}
