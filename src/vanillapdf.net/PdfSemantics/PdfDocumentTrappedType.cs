namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Indicates whether the document has been modified to include trapping information.
    /// </summary>
    public enum PdfDocumentTrappedType
    {
        /// <summary>
        /// Undefined uninitialized default value, triggers error when used.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Either it is unknown whether the document has been trapped
        /// or it has been partly but not yet fully trapped.
        /// </summary>
        Unknown,

        /// <summary>
        /// The document has been fully trapped.
        /// </summary>
        True,

        /// <summary>
        /// The document has not yet been trapped.
        /// </summary>
        False,
    }
}
