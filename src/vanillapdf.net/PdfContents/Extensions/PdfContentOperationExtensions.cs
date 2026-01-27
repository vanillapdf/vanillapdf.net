namespace vanillapdf.net.PdfContents.Extensions
{
    /// <summary>
    /// Extension methods for PdfContentOperation type checking and conversion.
    /// </summary>
    public static class PdfContentOperationExtensions
    {
        /// <summary>
        /// Checks if the operation is of the specified type.
        /// </summary>
        public static bool Is<T>(this PdfContentOperation operation) where T : PdfContentOperation
        {
            using (var upgraded = operation.Upgrade()) {
                return upgraded is T;
            }
        }

        /// <summary>
        /// Returns the operation as the specified type, or null if type doesn't match.
        /// Caller must dispose the returned object.
        /// </summary>
        public static T As<T>(this PdfContentOperation operation) where T : PdfContentOperation
        {
            var upgraded = operation.Upgrade();
            if (upgraded is T result) {
                return result;
            }
            upgraded.Dispose();
            return null;
        }

        /// <summary>
        /// Upgrades to the most derived operation type (Generic, TextShow, TextShowArray, TextFont).
        /// </summary>
        internal static PdfContentOperation Upgrade(this PdfContentOperation operation)
        {
            var operationType = operation.GetOperationType();
            switch (operationType) {
                case PdfContentOperationType.Generic:
                    return PdfContentOperationGeneric.FromContentOperation(operation);
                case PdfContentOperationType.TextShow:
                    return PdfContentOperationTextShow.FromContentOperation(operation);
                case PdfContentOperationType.TextShowArray:
                    return PdfContentOperationTextShowArray.FromContentOperation(operation);
                case PdfContentOperationType.TextFont:
                    return PdfContentOperationTextFont.FromContentOperation(operation);
                default:
                    throw new vanillapdf.net.Utils.PdfManagedException($"Cannot upgrade operation with unknown type: {operationType}");
            }
        }
    }
}
