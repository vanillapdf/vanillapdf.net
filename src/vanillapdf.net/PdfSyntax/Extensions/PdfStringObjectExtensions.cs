using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax.Extensions
{
    /// <summary>
    /// Extension methods for PdfStringObject type checking and conversion.
    /// </summary>
    public static class PdfStringObjectExtensions
    {
        /// <summary>
        /// Checks if the string is of the specified type.
        /// </summary>
        public static bool Is<T>(this PdfStringObject obj) where T : PdfStringObject
        {
            using (var upgraded = obj.Upgrade()) {
                return upgraded is T;
            }
        }

        /// <summary>
        /// Returns the string as the specified type, or null if type doesn't match.
        /// Caller must dispose the returned object.
        /// </summary>
        public static T As<T>(this PdfStringObject obj) where T : PdfStringObject
        {
            var upgraded = obj.Upgrade();
            if (upgraded is T result) {
                return result;
            }
            upgraded.Dispose();
            return null;
        }

        /// <summary>
        /// Upgrades to PdfLiteralStringObject or PdfHexadecimalStringObject.
        /// </summary>
        /// <exception cref="PdfManagedException">Thrown if the string type is unknown.</exception>
        internal static PdfStringObject Upgrade(this PdfStringObject obj)
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
    }
}
