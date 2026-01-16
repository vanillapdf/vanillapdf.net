using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    internal sealed class PdfUnknownSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.IUnknown_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfBufferSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.Buffer_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfBufferSafeHandle handle)
        {
            UInt32 result = NativeMethods.Buffer_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfBufferSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.Buffer_FromUnknown(handle, out PdfBufferSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfInputStreamSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.InputStream_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfInputStreamSafeHandle handle)
        {
            UInt32 result = NativeMethods.InputStream_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfInputStreamSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.InputStream_FromUnknown(handle, out PdfInputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfOutputStreamSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.OutputStream_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfOutputStreamSafeHandle handle)
        {
            UInt32 result = NativeMethods.OutputStream_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfOutputStreamSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.OutputStream_FromUnknown(handle, out PdfOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfInputOutputStreamSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.InputOutputStream_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfUnknownSafeHandle(PdfInputOutputStreamSafeHandle handle)
        {
            UInt32 result = NativeMethods.InputOutputStream_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfInputOutputStreamSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.InputOutputStream_FromUnknown(handle, out PdfInputOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

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

        public static implicit operator PdfUnknownSafeHandle(PdfSigningKeySafeHandle handle)
        {
            UInt32 result = NativeMethods.SigningKey_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfSigningKeySafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = NativeMethods.SigningKey_FromUnknown(handle, out PdfSigningKeySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
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

        public static implicit operator PdfUnknownSafeHandle(PdfPKCS12KeySafeHandle handle)
        {
            return (PdfSigningKeySafeHandle)handle;
        }

        public static implicit operator PdfPKCS12KeySafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfSigningKeySafeHandle)handle;
        }
    }
}
