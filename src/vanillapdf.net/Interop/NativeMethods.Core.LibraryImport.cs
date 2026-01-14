#if NET7_0_OR_GREATER

using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.Interop
{
    internal static partial class NativeMethods
    {
        #region IUnknown

        [LibraryImport(LibraryName)]
        public static partial UInt32 IUnknown_AddRef(PdfUnknownSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 IUnknown_Release(IntPtr handle);

        #endregion

        #region Buffer

        [LibraryImport(LibraryName)]
        public static partial UInt32 Buffer_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Buffer_ToUnknown(PdfBufferSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Buffer_FromUnknown(PdfUnknownSafeHandle handle, out PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Buffer_Create(out PdfBufferSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Buffer_CreateFromData(IntPtr data, UIntPtr size, out PdfBufferSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Buffer_GetData(PdfBufferSafeHandle handle, out IntPtr data, out UIntPtr size);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Buffer_SetData(PdfBufferSafeHandle handle, IntPtr data, UIntPtr size);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Buffer_ToInputStream(PdfBufferSafeHandle handle, out PdfInputStreamSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Buffer_Equals(PdfBufferSafeHandle handle, PdfBufferSafeHandle other, [MarshalAs(UnmanagedType.U1)] out bool data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Buffer_Hash(PdfBufferSafeHandle handle, out UIntPtr data);

        #endregion

        #region InputStream

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputStream_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputStream_ToUnknown(PdfInputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputStream_FromUnknown(PdfUnknownSafeHandle handle, out PdfInputStreamSafeHandle data);

        #endregion

        #region OutputStream

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutputStream_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutputStream_ToUnknown(PdfOutputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutputStream_FromUnknown(PdfUnknownSafeHandle handle, out PdfOutputStreamSafeHandle data);

        #endregion

        #region InputOutputStream

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_ToUnknown(PdfInputOutputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_FromUnknown(PdfUnknownSafeHandle handle, out PdfInputOutputStreamSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_ToInputStream(PdfInputOutputStreamSafeHandle handle, out PdfInputStreamSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_FromInputStream(PdfInputStreamSafeHandle handle, out PdfInputOutputStreamSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_ToOutputStream(PdfInputOutputStreamSafeHandle handle, out PdfOutputStreamSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_FromOutputStream(PdfOutputStreamSafeHandle handle, out PdfInputOutputStreamSafeHandle data);

        #endregion

        #region SigningKey

        [LibraryImport(LibraryName)]
        public static partial UInt32 SigningKey_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SigningKey_ToUnknown(PdfSigningKeySafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SigningKey_FromUnknown(PdfUnknownSafeHandle handle, out PdfSigningKeySafeHandle data);

        #endregion

        #region PKCS12Key

        [LibraryImport(LibraryName)]
        public static partial UInt32 PKCS12Key_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PKCS12Key_ToSigningKey(PdfPKCS12KeySafeHandle handle, out PdfSigningKeySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PKCS12Key_FromSigningKey(PdfSigningKeySafeHandle handle, out PdfPKCS12KeySafeHandle data);

        #endregion
    }
}

#endif
