namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The document's page mode, specifying how to display
    /// the document on exiting full-screen mode.
    /// </summary>
    public enum PdfNonFullScreenPageMode
    {
        /// <summary>
        /// Undefined uninitialized default value, triggers error when used.
        /// </summary>
        Undefined = 0,

        /// <summary>Neither document outline nor thumbnail images visible.</summary>
        UseNone,

        /// <summary>Document outline visible.</summary>
        UseOutlines,

        /// <summary>Thumbnail images visible.</summary>
        UseThumbs,

        /// <summary>Optional content group panel visible.</summary>
        UseOC,
    };
}
