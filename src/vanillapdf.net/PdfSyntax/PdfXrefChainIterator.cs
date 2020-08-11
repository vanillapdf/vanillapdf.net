using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public class PdfXrefChainIterator : PdfUnknown
    {
        internal PdfXrefChainIterator(PdfXrefChainIteratorSafeHandle handle) : base(handle)
        {
        }

        static PdfXrefChainIterator()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfXref GetValue()
        {
            UInt32 result = NativeMethods.XrefChainIterator_GetValue(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXref(data);
        }

        public void Next()
        {
            UInt32 result = NativeMethods.XrefChainIterator_Next(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private static class NativeMethods
        {
            public static GetValueDelgate XrefChainIterator_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("XrefChainIterator_GetValue");
            public static NextDelgate XrefChainIterator_Next = LibraryInstance.GetFunction<NextDelgate>("XrefChainIterator_Next");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfXrefChainIteratorSafeHandle handle, out PdfXrefSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 NextDelgate(PdfXrefChainIteratorSafeHandle handle);
        }
    }
}
