namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Severity of a <see cref="PdfFileStructureIssue"/>.
    /// </summary>
    public enum PdfFileStructureIssueSeverityType
    {
        /// <summary>
        /// Severity not set.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Informational observation.
        /// </summary>
        Info,

        /// <summary>
        /// Non-critical issue.
        /// </summary>
        Warning,

        /// <summary>
        /// Critical structural error, the file is not structurally valid.
        /// </summary>
        Error
    };
}
