using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public class PdfXrefChain : PdfUnknown
    {
        internal PdfXrefChain(PdfXrefChainSafeHandle handle) : base(handle)
        {
        }

        static PdfXrefChain()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfXrefChainIterator GetIterator()
        {
            UInt32 result = NativeMethods.XrefChain_Iterator(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXrefChainIterator(value);
        }

        public bool IsIteratorValid(PdfXrefChainIterator iterator)
        {
            UInt32 result = NativeMethods.XrefChain_IsIteratorValid(Handle, iterator.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private static class NativeMethods
        {
            public static IteratorDelgate XrefChain_Iterator = LibraryInstance.GetFunction<IteratorDelgate>("XrefChain_Iterator");
            public static IsIteratorValidDelgate XrefChain_IsIteratorValid = LibraryInstance.GetFunction<IsIteratorValidDelgate>("XrefChain_IsIteratorValid");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IteratorDelgate(PdfXrefChainSafeHandle handle, out PdfXrefChainIteratorSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IsIteratorValidDelgate(PdfXrefChainSafeHandle handle, PdfXrefChainIteratorSafeHandle iterator_handle, out bool data);
        }
    }
}
