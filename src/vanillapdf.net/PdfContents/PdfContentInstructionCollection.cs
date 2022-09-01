using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// TODO
    /// </summary>
    public class PdfContentInstructionCollection : PdfUnknown, IEnumerable<PdfContentInstruction>
    {
        internal PdfContentInstructionCollectionSafeHandle Handle { get; }

        internal PdfContentInstructionCollection(PdfContentInstructionCollectionSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfContentInstructionCollection()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentInstructionCollectionSafeHandle).TypeHandle);
        }

        public PdfContentInstructionCollectionIterator GetIterator()
        {
            UInt32 result = NativeMethods.ContentInstructionCollection_GetIterator(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentInstructionCollectionIterator(value);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #region IEnumerable<PdfXref>

        public IEnumerator<PdfContentInstruction> GetEnumerator()
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
            public static GetIteratorDelgate ContentInstructionCollection_GetIterator = LibraryInstance.GetFunction<GetIteratorDelgate>("ContentInstructionCollection_GetIterator");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetIteratorDelgate(PdfContentInstructionCollectionSafeHandle handle, out PdfContentInstructionCollectionIteratorSafeHandle data);
        }
    }
}
