namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Derived types of \ref PdfXrefEntry
    /// </summary>
    public enum PdfXrefEntryType
    {
        Null = 0,

        /// <summary>
        /// Represents \ref PdfXrefFreeEntry.
        /// </summary>
        Free,

        /// <summary>
        /// Represents \ref PdfXrefUsedEntry.
        /// </summary>
        Used,

        /// <summary>
        /// Represents \ref PdfXrefCompressedEntry.
        /// </summary>
        Compressed
    };
}
