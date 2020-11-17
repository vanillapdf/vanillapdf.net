using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Levels of detail in debug traces
    /// </summary>
    public enum PdfLoggingSeverity
    {
        Debug = 0,
        Info,
        Warning,
        Error,
        Fatal
    }

    /// <summary>
    /// Class supporting additional debug information in case of errors
    /// </summary>
    public static class PdfLogging
    {
        static PdfLogging()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        /// <summary>
        /// Current level of debug traces
        /// </summary>
        public static PdfLoggingSeverity Severity
        {
            get { return GetSeverity(); }
            set { SetSeverity(value); }
        }

        /// <summary>
        /// Check if logging is enabled
        /// </summary>
        /// <returns><c>true</c> if enabled, <c>false</c> otherwise</returns>
        public static bool IsEnabled()
        {
            UInt32 result = NativeMethods.Logging_IsEnabled(out bool enabled);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return enabled;
        }

        /// <summary>
        /// Enables
        /// </summary>
        public static void Enable()
        {
            UInt32 result = NativeMethods.Logging_Enable();
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public static void Disable()
        {
            UInt32 result = NativeMethods.Logging_Disable();
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private static PdfLoggingSeverity GetSeverity()
        {
            UInt32 result = NativeMethods.Logging_GetSeverity(out var severity);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfLoggingSeverity>.CheckedCast(severity);
        }

        private static void SetSeverity(PdfLoggingSeverity severity)
        {
            UInt32 result = NativeMethods.Logging_SetSeverity(severity);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private static class NativeMethods
        {
            public static LoggingIsEnabledDelgate Logging_IsEnabled = LibraryInstance.GetFunction<LoggingIsEnabledDelgate>("Logging_IsEnabled");
            public static LoggingEnableDelgate Logging_Enable = LibraryInstance.GetFunction<LoggingEnableDelgate>("Logging_Enable");
            public static LoggingDisableDelgate Logging_Disable = LibraryInstance.GetFunction<LoggingDisableDelgate>("Logging_Disable");
            public static LoggingGetSeverityDelgate Logging_GetSeverity = LibraryInstance.GetFunction<LoggingGetSeverityDelgate>("Logging_GetSeverity");
            public static LoggingSetSeverityDelgate Logging_SetSeverity = LibraryInstance.GetFunction<LoggingSetSeverityDelgate>("Logging_SetSeverity");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LoggingIsEnabledDelgate(out bool result);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LoggingEnableDelgate();

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LoggingDisableDelgate();

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LoggingGetSeverityDelgate(out PdfLoggingSeverity severity);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LoggingSetSeverityDelgate(PdfLoggingSeverity severity);
        }
    }
}
