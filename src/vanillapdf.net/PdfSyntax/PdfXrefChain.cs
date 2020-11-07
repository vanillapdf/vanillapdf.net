using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;
using vanillapdf.net.Utils.SafeHandles;

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
            UInt32 result = NativeMethods.XrefChain_GetIterator(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXrefChainIterator(value);
        }

        #region IEnumerable<PdfXref>

        public IEnumerator<PdfXref> GetEnumerator()
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
            public static GetIteratorDelgate XrefChain_GetIterator = LibraryInstance.GetFunction<GetIteratorDelgate>("XrefChain_GetIterator");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetIteratorDelgate(PdfXrefChainSafeHandle handle, out PdfXrefChainIteratorSafeHandle data);
        }
    }
}
