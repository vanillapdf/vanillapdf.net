using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax.Extensions
{
    /// <summary>
    /// Extension methods for PdfStringObject providing explicit type upgrading and type checking.
    /// </summary>
    public static class PdfStringObjectExtensions
    {
        /// <summary>
        /// Upgrades to PdfLiteralStringObject or PdfHexadecimalStringObject.
        /// </summary>
        /// <remarks>
        /// Always creates a new wrapper object. Callers should check if upgrade is needed
        /// before calling (e.g., use Is* methods or check the string type directly).
        /// </remarks>
        /// <exception cref="PdfManagedException">Thrown if the string type is unknown.</exception>
        public static PdfStringObject UpgradeString(this PdfStringObject obj)
        {
            var stringType = obj.GetStringType();

            switch (stringType) {
                case PdfStringType.Literal:
                    return PdfLiteralStringObject.FromString(obj);
                case PdfStringType.Hexadecimal:
                    return PdfHexadecimalStringObject.FromString(obj);
                default:
                    throw new PdfManagedException($"Cannot upgrade string with unknown type: {stringType}");
            }
        }

        /// <summary>
        /// Checks if the string is a literal string.
        /// </summary>
        public static bool IsLiteral(this PdfStringObject obj)
        {
            return obj.GetStringType() == PdfStringType.Literal;
        }

        /// <summary>
        /// Returns the string as a literal string, or null if not a literal string.
        /// </summary>
        public static PdfLiteralStringObject AsLiteral(this PdfStringObject obj)
        {
            return obj.IsLiteral() ? PdfLiteralStringObject.FromString(obj) : null;
        }

        /// <summary>
        /// Checks if the string is a hexadecimal string.
        /// </summary>
        public static bool IsHexadecimal(this PdfStringObject obj)
        {
            return obj.GetStringType() == PdfStringType.Hexadecimal;
        }

        /// <summary>
        /// Returns the string as a hexadecimal string, or null if not a hexadecimal string.
        /// </summary>
        public static PdfHexadecimalStringObject AsHexadecimal(this PdfStringObject obj)
        {
            return obj.IsHexadecimal() ? PdfHexadecimalStringObject.FromString(obj) : null;
        }
    }
}
