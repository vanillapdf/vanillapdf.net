namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The predominant reading order for text.
    /// </summary>
    public enum PdfReadingOrder
    {
        /// <summary>
        /// Undefined uninitialized default value, triggers error when used.
        /// </summary>
        Undefined = 0,

        /// <summary>Left to right.</summary>
        LeftToRight,

        /// <summary>
        /// Right to left (including vertical writing systems,
        /// such as Chinese, Japanese, and Korean).
        /// </summary>
        RightToLeft,
    };
}
