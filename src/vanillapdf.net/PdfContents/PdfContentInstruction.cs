using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public enum PdfContentInstructionType
    {
        Undefined = 0,
        Operation,
        Object
    };

    public class PdfContentInstruction : PdfUnknown
    {
        internal PdfContentInstruction(PdfContentInstructionSafeHandle handle) : base(handle)
        {
        }

        static PdfContentInstruction()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfContentInstructionType GetInstructionType()
        {
            UInt32 result = NativeMethods.ContentInstruction_GetInstructionType(Handle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfContentInstructionType>.CheckedCast(data);
        }

        private static class NativeMethods
        {
            public static GetInstructionTypeDelgate ContentInstruction_GetInstructionType = LibraryInstance.GetFunction<GetInstructionTypeDelgate>("ContentInstruction_GetInstructionType");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetInstructionTypeDelgate(PdfContentInstructionSafeHandle handle, out Int32 data);
        }
    }
}
