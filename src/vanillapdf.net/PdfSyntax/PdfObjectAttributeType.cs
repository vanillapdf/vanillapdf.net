namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Derived types of \ref PdfBaseObjectAttribute
    /// </summary>
    public enum PdfObjectAttributeType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        Empty,
        SerializationOverride,
        TrackingIdentifier,
        ImageMetadata
    };
}
