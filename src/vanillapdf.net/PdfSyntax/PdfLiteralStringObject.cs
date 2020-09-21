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

        public static PdfLiteralStringObject FromString(PdfStringObject data)
        {
            return new PdfLiteralStringObject(data.Handle);
        }

        private static class NativeMethods
        {
            public static CreateDelgate LiteralStringObject_Create = LibraryInstance.GetFunction<CreateDelgate>("LiteralStringObject_Create");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfLiteralStringObjectSafeHandle handle);
        }
    }
}
