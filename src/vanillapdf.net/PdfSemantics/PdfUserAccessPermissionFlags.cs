using System;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Flags for indicating user permissions within encrypted document
    /// </summary>
    [Flags]
    public enum PdfUserAccessPermissionFlags
    {
        /// <summary>
        /// No actions are allowed for the user.
        /// </summary>
        None = 0,

        /// <summary>
        /// (Security handlers of revision 2)
        /// Print the document.
        /// (Security handlers of revision 3 or greater)
        /// Print the document (possibly not at the highest quality level,
        /// depending on whether bit 12 is also set).
        /// </summary>
        PrintDegraded = 4,

        /// <summary>
        /// Modify the contents of the document by operations other than those controlled by bits 6, 9, and 11.
        /// </summary>
        ModifyContents = 8,

        /// <summary>
        /// (Security handlers of revision 2)
        /// Copy or otherwise extract text and graphics from the document,
        /// including extracting text and graphics (in support of accessibility to users with disabilities or for other purposes).
        /// (Security handlers of revision 3 or greater)
        /// Copy or otherwise extract text and graphics from the document by operations other than that controlled by bit 10.
        /// </summary>
        CopyAndExtract = 16,

        /// <summary>
        /// Add or modify text annotations, fill in interactive form fields, and, if bit 4 is also set,
        /// create or modify interactive form fields (including signature fields).
        /// </summary>
        AddAnnotations = 32,

        /// <summary>
        /// (Security handlers of revision 3 or greater)
        /// Fill in existing interactive form fields (including signature fields), even if bit 6 is clear.
        /// </summary>
        FillForms = 256,

        /// <summary>
        /// (Security handlers of revision 3 or greater)
        /// Extract text and graphics (in support of accessibility to users with disabilities or for other purposes).
        /// </summary>
        ExtractText = 512,

        /// <summary>
        /// (Security handlers of revision 3 or greater)
        /// Assemble the document (insert, rotate, or delete pages and create bookmarks or thumbnail images), even if bit 4 is clear.
        /// </summary>
        AssembleDocument = 1024,

        /// <summary>
        /// (Security handlers of revision 3 or greater)
        /// Print the document to a representation from which a faithful digital copy of the PDF content could be generated.
        /// When this bit is clear (and bit 3 is set), printing is limited to a low-level representation of the appearance,
        /// possibly of degraded quality.
        /// </summary>
        PrintFaithful = 2048
    }
}
