namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Derived types of \ref PdfContentObject
    /// </summary>
    public enum PdfContentObjectType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// A PDF text object consists of operators that may show text strings, move the text position, and set text state and certain other parameters.
        /// </summary>
        Text,

        /// <summary>
        /// As an alternative to the image XObjects a sampled image may be specified in the form of an inline image.
        /// </summary>
        InlineImage
    };
}
