using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A set of flags specifying various document-level characteristics related to signature fields.
    /// </summary>
    public class PdfSignatureFlags : IDisposable
    {
        internal PdfSignatureFlagsSafeHandle Handle { get; }

        internal PdfSignatureFlags(PdfSignatureFlagsSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// If true, the document contains at least one signature field.
        /// </summary>
        public bool SignaturesExist
        {
            get => GetSignaturesExist();
            set => SetSignaturesExist(value);
        }

        /// <summary>
        /// If true, the document contains signatures that may be invalidated when the file
        /// is saved in a way that alters its previous contents, as opposed to an incremental update.
        /// </summary>
        public bool AppendOnly
        {
            get => GetAppendOnly();
            set => SetAppendOnly(value);
        }

        /// <summary>
        /// Create a set of signature flags with every flag cleared.
        /// </summary>
        /// <returns>A new <see cref="PdfSignatureFlags"/> instance.</returns>
        public static PdfSignatureFlags Create()
        {
            UInt32 result = NativeMethods.SignatureFlags_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfSignatureFlags(data);
        }

        private bool GetSignaturesExist()
        {
            UInt32 result = NativeMethods.SignatureFlags_GetSignaturesExist(Handle, out bool data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetSignaturesExist(bool value)
        {
            UInt32 result = NativeMethods.SignatureFlags_SetSignaturesExist(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private bool GetAppendOnly()
        {
            UInt32 result = NativeMethods.SignatureFlags_GetAppendOnly(Handle, out bool data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetAppendOnly(bool value)
        {
            UInt32 result = NativeMethods.SignatureFlags_SetAppendOnly(Handle, value);
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
