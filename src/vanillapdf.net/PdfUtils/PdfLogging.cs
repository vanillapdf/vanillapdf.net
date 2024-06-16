﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
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
        /// Set logging pattern to be used for each logging entry
        /// </summary>
        public static string Pattern
        {
            set { SetPattern(value); }
        }

        /// <summary>
        /// Set a logger, that will invoke custom callback functions specified by context
        /// </summary>
        /// <param name="context">Context object holding the references to callback methods</param>
        public static void SetCallbackLogger(PdfCallbackLoggerContext context)
        {
            UInt32 result = NativeMethods.Logging_SetCallbackLogger(onSinkLogDelegate, onSinkFlushDelegate, GCHandle.ToIntPtr(context.Handle));
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Set a logger using rotating file sink based on size
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="max_file_size"></param>
        /// <param name="max_files"></param>
        public static void SetRotatingFileLogger(string filename, int max_file_size, int max_files)
        {
            UInt32 result = NativeMethods.Logging_SetRotatingFileLogger(filename, max_file_size, max_files);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Gracefully terminate the entire logging ecosystem
        /// </summary>
        public static void Shutdown()
        {
            UInt32 result = NativeMethods.Logging_Shutdown();
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private static void SetPattern(string pattern)
        {
            UInt32 result = NativeMethods.Logging_SetPattern(pattern);
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

        private static void OnSinkLog(IntPtr userdata, PdfLoggingSeverity severity, IntPtr payload, UIntPtr length)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfCallbackLoggerContext context = (handle.Target as PdfCallbackLoggerContext);

                var message = Marshal.PtrToStringAnsi(payload, (int)length);
                context.SinkLog(severity, message);
            }
            catch {
            }
        }

        private static void OnSinkFlush(IntPtr userdata)
        {
            try {
                GCHandle handle = GCHandle.FromIntPtr(userdata);
                PdfCallbackLoggerContext context = (handle.Target as PdfCallbackLoggerContext);

                context.SinkFlush();
            }
            catch {
            }
        }

        // We need to have static delegates as the FileWriterObserver_CreateCustom
        // would create a delegate when referencing the static function
        // which would be disposed during the garbage collection.
        // We prevent cleaning up the delegates by having static references.

        private static NativeMethods.SinkLogDelegate onSinkLogDelegate = OnSinkLog;
        private static NativeMethods.SinkFlushDelegate onSinkFlushDelegate = OnSinkFlush;

        private static class NativeMethods
        {
            public static SetCallbackLoggerDelegate Logging_SetCallbackLogger = LibraryInstance.GetFunction<SetCallbackLoggerDelegate>("Logging_SetCallbackLogger");
            public static SetRotatingFileLoggerDelegate Logging_SetRotatingFileLogger = LibraryInstance.GetFunction<SetRotatingFileLoggerDelegate>("Logging_SetRotatingFileLogger");
            public static LoggingGetSeverityDelgate Logging_GetSeverity = LibraryInstance.GetFunction<LoggingGetSeverityDelgate>("Logging_GetSeverity");
            public static LoggingSetSeverityDelgate Logging_SetSeverity = LibraryInstance.GetFunction<LoggingSetSeverityDelgate>("Logging_SetSeverity");
            public static LoggingSetPatternDelgate Logging_SetPattern = LibraryInstance.GetFunction<LoggingSetPatternDelgate>("Logging_SetPattern");
            public static LoggingShutdownDelgate Logging_Shutdown = LibraryInstance.GetFunction<LoggingShutdownDelgate>("Logging_Shutdown");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate void SinkLogDelegate(IntPtr userdata, PdfLoggingSeverity severity, IntPtr payload, UIntPtr length);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate void SinkFlushDelegate(IntPtr userdata);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetCallbackLoggerDelegate(SinkLogDelegate sinkLogDelegate, SinkFlushDelegate sinkFlushDelegate, IntPtr userdata);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetRotatingFileLoggerDelegate(string filename, int max_file_size, int max_files);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LoggingGetSeverityDelgate(out PdfLoggingSeverity severity);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LoggingSetSeverityDelgate(PdfLoggingSeverity severity);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LoggingSetPatternDelgate(string pattern);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LoggingShutdownDelgate();
        }
    }
}
