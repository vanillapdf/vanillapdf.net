using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfContents;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    public class PdfPageContents : PdfUnknown
    {
        internal PdfPageContents(PdfPageContentsSafeHandle handle) : base(handle)
        {
        }

        static PdfPageContents()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public UInt64 GetInstructionsSize()
        {
            UInt32 result = NativeMethods.Contents_GetInstructionsSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        public PdfContentInstruction GetInstructionAt(UInt64 index)
        {
            UInt32 result = NativeMethods.Contents_GetInstructionAt(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentInstruction(data);
        }

        private static class NativeMethods
        {
            public static GetInstructionsSizeDelgate Contents_GetInstructionsSize = LibraryInstance.GetFunction<GetInstructionsSizeDelgate>("PageContents_GetInstructionsSize");
            public static GetInstructionAtDelgate Contents_GetInstructionAt = LibraryInstance.GetFunction<GetInstructionAtDelgate>("PageContents_GetInstructionAt");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetInstructionsSizeDelgate(PdfPageContentsSafeHandle handle, out UIntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetInstructionAtDelgate(PdfPageContentsSafeHandle handle, UIntPtr at, out PdfContentInstructionSafeHandle data);
        }
    }
}
