using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax.Extensions
{
    /// <summary>
    /// Extension methods for PdfXrefEntry providing explicit type upgrading and type checking.
    /// </summary>
    public static class PdfXrefEntryExtensions
    {
        /// <summary>
        /// Upgrades to the most derived entry type.
        /// </summary>
        /// <remarks>
        /// Always creates a new wrapper object. Callers should check if upgrade is needed
        /// before calling (e.g., use Is* methods or check the entry type directly).
        /// </remarks>
        /// <exception cref="PdfManagedException">Thrown if the entry type is unknown.</exception>
        public static PdfXrefEntry Upgrade(this PdfXrefEntry entry)
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

        /// <summary>
        /// Checks if the entry is a free entry.
        /// </summary>
        public static bool IsFree(this PdfXrefEntry entry)
        {
            return entry.GetEntryType() == PdfXrefEntryType.Free;
        }

        /// <summary>
        /// Returns the entry as a free entry, or null if not a free entry.
        /// </summary>
        public static PdfXrefFreeEntry AsFree(this PdfXrefEntry entry)
        {
            return entry.IsFree() ? PdfXrefFreeEntry.FromEntry(entry) : null;
        }

        /// <summary>
        /// Checks if the entry is a used entry.
        /// </summary>
        public static bool IsUsed(this PdfXrefEntry entry)
        {
            return entry.GetEntryType() == PdfXrefEntryType.Used;
        }

        /// <summary>
        /// Returns the entry as a used entry, or null if not a used entry.
        /// </summary>
        public static PdfXrefUsedEntry AsUsed(this PdfXrefEntry entry)
        {
            return entry.IsUsed() ? PdfXrefUsedEntry.FromEntry(entry) : null;
        }

        /// <summary>
        /// Checks if the entry is a compressed entry.
        /// </summary>
        public static bool IsCompressed(this PdfXrefEntry entry)
        {
            return entry.GetEntryType() == PdfXrefEntryType.Compressed;
        }

        /// <summary>
        /// Returns the entry as a compressed entry, or null if not a compressed entry.
        /// </summary>
        public static PdfXrefCompressedEntry AsCompressed(this PdfXrefEntry entry)
        {
            return entry.IsCompressed() ? PdfXrefCompressedEntry.FromEntry(entry) : null;
        }
    }
}
