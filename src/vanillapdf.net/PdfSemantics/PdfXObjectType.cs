namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Types of external objects (XObjects).
    /// </summary>
    public enum PdfXObjectType
    {
        /// <summary>
        /// Undefined uninitialized default value, triggers error when used.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// A form XObject, a self-contained description of a sequence of graphics objects.
        /// </summary>
        Form,

        /// <summary>
        /// An image XObject, representing a sampled image.
        /// </summary>
        Image,

        /// <summary>
        /// A PostScript XObject (deprecated in PDF 2.0).
        /// </summary>
        PostScript,
    }
}
