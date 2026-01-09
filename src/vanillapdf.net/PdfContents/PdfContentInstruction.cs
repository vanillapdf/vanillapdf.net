using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Base class for all content objects and operations.
    /// </summary>
    public class PdfContentInstruction : PdfUnknown
    {
        internal PdfContentInstructionSafeHandle InstructionHandle { get; }

        private PdfContentInstructionType? _cachedInstructionType;

        internal PdfContentInstruction(PdfContentInstructionSafeHandle handle) : base(handle)
        {
            InstructionHandle = handle;
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
            if (_cachedInstructionType.HasValue) {
                return _cachedInstructionType.Value;
            }

            UInt32 result = NativeMethods.ContentInstruction_GetInstructionType(InstructionHandle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            _cachedInstructionType = EnumUtil<PdfContentInstructionType>.CheckedCast(data);
            return _cachedInstructionType.Value;
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            InstructionHandle?.Dispose();
        }

        private static class NativeMethods
        {
            public static GetInstructionTypeDelgate ContentInstruction_GetInstructionType = LibraryInstance.GetFunction<GetInstructionTypeDelgate>("ContentInstruction_GetInstructionType");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetInstructionTypeDelgate(PdfContentInstructionSafeHandle handle, out Int32 data);
        }
    }
}
