namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Derived types of \ref PdfStringObject
    /// </summary>
    public enum PdfStringType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// A literal string is preferable for printable data
        /// </summary>
        Literal,

        /// <summary>
        /// A hexadecimal string is preferable for arbitrary binary data
        /// </summary>
        Hexadecimal
    }
}
