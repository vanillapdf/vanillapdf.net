using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    /// <summary>
    /// Controls how accessors auto-upgrade returned objects to derived types.
    /// </summary>
    public enum UpgradePolicy
    {
        /// <summary>
        /// No auto-upgrade. Accessors return the base type as-is.
        /// Callers use Is&lt;T&gt;/As&lt;T&gt; to upgrade on demand.
        /// </summary>
        None,

        /// <summary>
        /// Single-level upgrade. Accessors return one level of derived type.
        /// E.g. Object -> StringObject, Instruction -> Operation.
        /// </summary>
        Single,

        /// <summary>
        /// Full upgrade to the most-derived (leaf) type.
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
        private static volatile UpgradePolicy _upgradePolicy = UpgradePolicy.Full;

        /// <summary>
        /// Gets or sets the upgrade policy for accessor methods.
        /// Default is <see cref="UpgradePolicy.Full"/>.
        /// </summary>
        public static UpgradePolicy UpgradePolicy
        {
            get => _upgradePolicy;
            set => _upgradePolicy = value;
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
        /// Get number of active PdfUnknown objects
        /// </summary>
        /// <returns>Number of active PdfUnknown objects</returns>
        public static int GetUnknownCounter()
        {
            return PdfUnknown.Counter;
        }
    }
}
