using System;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Flags specifying various characteristics of a PDF interactive form field.
    /// </summary>
    [Flags]
    public enum PdfFieldFlags
    {
        /// <summary>No flags set.</summary>
        None = 0,

        /// <summary>The user may not change the value of the field.</summary>
        ReadOnly = 1 << 0,

        /// <summary>The field shall have a value at the time it is exported by a submit-form action.</summary>
        Required = 1 << 1,

        /// <summary>The field shall not be exported by a submit-form action.</summary>
        NoExport = 1 << 2,

        // Button field flags

        /// <summary>The field is a set of radio buttons rather than a check box.</summary>
        Radio = 1 << 15,

        /// <summary>The field is a push button with no permanent value.</summary>
        PushButton = 1 << 16,

        // Text field flags

        /// <summary>The field may contain multiple lines of text.</summary>
        Multiline = 1 << 12,

        /// <summary>The field is intended for entering a secure password.</summary>
        Password = 1 << 13,

        // Choice field flags

        /// <summary>The field is a combo box rather than a list box.</summary>
        Combo = 1 << 17,

        /// <summary>The combo box includes an editable text box.</summary>
        Edit = 1 << 18,

        /// <summary>The field's option items shall be sorted alphabetically.</summary>
        Sort = 1 << 19,
    }
}
