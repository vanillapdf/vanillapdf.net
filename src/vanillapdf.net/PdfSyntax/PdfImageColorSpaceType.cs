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

        /// <summary>Device gray color space.</summary>
        GRAY,

        /// <summary>Device RGB color space.</summary>
        RGB,

        /// <summary>Device CMYK color space.</summary>
        CMYK,
    };
}
