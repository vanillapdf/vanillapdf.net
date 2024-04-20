namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Derived types of \ref PdfImageMetadataObjectAttribute ColorSpace property
    /// </summary>
    public enum PdfImageColorSpaceType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        GRAY,
        RGB,
        CMYK,
    };
}
