namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The paper handling option that shall be used
    /// when printing the file from the print dialog.
    /// </summary>
    public enum PdfDuplex
    {
        /// <summary>
        /// Undefined uninitialized default value, triggers error when used.
        /// </summary>
        Undefined = 0,

        /// <summary>Print single-sided.</summary>
        Simplex = 0,

        /// <summary>Duplex and flip on the short edge of the sheet.</summary>
        DuplexFlipShortEdge,

        /// <summary>Duplex and flip on the long edge of the sheet.</summary>
        DuplexFlipLongEdge,
    };
}
