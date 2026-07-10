namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A name object specifying the page layout to be used when
    /// the document is opened.
    /// </summary>
    public enum PdfPageLayoutType
    {
        /// <summary>
        /// Undefined uninitialized default value, triggers error when used.
        /// </summary>
        Undefined = 0,

        /// <summary>Display one page at a time.</summary>
        SinglePage,

        /// <summary>Display the pages in one column.</summary>
        OneColumn,

        /// <summary>Display the pages in two columns, with odd-numbered pages on the left.</summary>
        TwoColumnLeft,

        /// <summary>Display the pages in two columns, with odd-numbered pages on the right.</summary>
        TwoColumnRight,

        /// <summary>(PDF 1.5) Display the pages two at a time, with odd-numbered pages on the left.</summary>
        TwoPageLeft,

        /// <summary>(PDF 1.5) Display the pages two at a time, with odd-numbered pages on the right.</summary>
        TwoPageRight,
    };
}
