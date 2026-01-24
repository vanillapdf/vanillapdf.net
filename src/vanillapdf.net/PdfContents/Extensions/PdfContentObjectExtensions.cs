using vanillapdf.net.PdfContents;

namespace vanillapdf.net.PdfContents.Extensions
{
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
}
