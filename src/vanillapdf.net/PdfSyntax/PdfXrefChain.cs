using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfXrefChain : PdfUnknown, IEnumerable<PdfXref>
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

        #region IEnumerable<PdfXref>

        IEnumerator<PdfXref> IEnumerable<PdfXref>.GetEnumerator()
        {
            return GetIterator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetIterator();
        }

        #endregion

        private static class NativeMethods
        {
            public static IteratorDelgate XrefChain_Iterator = LibraryInstance.GetFunction<IteratorDelgate>("XrefChain_GetIterator");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IteratorDelgate(PdfXrefChainSafeHandle handle, out PdfXrefChainIteratorSafeHandle data);
        }
    }
}
