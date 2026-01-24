#if NETSTANDARD2_0

using System;
using System.Runtime.InteropServices;
using System.Text;
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

        #region Errors

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Errors_GetLastError(out UInt32 code);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Errors_GetPrintableErrorTextLength(UInt32 code, out UInt32 size);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Errors_GetPrintableErrorText(UInt32 code, byte[] buffer, UInt32 size);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Errors_GetLastErrorMessageLength(out UInt32 size);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Errors_GetLastErrorMessage(byte[] buffer, UInt32 size);

        #endregion

        #region LicenseInfo

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 LicenseInfo_SetLicenseFile(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string filename);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 LicenseInfo_SetLicenseBuffer(PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 LicenseInfo_IsValid(out bool data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 LicenseInfo_IsTemporary(out bool data);

        #endregion

        #region Logging

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Logging_SetCallbackLogger(IntPtr sinkLog, IntPtr sinkFlush, IntPtr userdata);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Logging_SetRotatingFileLogger(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string filename,
            int maxFileSize,
            int maxFiles);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Logging_Shutdown();

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention, CharSet = CharSet.Ansi)]
        public static extern UInt32 Logging_SetPattern(string pattern);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Logging_GetSeverity(out int severity);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Logging_SetSeverity(PdfUtils.PdfLoggingSeverity severity);

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

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention, CharSet = CharSet.Ansi)]
        public static extern UInt32 InputStream_CreateFromFile(string filename, out PdfInputStreamSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputStream_CreateFromBuffer(PdfBufferSafeHandle buffer, out PdfInputStreamSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputStream_ToBuffer(PdfInputStreamSafeHandle handle, out PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputStream_GetInputPosition(PdfInputStreamSafeHandle handle, out Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputStream_SetInputPosition(PdfInputStreamSafeHandle handle, Int64 data);

        #endregion

        #region OutputStream

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutputStream_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutputStream_ToUnknown(PdfOutputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutputStream_FromUnknown(PdfUnknownSafeHandle handle, out PdfOutputStreamSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention, CharSet = CharSet.Ansi)]
        public static extern UInt32 OutputStream_CreateFromFile(string filename, out PdfOutputStreamSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutputStream_GetOutputPosition(PdfOutputStreamSafeHandle handle, out Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutputStream_SetOutputPosition(PdfOutputStreamSafeHandle handle, Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention, CharSet = CharSet.Ansi)]
        public static extern UInt32 OutputStream_WriteString(PdfOutputStreamSafeHandle handle, string data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutputStream_WriteBuffer(PdfOutputStreamSafeHandle handle, PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 OutputStream_Flush(PdfOutputStreamSafeHandle handle);

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

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention, CharSet = CharSet.Ansi)]
        public static extern UInt32 InputOutputStream_CreateFromFile(string filename, out PdfInputOutputStreamSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_CreateFromMemory(out PdfInputOutputStreamSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_Read(PdfInputOutputStreamSafeHandle handle, Int64 length, IntPtr data, out Int64 readLength);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_ReadBuffer(PdfInputOutputStreamSafeHandle handle, Int64 length, out PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_GetInputPosition(PdfInputOutputStreamSafeHandle handle, out Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_SetInputPosition(PdfInputOutputStreamSafeHandle handle, Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_GetOutputPosition(PdfInputOutputStreamSafeHandle handle, out Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_SetOutputPosition(PdfInputOutputStreamSafeHandle handle, Int64 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention, CharSet = CharSet.Ansi)]
        public static extern UInt32 InputOutputStream_WriteString(PdfInputOutputStreamSafeHandle handle, string data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_WriteBuffer(PdfInputOutputStreamSafeHandle handle, PdfBufferSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 InputOutputStream_Flush(PdfInputOutputStreamSafeHandle handle);

        #endregion

        #region SigningKey

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 SigningKey_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 SigningKey_ToUnknown(PdfSigningKeySafeHandle handle, out PdfUnknownSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 SigningKey_FromUnknown(PdfUnknownSafeHandle handle, out PdfSigningKeySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 SigningKey_CreateCustom(
            IntPtr initialize,
            IntPtr update,
            IntPtr final,
            IntPtr cleanup,
            IntPtr userdata,
            out PdfSigningKeySafeHandle data);

        #endregion

        #region PKCS12Key

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PKCS12Key_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PKCS12Key_ToSigningKey(PdfPKCS12KeySafeHandle handle, out PdfSigningKeySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PKCS12Key_FromSigningKey(PdfSigningKeySafeHandle handle, out PdfPKCS12KeySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PKCS12Key_CreateFromFile(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]
            string filename,
            [MarshalAs(UnmanagedType.LPStr)]
            string password,
            out PdfPKCS12KeySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 PKCS12Key_CreateFromBuffer(
            PdfBufferSafeHandle buffer,
            [MarshalAs(UnmanagedType.LPStr)]
            string password,
            out PdfPKCS12KeySafeHandle data);

        #endregion
    }
}

#endif
