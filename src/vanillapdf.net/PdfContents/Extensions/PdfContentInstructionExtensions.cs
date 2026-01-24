namespace vanillapdf.net.PdfContents.Extensions
{
    /// <summary>
    /// Extension methods for PdfContentInstruction type checking and conversion.
    /// </summary>
    public static class PdfContentInstructionExtensions
    {
        /// <summary>
        /// Checks if the instruction is of the specified type.
        /// </summary>
        public static bool Is<T>(this PdfContentInstruction instruction) where T : PdfContentInstruction
        {
            using (var upgraded = instruction.Upgrade()) {
                return upgraded is T;
            }
        }

        /// <summary>
        /// Returns the instruction as the specified type, or null if type doesn't match.
        /// Caller must dispose the returned object.
        /// </summary>
        public static T As<T>(this PdfContentInstruction instruction) where T : PdfContentInstruction
        {
            var upgraded = instruction.Upgrade();
            if (upgraded is T result) {
                return result;
            }
            upgraded.Dispose();
            return null;
        }

        /// <summary>
        /// Upgrades to PdfContentOperation or PdfContentObject.
        /// </summary>
        internal static PdfContentInstruction Upgrade(this PdfContentInstruction instruction)
        {
            var instructionType = instruction.GetInstructionType();
            switch (instructionType) {
                case PdfContentInstructionType.Operation:
                    return PdfContentOperation.FromContentInstruction(instruction);
                case PdfContentInstructionType.Object:
                    return PdfContentObject.FromContentInstruction(instruction);
                default:
                    throw new vanillapdf.net.Utils.PdfManagedException($"Cannot upgrade instruction with unknown type: {instructionType}");
            }
        }
    }
}
