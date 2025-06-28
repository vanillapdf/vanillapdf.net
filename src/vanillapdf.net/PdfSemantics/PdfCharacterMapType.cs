namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Derived types of \ref PdfCharacterMap
    /// </summary>
    public enum PdfCharacterMapType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>Embedded character map.</summary>
        Embedded,

        /// <summary>Unicode character map.</summary>
        Unicode
    };
}
