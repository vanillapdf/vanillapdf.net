namespace vanillapdf.net.PdfContents
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

    /// <summary>
    /// Extension methods for PdfContentObject providing explicit type upgrading and type checking.
    /// </summary>
    public static class PdfContentObjectExtensions
    {
        /// <summary>
        /// Upgrades to PdfContentObjectText or other derived types.
        /// </summary>
        /// <param name="obj">The content object to upgrade.</param>
        /// <returns>The upgraded content object.</returns>
        public static PdfContentObject UpgradeObject(this PdfContentObject obj)
        {
            switch (obj.GetObjectType()) {
                case PdfContentObjectType.Text:
                    return PdfContentObjectText.FromContentObject(obj);
                // InlineImage type exists in enum but no corresponding class found
                default:
                    return obj;
            }
        }

        /// <summary>
        /// Checks if the content object is a text object.
        /// </summary>
        public static bool IsText(this PdfContentObject obj)
        {
            return obj.GetObjectType() == PdfContentObjectType.Text;
        }

        /// <summary>
        /// Returns the content object as a text object, or null if not a text object.
        /// </summary>
        public static PdfContentObjectText AsText(this PdfContentObject obj)
        {
            return obj.IsText() ? PdfContentObjectText.FromContentObject(obj) : null;
        }

        /// <summary>
        /// Checks if the content object is an inline image.
        /// </summary>
        public static bool IsInlineImage(this PdfContentObject obj)
        {
            return obj.GetObjectType() == PdfContentObjectType.InlineImage;
        }
    }

    /// <summary>
    /// Extension methods for PdfContentOperation providing explicit type upgrading and type checking.
    /// </summary>
    public static class PdfContentOperationExtensions
    {
        /// <summary>
        /// Upgrades to the most derived operation type (Generic, TextShow, TextShowArray, TextFont).
        /// </summary>
        /// <param name="operation">The operation to upgrade.</param>
        /// <returns>The upgraded operation.</returns>
        public static PdfContentOperation UpgradeOperation(this PdfContentOperation operation)
        {
            switch (operation.GetOperationType()) {
                case PdfContentOperationType.Generic:
                    return PdfContentOperationGeneric.FromContentOperation(operation);
                case PdfContentOperationType.TextShow:
                    return PdfContentOperationTextShow.FromContentOperation(operation);
                case PdfContentOperationType.TextShowArray:
                    return PdfContentOperationTextShowArray.FromContentOperation(operation);
                case PdfContentOperationType.TextFont:
                    return PdfContentOperationTextFont.FromContentOperation(operation);
                default:
                    return operation;
            }
        }

        /// <summary>
        /// Checks if the operation is a generic operation.
        /// </summary>
        public static bool IsGeneric(this PdfContentOperation operation)
        {
            return operation.GetOperationType() == PdfContentOperationType.Generic;
        }

        /// <summary>
        /// Returns the operation as a generic operation, or null if not a generic operation.
        /// </summary>
        public static PdfContentOperationGeneric AsGeneric(this PdfContentOperation operation)
        {
            return operation.IsGeneric() ? PdfContentOperationGeneric.FromContentOperation(operation) : null;
        }

        /// <summary>
        /// Checks if the operation is a text show operation.
        /// </summary>
        public static bool IsTextShow(this PdfContentOperation operation)
        {
            return operation.GetOperationType() == PdfContentOperationType.TextShow;
        }

        /// <summary>
        /// Returns the operation as a text show operation, or null if not a text show operation.
        /// </summary>
        public static PdfContentOperationTextShow AsTextShow(this PdfContentOperation operation)
        {
            return operation.IsTextShow() ? PdfContentOperationTextShow.FromContentOperation(operation) : null;
        }

        /// <summary>
        /// Checks if the operation is a text show array operation.
        /// </summary>
        public static bool IsTextShowArray(this PdfContentOperation operation)
        {
            return operation.GetOperationType() == PdfContentOperationType.TextShowArray;
        }

        /// <summary>
        /// Returns the operation as a text show array operation, or null if not a text show array operation.
        /// </summary>
        public static PdfContentOperationTextShowArray AsTextShowArray(this PdfContentOperation operation)
        {
            return operation.IsTextShowArray() ? PdfContentOperationTextShowArray.FromContentOperation(operation) : null;
        }

        /// <summary>
        /// Checks if the operation is a text font operation.
        /// </summary>
        public static bool IsTextFont(this PdfContentOperation operation)
        {
            return operation.GetOperationType() == PdfContentOperationType.TextFont;
        }

        /// <summary>
        /// Returns the operation as a text font operation, or null if not a text font operation.
        /// </summary>
        public static PdfContentOperationTextFont AsTextFont(this PdfContentOperation operation)
        {
            return operation.IsTextFont() ? PdfContentOperationTextFont.FromContentOperation(operation) : null;
        }
    }
}
