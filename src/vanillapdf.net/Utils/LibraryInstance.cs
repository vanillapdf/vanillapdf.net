using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    /// <summary>
    /// Class for managing native library diagnostics
    /// </summary>
    public static class LibraryInstance
    {
        /// <summary>
        /// Get number of active PdfSafeHandle objects
        /// </summary>
        /// <returns>Number of active PdfSafeHandle objects</returns>
        public static int GetSafeHandleCounter()
        {
            return PdfSafeHandle.Counter;
        }

        /// <summary>
        /// Get number of active PdfUnknown objects
        /// </summary>
        /// <returns>Number of active PdfUnknown objects</returns>
        public static int GetUnknownCounter()
        {
            return PdfUnknown.Counter;
        }
    }
}
