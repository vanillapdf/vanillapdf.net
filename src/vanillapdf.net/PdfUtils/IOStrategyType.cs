namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Specifies the I/O backend strategy used when opening or creating PDF files
    /// </summary>
    public enum IOStrategyType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Use in-memory I/O backend
        /// </summary>
        Memory,

        /// <summary>
        /// Use memory-mapped file I/O backend (not yet supported)
        /// </summary>
        MemoryMapped,

        /// <summary>
        /// Use file stream I/O backend
        /// </summary>
        FileStream
    };
}
