namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Identifies the kind of a <see cref="PdfFileStructureIssue"/>.
    /// </summary>
    /// <remarks>
    /// The codes are grouped by the part of the file they concern:
    /// 1xx header, 2xx trailer, 3xx cross-reference entries, 4xx trailer references and 5xx cross-reference chain.
    /// </remarks>
    public enum PdfFileStructureIssueCodeType
    {
        /// <summary>
        /// Code not set.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// The file header is missing.
        /// </summary>
        HeaderMissing = 100,

        /// <summary>
        /// The header version is undefined.
        /// </summary>
        HeaderVersionUndefined = 101,

        /// <summary>
        /// The trailer is missing the /Size entry.
        /// </summary>
        TrailerMissingSize = 200,

        /// <summary>
        /// The trailer is missing the /Root entry.
        /// </summary>
        TrailerMissingRoot = 201,

        /// <summary>
        /// The trailer /Size does not match the cross-reference table.
        /// </summary>
        TrailerSizeMismatch = 202,

        /// <summary>
        /// The trailer dictionary is missing entirely.
        /// </summary>
        TrailerMissing = 203,

        /// <summary>
        /// A used entry has an invalid offset.
        /// </summary>
        XrefUsedEntryBadOffset = 300,

        /// <summary>
        /// A used entry offset exceeds the file size.
        /// </summary>
        XrefUsedEntryOffsetExceedsFileSize = 301,

        /// <summary>
        /// A compressed entry references a missing object stream.
        /// </summary>
        XrefCompressedEntryMissingObjectStream = 302,

        /// <summary>
        /// A free entry references an invalid next free object.
        /// </summary>
        XrefFreeEntryInvalidNextFree = 303,

        /// <summary>
        /// A compressed entry references an object stream that is itself compressed.
        /// </summary>
        XrefCompressedEntryObjectStreamCompressed = 304,

        /// <summary>
        /// The /Root target is not in the cross-reference table.
        /// </summary>
        TrailerRootReferenceNotInXref = 400,

        /// <summary>
        /// The /Info target is not in the cross-reference table.
        /// </summary>
        TrailerInfoReferenceNotInXref = 401,

        /// <summary>
        /// The cross-reference chain has no sections.
        /// </summary>
        XrefChainEmpty = 500
    };
}
