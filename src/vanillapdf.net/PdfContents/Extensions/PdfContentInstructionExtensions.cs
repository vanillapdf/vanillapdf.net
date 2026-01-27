using vanillapdf.net.Utils;

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
        /// Upgrades to the most-derived type (full chain).
        /// Instruction -> Operation -> Generic/TextShow/TextShowArray/TextFont
        /// Instruction -> Object -> ObjectText
        /// </summary>
        /// <remarks>
        /// Benchmark (i7-10700KF, .NET 8.0, 19005-1_FAQ.PDF page 1, 167 instructions):
        ///
        /// | Method                    | Policy | Mean       | Allocated |
        /// |-------------------------- |------- |-----------:|----------:|
        /// | InstructionCollection_At  | None   |  32.6 us  |  11.58 KB |
        /// | InstructionCollection_At  | Single |  82.2 us  |  30.29 KB |
        /// | InstructionCollection_At  | Full   | 141.7 us  |  53.45 KB |
        /// | TextObject_GetOperationAt | None   |   2.6 us  |   1.13 KB |
        /// | TextObject_GetOperationAt | Single |   8.7 us  |   2.75 KB |
        /// | TextObject_GetOperationAt | Full   |   7.7 us  |   2.75 KB |
        ///
        /// Each upgrade level adds ~0.3 us per element. Full is the default because
        /// real-world usage (e.g. rendering) immediately needs the concrete type
        /// (TextShow, TextShowArray, Generic) to dispatch on.
        /// Controlled by <see cref="LibraryInstance.UpgradePolicy"/>.
        /// </remarks>
        internal static PdfContentInstruction Upgrade(this PdfContentInstruction instruction)
        {
            var instructionType = instruction.GetInstructionType();
            switch (instructionType) {
                case PdfContentInstructionType.Operation:
                    if (LibraryInstance.UpgradePolicy == UpgradePolicy.Full) {
                        using (var operation = PdfContentOperation.FromContentInstruction(instruction)) {
                            return operation.Upgrade();
                        }
                    }
                    return PdfContentOperation.FromContentInstruction(instruction);
                case PdfContentInstructionType.Object:
                    if (LibraryInstance.UpgradePolicy == UpgradePolicy.Full) {
                        using (var obj = PdfContentObject.FromContentInstruction(instruction)) {
                            return obj.Upgrade();
                        }
                    }
                    return PdfContentObject.FromContentInstruction(instruction);
                default:
                    throw new vanillapdf.net.Utils.PdfManagedException($"Cannot upgrade instruction with unknown type: {instructionType}");
            }
        }
    }
}
