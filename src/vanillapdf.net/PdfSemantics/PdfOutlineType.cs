namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Derived types of \ref PdfOutlineBase
    /// </summary>
    public enum PdfOutlineType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>Root outline item.</summary>
        Outline,

        /// <summary>Child outline item.</summary>
        Item,
    };
}
