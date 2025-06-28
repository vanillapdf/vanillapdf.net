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

        /// <summary>Multiple master Type&nbsp;1 font.</summary>
        MMType1,

        /// <summary>
        /// A font that defines glyphs with streams of PDF graphics operators.
        /// </summary>
        /// <summary>A font that defines glyphs with streams of PDF graphics operators.</summary>
        Type3,

        /// <summary>A TrueType font.</summary>
        TrueType,

        /// <summary>CIDFont Type&nbsp;0 font.</summary>
        CIDFontType0,

        /// <summary>CIDFont Type&nbsp;2 font.</summary>
        CIDFontType2
    };
}
