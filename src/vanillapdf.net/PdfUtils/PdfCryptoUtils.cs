using System;
using vanillapdf.net.Interop;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Initialization and cleanup of the cryptographic toolkit.
    /// </summary>
    /// <remarks>
    /// Initialization is optional: the first cryptographic operation initializes the toolkit
    /// with the default settings. Initialize explicitly, before any other call, to apply different settings.
    /// </remarks>
    public static class PdfCryptoUtils
    {
        /// <summary>
        /// Initialize the cryptographic toolkit with the given settings.
        /// </summary>
        /// <remarks>
        /// Once the toolkit is initialized, by an earlier call or by the first cryptographic operation,
        /// this call succeeds without changing anything. Use <see cref="GetLegacyAlgorithmsAvailable"/>
        /// to check the outcome, and call <see cref="Cleanup"/> first to apply different settings.
        /// Fails when the settings require legacy algorithms that cannot be enabled.
        /// </remarks>
        /// <param name="settings">The settings to apply.</param>
        public static void Initialize(PdfCryptoSettings settings)
        {
            UInt32 result = NativeMethods.CryptoUtils_Initialize(settings.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Check whether the legacy algorithms (RC2, MD2, MD4) can be used.
        /// </summary>
        /// <remarks>
        /// Initializes the toolkit with the default settings first, unless already initialized.
        /// </remarks>
        /// <returns>True when the legacy algorithms are available.</returns>
        public static bool GetLegacyAlgorithmsAvailable()
        {
            UInt32 result = NativeMethods.CryptoUtils_GetLegacyAlgorithmsAvailable(out bool data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Release what the initialization loaded.
        /// </summary>
        /// <remarks>
        /// Optional: everything is released at process exit. The toolkit can be initialized again afterwards,
        /// explicitly or by the next cryptographic operation. Do not call this while other threads
        /// perform cryptographic operations.
        /// </remarks>
        public static void Cleanup()
        {
            UInt32 result = NativeMethods.CryptoUtils_Cleanup();
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }
    }
}
