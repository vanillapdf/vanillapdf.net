namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Slots of an annotation appearance dictionary (AP entry).
    /// </summary>
    public enum PdfAppearanceType
    {
        /// <summary>
        /// Undefined uninitialized default value, triggers error when used.
        /// </summary>
        Undefined = 0,

        /// <summary>The annotation's normal appearance (N entry).</summary>
        Normal,

        /// <summary>
        /// The annotation's appearance while the cursor is held over its active area (R entry).
        /// </summary>
        Rollover,

        /// <summary>
        /// The annotation's appearance while the mouse button is pressed within its active area (D entry).
        /// </summary>
        Down,
    }
}
