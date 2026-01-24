using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax.Extensions
{
    /// <summary>
    /// Extension methods for PdfXrefEntry type checking and conversion.
    /// </summary>
    public static class PdfXrefEntryExtensions
    {
        /// <summary>
        /// Checks if the entry is of the specified type.
        /// </summary>
        public static bool Is<T>(this PdfXrefEntry entry) where T : PdfXrefEntry
        {
            using (var upgraded = entry.Upgrade()) {
                return upgraded is T;
            }
        }

        /// <summary>
        /// Returns the entry as the specified type, or null if type doesn't match.
        /// Caller must dispose the returned object.
        /// </summary>
        public static T As<T>(this PdfXrefEntry entry) where T : PdfXrefEntry
        {
            var upgraded = entry.Upgrade();
            if (upgraded is T result) {
                return result;
            }
            upgraded.Dispose();
            return null;
        }

        /// <summary>
        /// Upgrades to the most derived entry type.
        /// </summary>
        /// <exception cref="PdfManagedException">Thrown if the entry type is unknown.</exception>
        internal static PdfXrefEntry Upgrade(this PdfXrefEntry entry)
        {
            var entryType = entry.GetEntryType();

            switch (entryType) {
                case PdfXrefEntryType.Free:
                    return PdfXrefFreeEntry.FromEntry(entry);
                case PdfXrefEntryType.Used:
                    return PdfXrefUsedEntry.FromEntry(entry);
                case PdfXrefEntryType.Compressed:
                    return PdfXrefCompressedEntry.FromEntry(entry);
                default:
                    throw new PdfManagedException($"Cannot upgrade entry with unknown type: {entryType}");
            }
        }
    }
}
