using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfXrefIterator : PdfUnknown
    {
        internal PdfXrefIterator(PdfXrefIteratorSafeHandle handle) : base(handle)
        {
        }

        static PdfXrefIterator()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfXrefEntry GetValue()
        {
            UInt32 result = NativeMethods.XrefIterator_GetValue(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfXrefEntry(data);
        }

        public void Next()
        {
            UInt32 result = NativeMethods.XrefIterator_Next(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private static class NativeMethods
        {
            public static GetValueDelgate XrefIterator_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("XrefIterator_GetValue");
            public static NextDelgate XrefIterator_Next = LibraryInstance.GetFunction<NextDelgate>("XrefIterator_Next");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfXrefIteratorSafeHandle handle, out PdfXrefEntrySafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 NextDelgate(PdfXrefIteratorSafeHandle handle);
        }
    }
}
