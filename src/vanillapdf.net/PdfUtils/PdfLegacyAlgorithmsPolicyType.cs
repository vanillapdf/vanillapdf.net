namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Whether the algorithms used only by old documents and key files are enabled.
    /// </summary>
    /// <remarks>
    /// Legacy algorithms are RC2 (envelopes of certificate encrypted documents,
    /// PKCS#12 key files) and MD2/MD4 (signatures). RC4 and MD5 of the standard
    /// security handler are always available.
    /// </remarks>
    public enum PdfLegacyAlgorithmsPolicyType
    {
        /// <summary>
        /// Undefined uninitialized default value, triggers error when used.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Never enable the legacy algorithms.
        /// </summary>
        Disabled,

        /// <summary>
        /// Enable the legacy algorithms when available, otherwise continue without them. This is the default.
        /// </summary>
        Optional,

        /// <summary>
        /// Fail the initialization when the legacy algorithms cannot be enabled.
        /// </summary>
        Required,
    }
}
