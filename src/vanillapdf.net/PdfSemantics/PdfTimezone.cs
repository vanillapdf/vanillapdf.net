namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Timezone for representing UTC offset
    /// </summary>
    public enum PdfTimezoneType
    {
        /// <summary>
        /// Undefined unitialized default value
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Signifies that local time is equal to UT.
        /// </summary>
        UTC,

        /// <summary>
        /// Signifies that local time is later than UT.
        /// </summary>
        Later,

        /// <summary>
        /// Signifies that local time is earlier than UT.
        /// </summary>
        Earlier
    }
}
