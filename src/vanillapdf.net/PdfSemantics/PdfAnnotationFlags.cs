using System;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Annotation flags controlling visibility and behavior.
    /// These flags may be combined using bitwise OR operations.
    /// </summary>
    [Flags]
    public enum PdfAnnotationFlags
    {
        /// <summary>No flags set</summary>
        None = 0,

        /// <summary>Do not display if not a standard type and no handler is available</summary>
        Invisible = 1,

        /// <summary>Do not display or print the annotation</summary>
        Hidden = 2,

        /// <summary>Print the annotation when the page is printed</summary>
        Print = 4,

        /// <summary>Do not scale the annotation to match page magnification</summary>
        NoZoom = 8,

        /// <summary>Do not rotate the annotation to match page rotation</summary>
        NoRotate = 16,

        /// <summary>Do not display the annotation on screen</summary>
        NoView = 32,

        /// <summary>Do not allow the annotation to interact with the user</summary>
        ReadOnly = 64,

        /// <summary>Do not allow the annotation to be deleted or modified</summary>
        Locked = 128,

        /// <summary>Invert the interpretation of the NoView flag for certain events</summary>
        ToggleNoView = 256,

        /// <summary>Do not allow the contents of the annotation to be modified</summary>
        LockedContents = 512,
    };
}
