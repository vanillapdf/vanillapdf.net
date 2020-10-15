using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public class PdfPKCS12Key : PdfUnknown
    {
        internal PdfPKCS12Key(PdfPKCS12KeySafeHandle handle) : base(handle)
        {
        }

        static PdfPKCS12Key()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public static PdfPKCS12Key CreateFromFile(string filename, string password)
        {
            UInt32 result = NativeMethods.PKCS12Key_CreateFromFile(filename, password, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfPKCS12Key(data);
        }

        public static PdfPKCS12Key CreateFromBuffer(PdfBuffer buffer, string password)
        {
            UInt32 result = NativeMethods.PKCS12Key_CreateFromBuffer(buffer.Handle, password, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfPKCS12Key(data);
        }

        public static implicit operator PdfSigningKey(PdfPKCS12Key data)
        {
            return new PdfSigningKey(data.Handle);
        }

        public static explicit operator PdfPKCS12Key(PdfSigningKey data)
        {
            return new PdfPKCS12Key(data.Handle);
        }

        private static class NativeMethods
        {
            public static CreateFromFileDelgate PKCS12Key_CreateFromFile = LibraryInstance.GetFunction<CreateFromFileDelgate>("PKCS12Key_CreateFromFile");
            public static CreateFromBufferDelgate PKCS12Key_CreateFromBuffer = LibraryInstance.GetFunction<CreateFromBufferDelgate>("PKCS12Key_CreateFromBuffer");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromFileDelgate(string filename, string password, out PdfPKCS12KeySafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromBufferDelgate(PdfBufferSafeHandle buffer, string password, out PdfPKCS12KeySafeHandle data);
        }
    }
}
