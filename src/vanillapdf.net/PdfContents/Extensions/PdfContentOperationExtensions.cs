using vanillapdf.net.PdfContents;

namespace vanillapdf.net.PdfContents.Extensions
{
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
