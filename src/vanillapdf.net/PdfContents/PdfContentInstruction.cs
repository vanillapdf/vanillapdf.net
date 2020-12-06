using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Available content instruction types
    /// </summary>
    public enum PdfContentInstructionType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Atomic content operation.
        /// </summary>
        Operation,

        /// <summary>
        /// Composed non-atomic content object
        /// </summary>
        Object
    };

    /// <summary>
    /// Base class for all content objects and operations.
    /// </summary>
    public class PdfContentInstruction : PdfUnknown
    {
        internal PdfContentInstruction(PdfContentInstructionSafeHandle handle) : base(handle)
        {
        }

        static PdfContentInstruction()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentInstructionSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of current content instruction on success, throws exception on failure</returns>
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
