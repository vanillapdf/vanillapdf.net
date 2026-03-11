namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The type of a PDF interactive form field.
    /// </summary>
    public enum PdfFieldType
    {
        /// <summary>
        /// The field type is unknown or not set.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// A non-terminal field that groups other fields.
        /// </summary>
        NonTerminal = 1,

        /// <summary>
        /// A button field (check box, radio button, or push button).
        /// </summary>
        Button = 2,

        /// <summary>
        /// A text field.
        /// </summary>
        Text = 3,

        /// <summary>
        /// A choice field (list box or combo box).
        /// </summary>
        Choice = 4,

        /// <summary>
        /// A signature field.
        /// </summary>
        Signature = 5,
    }
}
