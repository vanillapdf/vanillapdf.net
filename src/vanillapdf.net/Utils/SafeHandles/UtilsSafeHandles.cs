using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    internal sealed class PdfBufferSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Buffer_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfInputStreamSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.InputStream_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfOutputStreamSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.OutputStream_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfInputOutputStreamSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.InputOutputStream_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfInputStreamSafeHandle(PdfInputOutputStreamSafeHandle handle)
        {
            UInt32 result = NativeMethods.InputOutputStream_ToInputStream(handle, out PdfInputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfInputOutputStreamSafeHandle(PdfInputStreamSafeHandle handle)
        {
            UInt32 result = NativeMethods.InputOutputStream_FromInputStream(handle, out PdfInputOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfOutputStreamSafeHandle(PdfInputOutputStreamSafeHandle handle)
        {
            UInt32 result = NativeMethods.InputOutputStream_ToOutputStream(handle, out PdfOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfInputOutputStreamSafeHandle(PdfOutputStreamSafeHandle handle)
        {
            UInt32 result = NativeMethods.InputOutputStream_FromOutputStream(handle, out PdfInputOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfSigningKeySafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.SigningKey_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfPKCS12KeySafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.PKCS12Key_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfSigningKeySafeHandle(PdfPKCS12KeySafeHandle handle)
        {
            UInt32 result = NativeMethods.PKCS12Key_ToSigningKey(handle, out PdfSigningKeySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfPKCS12KeySafeHandle(PdfSigningKeySafeHandle handle)
        {
            UInt32 result = NativeMethods.PKCS12Key_FromSigningKey(handle, out PdfPKCS12KeySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class TrustedCertificateStoreSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.TrustedCertificateStore_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(TrustedCertificateStoreSafeHandle handle)
        {
            UInt32 result = NativeMethods.TrustedCertificateStore_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator TrustedCertificateStoreSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.TrustedCertificateStore_FromUnknown(handle, out TrustedCertificateStoreSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class SignatureVerificationSettingsSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.SignatureVerificationSettings_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(SignatureVerificationSettingsSafeHandle handle)
        {
            UInt32 result = NativeMethods.SignatureVerificationSettings_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator SignatureVerificationSettingsSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.SignatureVerificationSettings_FromUnknown(handle, out SignatureVerificationSettingsSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class SignatureVerificationResultSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.SignatureVerificationResult_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(SignatureVerificationResultSafeHandle handle)
        {
            UInt32 result = NativeMethods.SignatureVerificationResult_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator SignatureVerificationResultSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.SignatureVerificationResult_FromUnknown(handle, out SignatureVerificationResultSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }
}
