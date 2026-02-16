using System;

namespace vanillapdf.net.Utils
{
    /// <summary>
    /// Controls how accessors auto-upgrade returned objects to derived types.
    /// </summary>
    public enum UpgradePolicy
    {
        /// <summary>
        /// No resolution, no upgrade. Accessors return the raw handle as-is.
        /// Fast but may return indirect references that are hard to work with.
        /// </summary>
        None,

        /// <summary>
        /// Resolve indirect references only, return base type.
        /// E.g. IndirectReference -> PdfObject (resolved but not upgraded).
        /// Callers use Is&lt;T&gt;/As&lt;T&gt; to upgrade on demand.
        /// </summary>
        ResolveOnly,

        /// <summary>
        /// Resolve + single-level upgrade. Accessors return one level of derived type.
        /// E.g. Object -> StringObject, Instruction -> Operation.
        /// </summary>
        Single,

        /// <summary>
        /// Resolve + full upgrade to the most-derived (leaf) type.
        /// E.g. Object -> StringObject -> LiteralStringObject,
        /// Instruction -> Operation -> TextShow.
        /// </summary>
        Full
    }

    /// <summary>
    /// Class for managing native library diagnostics
    /// </summary>
    public static class LibraryInstance
    {
        // TODO: Review default upgrade policy for 3.0 — consider ResolveOnly as the default
        // to encourage explicit Is<T>/As<T> usage and reduce unnecessary allocations.
        private static volatile UpgradePolicy _upgradePolicy = UpgradePolicy.Single;

        /// <summary>
        /// Gets or sets the upgrade policy for accessor methods.
        /// Default is <see cref="UpgradePolicy.Single"/>.
        /// </summary>
        public static UpgradePolicy UpgradePolicy
        {
            get => _upgradePolicy;
            set => _upgradePolicy = value;
        }

        /// <summary>
        /// Determine if the native library is already loaded in the memory.
        /// No longer required — the runtime loads the native library automatically via DllImport.
        /// </summary>
        /// <returns>Always returns true</returns>
        [Obsolete("Native library loading is now handled automatically. This method will be removed in a future version.")]
        public static bool IsInitialized()
        {
            return true;
        }

        /// <summary>
        /// Loads native library from the current assembly path.
        /// No longer required — the runtime loads the native library automatically via DllImport.
        /// </summary>
        [Obsolete("Native library loading is now handled automatically. This method will be removed in a future version.")]
        public static void Initialize()
        {
        }

        /// <summary>
        /// Loads native library from the specified folder.
        /// No longer required — the runtime loads the native library automatically via DllImport.
        /// </summary>
        /// <param name="rootPath">Folder to search for native library</param>
        [Obsolete("Native library loading is now handled automatically. This method will be removed in a future version.")]
        public static void Initialize(string rootPath)
        {
        }

        /// <summary>
        /// Release native memory handle.
        /// No longer required — the runtime manages the native library lifetime.
        /// </summary>
        [Obsolete("Native library lifetime is now managed automatically. This method will be removed in a future version.")]
        public static void Release()
        {
        }

        /// <summary>
        /// Find procedure in the native library by its name.
        /// No longer required — use DllImport/LibraryImport directly.
        /// </summary>
        /// <typeparam name="T">Function delegate type</typeparam>
        /// <param name="procName">Name of the symbol exported by the native library</param>
        /// <returns>Delegate to specified function</returns>
        [Obsolete("Use DllImport/LibraryImport instead. This method will be removed in a future version.")]
        public static T GetFunction<T>(string procName)
        {
            throw new NotSupportedException(
                "Manual native library loading has been replaced by DllImport/LibraryImport.");
        }

        /// <summary>
        /// Finds the constant symbol exported by the native library.
        /// No longer required — use DllImport/LibraryImport directly.
        /// </summary>
        /// <param name="constantName">Name of the symbol exported by the native library</param>
        /// <returns>Numeric value of the constant</returns>
        [Obsolete("Use DllImport/LibraryImport instead. This method will be removed in a future version.")]
        public static UInt32 GetConstant(string constantName)
        {
            throw new NotSupportedException(
                "Manual native library loading has been replaced by DllImport/LibraryImport.");
        }

        /// <summary>
        /// Get number of active PdfSafeHandle objects
        /// </summary>
        /// <returns>Number of active PdfSafeHandle objects</returns>
        public static int GetSafeHandleCounter()
        {
            return PdfSafeHandle.Counter;
        }

        /// <summary>
        /// Get number of active PdfUnknown objects.
        /// Backed by native ObjectDiagnostics for backward compatibility.
        /// </summary>
        /// <returns>Number of active PdfUnknown objects</returns>
        [Obsolete("Use ObjectDiagnostics.GetActiveObjectCount() instead. This method will be removed in a future version.")]
        public static int GetUnknownCounter()
        {
            return (int)ObjectDiagnostics.GetActiveObjectCount();
        }
    }
}
