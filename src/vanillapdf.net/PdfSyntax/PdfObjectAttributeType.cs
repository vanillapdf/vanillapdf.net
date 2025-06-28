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

        /// <summary>Attribute without specific semantics.</summary>
        Empty,

        /// <summary>Attribute used to override serialization.</summary>
        SerializationOverride,

        /// <summary>Identifier used for tracking purposes.</summary>
        TrackingIdentifier,

        /// <summary>Attribute holding image metadata.</summary>
        ImageMetadata
    };
}
