namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Derived types of <see cref="PdfAction"/>
    /// </summary>
    public enum PdfActionType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// A go-to action changes the view to a specified destination within the same document.
        /// </summary>
        GoTo,

        /// <summary>
        /// A remote go-to action jumps to a destination in another PDF file.
        /// </summary>
        GoToRemote,

        /// <summary>
        /// A URI action causes a URI to be resolved.
        /// </summary>
        URI,

        /// <summary>
        /// A launch action launches an application or opens a document.
        /// </summary>
        Launch,

        /// <summary>
        /// A named action executes a predefined action.
        /// </summary>
        Named,

        /// <summary>
        /// A JavaScript action causes a script to be compiled and executed.
        /// </summary>
        JavaScript,
    }
}
