namespace vanillapdf.net.PdfContents.Extensions
{
    /// <summary>
    /// Extension methods for PdfContentObject type checking and conversion.
    /// </summary>
    public static class PdfContentObjectExtensions
    {
        /// <summary>
        /// Checks if the content object is of the specified type.
        /// </summary>
        public static bool Is<T>(this PdfContentObject obj) where T : PdfContentObject
        {
            using (var upgraded = obj.Upgrade()) {
                return upgraded is T;
            }
        }

        /// <summary>
        /// Returns the content object as the specified type, or null if type doesn't match.
        /// Caller must dispose the returned object.
        /// </summary>
        public static T As<T>(this PdfContentObject obj) where T : PdfContentObject
        {
            var upgraded = obj.Upgrade();
            if (upgraded is T result) {
                return result;
            }
            upgraded.Dispose();
            return null;
        }

        /// <summary>
        /// Upgrades to PdfContentObjectText or other derived types.
        /// </summary>
        internal static PdfContentObject Upgrade(this PdfContentObject obj)
        {
            var objectType = obj.GetObjectType();
            switch (objectType) {
                case PdfContentObjectType.Text:
                    return PdfContentObjectText.FromContentObject(obj);
                default:
                    throw new vanillapdf.net.Utils.PdfManagedException($"Cannot upgrade content object with unknown type: {objectType}");
            }
        }
    }
}
