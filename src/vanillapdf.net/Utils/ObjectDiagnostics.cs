using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    /// <summary>
    /// Provides runtime introspection of native object lifecycle.
    /// All native objects derive from IUnknown and are reference-counted;
    /// this class exposes thread-safe counters for tracking live objects.
    /// </summary>
    public static class ObjectDiagnostics
    {
        /// <summary>
        /// Get number of currently live native IUnknown-derived objects
        /// </summary>
        /// <returns>Current count of live native objects</returns>
        public static Int64 GetActiveObjectCount()
        {
            UInt32 result = NativeMethods.ObjectDiagnostics_GetActiveObjectCount(out Int64 count);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return count;
        }

        /// <summary>
        /// Get peak count of simultaneously live native IUnknown-derived objects since startup
        /// </summary>
        /// <returns>Peak count of native objects</returns>
        public static Int64 GetPeakObjectCount()
        {
            UInt32 result = NativeMethods.ObjectDiagnostics_GetPeakObjectCount(out Int64 count);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return count;
        }
    }
}
