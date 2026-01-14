using System;
using vanillapdf.net.Interop;
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

        internal PdfContentInstruction(PdfContentInstructionSafeHandle handle) : base(handle)
        {
            InstructionHandle = handle;
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of current content instruction on success, throws exception on failure</returns>
        public PdfContentInstructionType GetInstructionType()
        {
            UInt32 result = NativeMethods.ContentInstruction_GetInstructionType(InstructionHandle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfContentInstructionType>.CheckedCast(data);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            InstructionHandle?.Dispose();
        }
    }
}
