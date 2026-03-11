using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics.Extensions
{
    /// <summary>
    /// Extension methods for PdfField type checking and conversion.
    /// </summary>
    public static class PdfFieldExtensions
    {
        /// <summary>
        /// Checks if the field is of the specified type.
        /// </summary>
        public static bool Is<T>(this PdfField field) where T : PdfField
        {
            using (var upgraded = field.Upgrade()) {
                return upgraded is T;
            }
        }

        /// <summary>
        /// Returns the field as the specified type, or null if type doesn't match.
        /// Caller must dispose the returned object.
        /// </summary>
        public static T As<T>(this PdfField field) where T : PdfField
        {
            var upgraded = field.Upgrade();
            if (upgraded is T result) {
                return result;
            }
            upgraded.Dispose();
            return null;
        }

        /// <summary>
        /// Upgrades to the most derived field type.
        /// </summary>
        internal static PdfField Upgrade(this PdfField field)
        {
            var fieldType = field.GetFieldType();
            switch (fieldType) {
                case PdfFieldType.NonTerminal:
                    return PdfNonTerminalField.FromField(field);
                case PdfFieldType.Button:
                    return PdfButtonField.FromField(field);
                case PdfFieldType.Text:
                    return PdfTextField.FromField(field);
                case PdfFieldType.Choice:
                    return PdfChoiceField.FromField(field);
                case PdfFieldType.Signature:
                    return PdfSignatureField.FromField(field);
                default:
                    throw new PdfManagedException($"Cannot upgrade field with unsupported type: {fieldType}");
            }
        }
    }
}
