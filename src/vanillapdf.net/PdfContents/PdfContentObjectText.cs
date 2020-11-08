using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;
using vanillapdf.net.Utils.SafeHandles;

namespace vanillapdf.net
{
    public class PdfContentObjectText : PdfContentObject
    {
        internal PdfContentObjectText(PdfContentObjectTextSafeHandle handle) : base(handle)
        {
        }

        static PdfContentObjectText()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public UInt64 GetOperationsSize()
        {
            UInt32 result = NativeMethods.ContentObjectText_GetOperationsSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        public PdfContentOperation GetOperationAt(UInt64 index)
        {
            UInt32 result = NativeMethods.ContentObjectText_GetOperationAt(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentOperation(data);
        }

        public static PdfContentObjectText FromContentObject(PdfContentObject data)
        {
            return new PdfContentObjectText(data.Handle);
        }

        private static class NativeMethods
        {
            public static GetOperationsSizeDelgate ContentObjectText_GetOperationsSize = LibraryInstance.GetFunction<GetOperationsSizeDelgate>("ContentObjectText_GetOperationsSize");
            public static GetOperationAtDelgate ContentObjectText_GetOperationAt = LibraryInstance.GetFunction<GetOperationAtDelgate>("ContentObjectText_GetOperationAt");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOperationsSizeDelgate(PdfContentObjectTextSafeHandle handle, out UIntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOperationAtDelgate(PdfContentObjectTextSafeHandle handle, UIntPtr at, out PdfContentOperationSafeHandle data);
        }
    }
}
