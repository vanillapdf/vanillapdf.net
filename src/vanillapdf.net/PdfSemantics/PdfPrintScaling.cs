namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The page scaling option that shall be selected when
    /// a print dialog is displayed for this document.
    /// </summary>
    public enum PdfPrintScaling
    {
        /// <summary>
        /// Undefined uninitialized default value, triggers error when used.
        /// </summary>
        Undefined = 0,

        /// <summary>Indicates the conforming reader's default print scaling.</summary>
        AppDefault,

        /// <summary>No page scaling.</summary>
        None,
    };
}
