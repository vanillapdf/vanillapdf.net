namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Derived types of \ref PdfContentInstruction
    /// </summary>
    public enum PdfContentInstructionType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Atomic content operation.
        /// </summary>
        Operation,

        /// <summary>
        /// Composed non-atomic content object
        /// </summary>
        Object
    };
}
