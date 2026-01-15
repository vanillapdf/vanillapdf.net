using System;
using System.Runtime.InteropServices;

namespace vanillapdf.net.Interop
{
    /// <summary>
    /// Centralized native methods for P/Invoke to the vanillapdf native library.
    /// </summary>
    internal static partial class NativeMethods
    {
        /// <summary>
        /// Name of the native library.
        /// </summary>
        public const string LibraryName = "vanillapdf";

        /// <summary>
        /// Calling convention used by all native methods.
        /// </summary>
        public const CallingConvention LibraryCallingConvention = CallingConvention.Cdecl;

        #region FileWriterObserver Delegates

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 OnInitializingDelegate(IntPtr userdata, IntPtr data);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 OnFinalizingDelegate(IntPtr userdata, IntPtr data);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 OnBeforeObjectWriteDelegate(IntPtr userdata, IntPtr data);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 OnAfterObjectWriteDelegate(IntPtr userdata, IntPtr data);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 OnBeforeObjectOffsetRecalculationDelegate(IntPtr userdata, IntPtr data);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 OnAfterObjectOffsetRecalculationDelegate(IntPtr userdata, IntPtr data);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 OnBeforeEntryOffsetRecalculationDelegate(IntPtr userdata, IntPtr data);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 OnAfterEntryOffsetRecalculationDelegate(IntPtr userdata, IntPtr data);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 OnBeforeOutputFlushDelegate(IntPtr userdata, IntPtr data);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 OnAfterOutputFlushDelegate(IntPtr userdata, IntPtr data);

        #endregion

        #region Logging Delegates

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate void SinkLogDelegate(IntPtr userdata, PdfUtils.PdfLoggingSeverity severity, IntPtr payload, UIntPtr length);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate void SinkFlushDelegate(IntPtr userdata);

        #endregion

        #region SigningKey Delegates

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 InitializeDelegate(IntPtr userdata, PdfUtils.PdfMessageDigestAlgorithmType digest);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 UpdateDelegate(IntPtr userdata, IntPtr buffer);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 FinalDelegate(IntPtr userdata, out IntPtr buffer);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        public delegate UInt32 CleanupDelegate(IntPtr userdata);

        #endregion
    }
}
