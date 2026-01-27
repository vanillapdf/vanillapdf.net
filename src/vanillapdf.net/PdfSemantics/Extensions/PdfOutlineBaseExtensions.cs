using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics.Extensions
{
    /// <summary>
    /// Extension methods for PdfOutlineBase type checking and conversion.
    /// </summary>
    public static class PdfOutlineBaseExtensions
    {
        /// <summary>
        /// Checks if the outline base is of the specified type.
        /// </summary>
        public static bool Is<T>(this PdfOutlineBase outlineBase) where T : PdfOutlineBase
        {
            using (var upgraded = outlineBase.Upgrade()) {
                return upgraded is T;
            }
        }

        /// <summary>
        /// Returns the outline base as the specified type, or null if type doesn't match.
        /// Caller must dispose the returned object.
        /// </summary>
        public static T As<T>(this PdfOutlineBase outlineBase) where T : PdfOutlineBase
        {
            var upgraded = outlineBase.Upgrade();
            if (upgraded is T result) {
                return result;
            }
            upgraded.Dispose();
            return null;
        }

        /// <summary>
        /// Upgrades to the most derived outline type (Outline or OutlineItem).
        /// </summary>
        internal static PdfOutlineBase Upgrade(this PdfOutlineBase outlineBase)
        {
            var outlineType = outlineBase.GetOutlineType();
            switch (outlineType) {
                case PdfOutlineType.Outline:
                    return PdfOutline.FromOutlineBase(outlineBase);
                case PdfOutlineType.Item:
                    return PdfOutlineItem.FromOutlineBase(outlineBase);
                default:
                    throw new PdfManagedException($"Cannot upgrade outline base with unknown type: {outlineType}");
            }
        }
    }
}
