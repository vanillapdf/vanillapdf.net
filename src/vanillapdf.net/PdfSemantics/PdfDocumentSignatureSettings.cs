using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Class for specifying document signature details
    /// </summary>
    public class PdfDocumentSignatureSettings : PdfUnknown
    {
        internal PdfDocumentSignatureSettingsSafeHandle Handle { get; }

        internal PdfDocumentSignatureSettings(PdfDocumentSignatureSettingsSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfDocumentSignatureSettings()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDocumentSignatureSettingsSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Message digest algorithm for calculation hash of the data to be signed
        /// </summary>
        public PdfMessageDigestAlgorithmType Digest
        {
            get { return GetDigest(); }
            set { SetDigest(value); }
        }

        /// <summary>
        /// Handle to \ref PdfSigningKey to perform the signature
        /// </summary>
        public PdfSigningKey SigningKey
        {
            get { return GetSigningKey(); }
            set { SetSigningKey(value); }
        }

        /// <summary>
        /// Set the name of the person or authority signing the document.
        /// </summary>
        public PdfLiteralStringObject Name
        {
            get { return GetName(); }
            set { SetName(value); }
        }

        /// <summary>
        /// Get the CPU host name or physical location of the signing.
        /// </summary>
        public PdfLiteralStringObject Location
        {
            get { return GetLocation(); }
            set { SetLocation(value); }
        }

        /// <summary>
        /// Set the reason for the signing, such as (I agree...).
        /// </summary>
        public PdfLiteralStringObject Reason
        {
            get { return GetReason(); }
            set { SetReason(value); }
        }

        /// <summary>
        /// The time of signing.
        /// 
        /// Depending on the signature handler, this may be a normal unverified computer time or
        /// a time generated in a verifiable way from a secure time server.
        /// This value should be used only when the time of signing is not available in the signature.
        /// </summary>
        public PdfDate SigningTime
        {
            get { return GetSigningTime(); }
            set { SetSigningTime(value); }
        }

        /// <summary>
        /// An array of byte strings that shall represent the X.509 certificate chain
        /// used when signing and verifying signatures that use public-key cryptography,
        /// or a byte string if the chain has only one entry.
        /// </summary>
        public PdfHexadecimalStringObject Certificate
        {
            get { return GetCertificate(); }
            set { SetCertificate(value); }
        }

        /// <summary>
        /// Creates a new blank instance of \ref PdfDocumentSignatureSettings with default values
        /// </summary>
        /// <returns>A new \ref PdfDocumentSignatureSettings instance on success, throws exception on failure</returns>
        public static PdfDocumentSignatureSettings Create()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDocumentSignatureSettings(data);
        }

        private PdfMessageDigestAlgorithmType GetDigest()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetDigest(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfMessageDigestAlgorithmType>.CheckedCast(data);
        }

        private void SetDigest(PdfMessageDigestAlgorithmType data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetDigest(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfSigningKey GetSigningKey()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetSigningKey(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfSigningKey(data);
        }

        private void SetSigningKey(PdfSigningKey data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetSigningKey(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfLiteralStringObject GetName()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetName(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        private void SetName(PdfLiteralStringObject data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetName(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfLiteralStringObject GetLocation()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetLocation(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        private void SetLocation(PdfLiteralStringObject data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetLocation(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfLiteralStringObject GetReason()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetReason(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        private void SetReason(PdfLiteralStringObject data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetReason(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfDate GetSigningTime()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetSigningTime(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDate(data);
        }

        private void SetSigningTime(PdfDate data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetSigningTime(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfHexadecimalStringObject GetCertificate()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetCertificate(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        private void SetCertificate(PdfHexadecimalStringObject data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetCertificate(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static CreateDelgate DocumentSignatureSettings_Create = LibraryInstance.GetFunction<CreateDelgate>("DocumentSignatureSettings_Create");

            public static GetDigestDelgate DocumentSignatureSettings_GetDigest = LibraryInstance.GetFunction<GetDigestDelgate>("DocumentSignatureSettings_GetDigest");
            public static SetDigestDelgate DocumentSignatureSettings_SetDigest = LibraryInstance.GetFunction<SetDigestDelgate>("DocumentSignatureSettings_SetDigest");

            public static GetSigningKeyDelgate DocumentSignatureSettings_GetSigningKey = LibraryInstance.GetFunction<GetSigningKeyDelgate>("DocumentSignatureSettings_GetSigningKey");
            public static SetSigningKeyDelgate DocumentSignatureSettings_SetSigningKey = LibraryInstance.GetFunction<SetSigningKeyDelgate>("DocumentSignatureSettings_SetSigningKey");

            public static GetNameDelgate DocumentSignatureSettings_GetName = LibraryInstance.GetFunction<GetNameDelgate>("DocumentSignatureSettings_GetName");
            public static SetNameDelgate DocumentSignatureSettings_SetName = LibraryInstance.GetFunction<SetNameDelgate>("DocumentSignatureSettings_SetName");

            public static GetLocationDelgate DocumentSignatureSettings_GetLocation = LibraryInstance.GetFunction<GetLocationDelgate>("DocumentSignatureSettings_GetLocation");
            public static SetLocationDelgate DocumentSignatureSettings_SetLocation = LibraryInstance.GetFunction<SetLocationDelgate>("DocumentSignatureSettings_SetLocation");

            public static GetReasonDelgate DocumentSignatureSettings_GetReason = LibraryInstance.GetFunction<GetReasonDelgate>("DocumentSignatureSettings_GetReason");
            public static SetReasonDelgate DocumentSignatureSettings_SetReason = LibraryInstance.GetFunction<SetReasonDelgate>("DocumentSignatureSettings_SetReason");

            public static GetSigningTimeDelgate DocumentSignatureSettings_GetSigningTime = LibraryInstance.GetFunction<GetSigningTimeDelgate>("DocumentSignatureSettings_GetSigningTime");
            public static SetSigningTimeDelgate DocumentSignatureSettings_SetSigningTime = LibraryInstance.GetFunction<SetSigningTimeDelgate>("DocumentSignatureSettings_SetSigningTime");

            public static GetCertificateDelgate DocumentSignatureSettings_GetCertificate = LibraryInstance.GetFunction<GetCertificateDelgate>("DocumentSignatureSettings_GetCertificate");
            public static SetCertificateDelgate DocumentSignatureSettings_SetCertificate = LibraryInstance.GetFunction<SetCertificateDelgate>("DocumentSignatureSettings_SetCertificate");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfDocumentSignatureSettingsSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetDigestDelgate(PdfDocumentSignatureSettingsSafeHandle handle, out PdfMessageDigestAlgorithmType data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetDigestDelgate(PdfDocumentSignatureSettingsSafeHandle handle, PdfMessageDigestAlgorithmType data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetSigningKeyDelgate(PdfDocumentSignatureSettingsSafeHandle handle, out PdfSigningKeySafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetSigningKeyDelgate(PdfDocumentSignatureSettingsSafeHandle handle, PdfSigningKeySafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetNameDelgate(PdfDocumentSignatureSettingsSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetNameDelgate(PdfDocumentSignatureSettingsSafeHandle handle, PdfLiteralStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetLocationDelgate(PdfDocumentSignatureSettingsSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetLocationDelgate(PdfDocumentSignatureSettingsSafeHandle handle, PdfLiteralStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetReasonDelgate(PdfDocumentSignatureSettingsSafeHandle handle, out PdfLiteralStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetReasonDelgate(PdfDocumentSignatureSettingsSafeHandle handle, PdfLiteralStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetSigningTimeDelgate(PdfDocumentSignatureSettingsSafeHandle handle, out PdfDateSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetSigningTimeDelgate(PdfDocumentSignatureSettingsSafeHandle handle, PdfDateSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetCertificateDelgate(PdfDocumentSignatureSettingsSafeHandle handle, out PdfHexadecimalStringObjectSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetCertificateDelgate(PdfDocumentSignatureSettingsSafeHandle handle, PdfHexadecimalStringObjectSafeHandle data);
        }
    }
}
