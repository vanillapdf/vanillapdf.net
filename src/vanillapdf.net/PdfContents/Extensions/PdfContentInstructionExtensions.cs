using vanillapdf.net.PdfContents;

namespace vanillapdf.net.PdfContents.Extensions
{
    /// <summary>
    /// Extension methods for PdfContentInstruction providing explicit type upgrading and type checking.
    /// </summary>
    public static class PdfContentInstructionExtensions
    {
        /// <summary>
        /// Upgrades to PdfContentOperation or PdfContentObject.
        /// </summary>
        /// <param name="instruction">The instruction to upgrade.</param>
        /// <returns>The upgraded instruction.</returns>
        public static PdfContentInstruction Upgrade(this PdfContentInstruction instruction)
        {
            switch (instruction.GetInstructionType()) {
                case PdfContentInstructionType.Operation:
                    return PdfContentOperation.FromContentInstruction(instruction);
                case PdfContentInstructionType.Object:
                    return PdfContentObject.FromContentInstruction(instruction);
                default:
                    return instruction;
            }
        }

        /// <summary>
        /// Checks if the instruction is an operation.
        /// </summary>
        public static bool IsOperation(this PdfContentInstruction instruction)
        {
            return instruction.GetInstructionType() == PdfContentInstructionType.Operation;
        }

        /// <summary>
        /// Returns the instruction as an operation, or null if not an operation.
        /// </summary>
        public static PdfContentOperation AsOperation(this PdfContentInstruction instruction)
        {
            return instruction.IsOperation() ? PdfContentOperation.FromContentInstruction(instruction) : null;
        }

        /// <summary>
        /// Checks if the instruction is an object.
        /// </summary>
        public static bool IsObject(this PdfContentInstruction instruction)
        {
            return instruction.GetInstructionType() == PdfContentInstructionType.Object;
        }

        /// <summary>
        /// Returns the instruction as an object, or null if not an object.
        /// </summary>
        public static PdfContentObject AsObject(this PdfContentInstruction instruction)
        {
            return instruction.IsObject() ? PdfContentObject.FromContentInstruction(instruction) : null;
        }
    }
}
