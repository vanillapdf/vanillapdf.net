#if NETSTANDARD2_0

using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.Interop
{
    internal static partial class NativeMethods
    {
        #region IUnknown

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IUnknown_AddRef(PdfUnknownSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 IUnknown_Release(IntPtr handle);

        #endregion

        #region Buffer

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Buffer_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Buffer_ToUnknown(PdfBufferSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Buffer_FromUnknown(PdfUnknownSafeHandle handle, out PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Buffer_Create(out PdfBufferSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Buffer_CreateFromData(IntPtr data, UIntPtr size, out PdfBufferSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Buffer_GetData(PdfBufferSafeHandle handle, out IntPtr data, out UIntPtr size);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Buffer_SetData(PdfBufferSafeHandle handle, IntPtr data, UIntPtr size);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Buffer_ToInputStream(PdfBufferSafeHandle handle, out PdfInputStreamSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Buffer_Equals(PdfBufferSafeHandle handle, PdfBufferSafeHandle other, out bool data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Buffer_Hash(PdfBufferSafeHandle handle, out UIntPtr data);

        #endregion

        #region InputStream

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputStream_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputStream_ToUnknown(PdfInputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputStream_FromUnknown(PdfUnknownSafeHandle handle, out PdfInputStreamSafeHandle data);

        #endregion

        #region OutputStream

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutputStream_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutputStream_ToUnknown(PdfOutputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutputStream_FromUnknown(PdfUnknownSafeHandle handle, out PdfOutputStreamSafeHandle data);

        #endregion

        #region InputOutputStream

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_ToUnknown(PdfInputOutputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_FromUnknown(PdfUnknownSafeHandle handle, out PdfInputOutputStreamSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_ToInputStream(PdfInputOutputStreamSafeHandle handle, out PdfInputStreamSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_FromInputStream(PdfInputStreamSafeHandle handle, out PdfInputOutputStreamSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_ToOutputStream(PdfInputOutputStreamSafeHandle handle, out PdfOutputStreamSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_FromOutputStream(PdfOutputStreamSafeHandle handle, out PdfInputOutputStreamSafeHandle data);

        #endregion

        #region SigningKey

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 SigningKey_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 SigningKey_ToUnknown(PdfSigningKeySafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 SigningKey_FromUnknown(PdfUnknownSafeHandle handle, out PdfSigningKeySafeHandle data);

        #endregion

        #region PKCS12Key

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PKCS12Key_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PKCS12Key_ToSigningKey(PdfPKCS12KeySafeHandle handle, out PdfSigningKeySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PKCS12Key_FromSigningKey(PdfSigningKeySafeHandle handle, out PdfPKCS12KeySafeHandle data);

        #endregion
    }
}

#endif
