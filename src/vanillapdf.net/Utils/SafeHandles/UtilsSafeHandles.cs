using System;
using System.Runtime.InteropServices;
using static vanillapdf.net.Utils.MiscUtils;

namespace vanillapdf.net.Utils.SafeHandles
{
    internal sealed class PdfUnknownSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("IUnknown_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        public void AddRef()
        {
            UInt32 result = IUnknown_AddRef(this);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Release()
        {
            UInt32 result = IUnknown_Release(this);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private static AddRefDelegate IUnknown_AddRef = LibraryInstance.GetFunction<AddRefDelegate>("IUnknown_AddRef");
        private static ReleaseRefDelegate IUnknown_Release = LibraryInstance.GetFunction<ReleaseRefDelegate>("IUnknown_Release");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 AddRefDelegate(PdfUnknownSafeHandle handle);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ReleaseRefDelegate(PdfUnknownSafeHandle handle);
    }

    internal sealed class PdfBufferSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("Buffer_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("Buffer_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("Buffer_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfBufferSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfBufferSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfBufferSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfBufferSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfBufferSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfInputStreamSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("InputStream_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("InputStream_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("InputStream_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfInputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfInputStreamSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfInputStreamSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfInputStreamSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfInputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfOutputStreamSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("OutputStream_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("OutputStream_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("OutputStream_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfOutputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfOutputStreamSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfOutputStreamSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfOutputStreamSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfInputOutputStreamSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("InputOutputStream_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("InputOutputStream_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("InputOutputStream_FromUnknown");

        private static ToInputStreamDelegate InputOutputStream_ToInputStream = LibraryInstance.GetFunction<ToInputStreamDelegate>("InputOutputStream_ToInputStream");
        private static FromInputStreamDelegate InputOutputStream_FromInputStream = LibraryInstance.GetFunction<FromInputStreamDelegate>("InputOutputStream_FromInputStream");

        private static ToOutputStreamDelegate InputOutputStream_ToOutputStream = LibraryInstance.GetFunction<ToOutputStreamDelegate>("InputOutputStream_ToOutputStream");
        private static FromOutputStreamDelegate InputOutputStream_FromOutputStream = LibraryInstance.GetFunction<FromOutputStreamDelegate>("InputOutputStream_FromOutputStream");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfInputOutputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfInputOutputStreamSafeHandle data);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ToInputStreamDelegate(PdfInputOutputStreamSafeHandle handle, out PdfInputStreamSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 FromInputStreamDelegate(PdfInputStreamSafeHandle handle, out PdfInputOutputStreamSafeHandle data);

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ToOutputStreamDelegate(PdfInputOutputStreamSafeHandle handle, out PdfOutputStreamSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 FromOutputStreamDelegate(PdfOutputStreamSafeHandle handle, out PdfInputOutputStreamSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfInputOutputStreamSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfInputOutputStreamSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfInputOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfInputStreamSafeHandle(PdfInputOutputStreamSafeHandle handle)
        {
            UInt32 result = InputOutputStream_ToInputStream(handle, out PdfInputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfInputOutputStreamSafeHandle(PdfInputStreamSafeHandle handle)
        {
            UInt32 result = InputOutputStream_FromInputStream(handle, out PdfInputOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfOutputStreamSafeHandle(PdfInputOutputStreamSafeHandle handle)
        {
            UInt32 result = InputOutputStream_ToOutputStream(handle, out PdfOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfInputOutputStreamSafeHandle(PdfOutputStreamSafeHandle handle)
        {
            UInt32 result = InputOutputStream_FromOutputStream(handle, out PdfInputOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfSigningKeySafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("SigningKey_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("SigningKey_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("SigningKey_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfSigningKeySafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfSigningKeySafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfSigningKeySafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfSigningKeySafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfSigningKeySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfPKCS12KeySafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("PKCS12Key_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToSigningKeyDelegate Convert_ToSigningKey = LibraryInstance.GetFunction<ConvertToSigningKeyDelegate>("PKCS12Key_ToSigningKey");
        private static ConvertFromSigningKeyDelegate Convert_FromSigningKey = LibraryInstance.GetFunction<ConvertFromSigningKeyDelegate>("PKCS12Key_FromSigningKey");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToSigningKeyDelegate(PdfPKCS12KeySafeHandle handle, out PdfSigningKeySafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromSigningKeyDelegate(PdfSigningKeySafeHandle handle, out PdfPKCS12KeySafeHandle data);

        public static implicit operator PdfSigningKeySafeHandle(PdfPKCS12KeySafeHandle handle)
        {
            UInt32 result = Convert_ToSigningKey(handle, out PdfSigningKeySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfPKCS12KeySafeHandle(PdfSigningKeySafeHandle handle)
        {
            UInt32 result = Convert_FromSigningKey(handle, out PdfPKCS12KeySafeHandle data);
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
