namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Derived types of \ref PdfFont
    /// </summary>
    public enum PdfFontType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// A font composed of glyphs from a descendant CIDFont.
        /// </summary>
        Type0,

        /// <summary>
        /// A font that defines glyph shapes using Type 1 font technology.
        /// </summary>
        Type1,

        MMType1,

        /// <summary>
        /// A font that defines glyphs with streams of PDF graphics operators.
        /// </summary>
        Type3,

        TrueType,
        CIDFontType0,
        CIDFontType2
    };
}
