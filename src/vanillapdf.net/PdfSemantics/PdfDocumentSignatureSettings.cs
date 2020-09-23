using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    public class PdfDocumentSignatureSettings : PdfUnknown
    {
        internal PdfDocumentSignatureSettings(PdfDocumentSignatureSettingsSafeHandle handle) : base(handle)
        {
        }

        static PdfDocumentSignatureSettings()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfMessageDigestAlgorithmType Digest
        {
            get { return GetDigest(); }
            set { SetDigest(value); }
        }

        public PdfSigningKey SigningKey
        {
            get { return GetSigningKey(); }
            set { SetSigningKey(value); }
        }

        public PdfLiteralStringObject Name
        {
            get { return GetName(); }
            set { SetName(value); }
        }

        public PdfLiteralStringObject Location
        {
            get { return GetLocation(); }
            set { SetLocation(value); }
        }

        public PdfLiteralStringObject Reason
        {
            get { return GetReason(); }
            set { SetReason(value); }
        }

        public PdfDate SigningTime
        {
            get { return GetSigningTime(); }
            set { SetSigningTime(value); }
        }

        public PdfHexadecimalStringObject Certificate
        {
            get { return GetCertificate(); }
            set { SetCertificate(value); }
        }

        public static PdfDocumentSignatureSettings Create()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDocumentSignatureSettings(data);
        }

        public PdfMessageDigestAlgorithmType GetDigest()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetDigest(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfMessageDigestAlgorithmType>.CheckedCast(data);
        }

        public void SetDigest(PdfMessageDigestAlgorithmType data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetDigest(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public PdfSigningKey GetSigningKey()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetSigningKey(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfSigningKey(data);
        }

        public void SetSigningKey(PdfSigningKey data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetSigningKey(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public PdfLiteralStringObject GetName()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetName(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        public void SetName(PdfLiteralStringObject data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetName(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public PdfLiteralStringObject GetLocation()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetLocation(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        public void SetLocation(PdfLiteralStringObject data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetLocation(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public PdfLiteralStringObject GetReason()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetReason(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        public void SetReason(PdfLiteralStringObject data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetReason(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public PdfDate GetSigningTime()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetSigningTime(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDate(data);
        }

        public void SetSigningTime(PdfDate data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetSigningTime(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public PdfHexadecimalStringObject GetCertificate()
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_GetCertificate(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfHexadecimalStringObject(data);
        }

        public void SetCertificate(PdfHexadecimalStringObject data)
        {
            UInt32 result = NativeMethods.DocumentSignatureSettings_SetCertificate(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
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
