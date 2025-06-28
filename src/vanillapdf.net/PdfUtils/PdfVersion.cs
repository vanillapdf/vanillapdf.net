namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// PDF document version to which document conforms
    /// </summary>
    public enum PdfVersion
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>PDF 1.0.</summary>
        PdfVersion_10,

        /// <summary>PDF 1.1.</summary>
        PdfVersion_11,

        /// <summary>PDF 1.2.</summary>
        PdfVersion_12,

        /// <summary>PDF 1.3.</summary>
        PdfVersion_13,

        /// <summary>PDF 1.4.</summary>
        PdfVersion_14,

        /// <summary>PDF 1.5.</summary>
        PdfVersion_15,

        /// <summary>PDF 1.6.</summary>
        PdfVersion_16,

        /// <summary>PDF 1.7.</summary>
        PdfVersion_17,

        /// <summary>PDF 2.0.</summary>
        PdfVersion_20
    }
}
