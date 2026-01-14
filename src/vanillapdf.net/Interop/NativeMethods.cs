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
    }
}
