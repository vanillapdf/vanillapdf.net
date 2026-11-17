using System;
using vanillapdf.net.Interop;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Configuration of the cryptographic toolkit, applied by <see cref="PdfCryptoUtils.Initialize"/>.
    /// </summary>
    public class PdfCryptoSettings : IDisposable
    {
        internal PdfCryptoSettingsSafeHandle Handle { get; }

        internal PdfCryptoSettings(PdfCryptoSettingsSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Whether the algorithms used only by old documents and key files (RC2, MD2, MD4) are enabled.
        /// Defaults to <see cref="PdfLegacyAlgorithmsPolicyType.Optional"/>.
        /// </summary>
        public PdfLegacyAlgorithmsPolicyType LegacyAlgorithmsPolicy
        {
            get => GetLegacyAlgorithmsPolicy();
            set => SetLegacyAlgorithmsPolicy(value);
        }

        /// <summary>
        /// Create new crypto settings with default values.
        /// </summary>
        /// <returns>A new <see cref="PdfCryptoSettings"/> instance.</returns>
        public static PdfCryptoSettings Create()
        {
            UInt32 result = NativeMethods.CryptoSettings_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfCryptoSettings(data);
        }

        private PdfLegacyAlgorithmsPolicyType GetLegacyAlgorithmsPolicy()
        {
            UInt32 result = NativeMethods.CryptoSettings_GetLegacyAlgorithmsPolicy(Handle, out PdfLegacyAlgorithmsPolicyType data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfLegacyAlgorithmsPolicyType>.CheckedCast(data);
        }

        private void SetLegacyAlgorithmsPolicy(PdfLegacyAlgorithmsPolicyType value)
        {
            UInt32 result = NativeMethods.CryptoSettings_SetLegacyAlgorithmsPolicy(Handle, (Int32)value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
