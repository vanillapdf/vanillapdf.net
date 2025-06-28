namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Levels of detail in debug traces
    /// </summary>
    public enum PdfLoggingSeverity
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>Trace level logs.</summary>
        Trace,

        /// <summary>Debug level logs.</summary>
        Debug,

        /// <summary>Informational logs.</summary>
        Info,

        /// <summary>Warning logs.</summary>
        Warning,

        /// <summary>Error logs.</summary>
        Error,

        /// <summary>Critical error logs.</summary>
        Critical,

        /// <summary>Disable logging.</summary>
        Off
    };
}
