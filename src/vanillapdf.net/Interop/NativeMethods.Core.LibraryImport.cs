#if NET7_0_OR_GREATER

using System;
using System.Runtime.InteropServices;
using System.Text;
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

        #region Errors

        [LibraryImport(LibraryName)]
        public static partial UInt32 Errors_GetLastError(out UInt32 code);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Errors_GetPrintableErrorTextLength(UInt32 code, out UInt32 size);

        // Using DllImport because LibraryImport doesn't support StringBuilder
        [System.Runtime.InteropServices.DllImport(LibraryName, CallingConvention = LibraryCallingConvention, CharSet = CharSet.Ansi)]
        public static extern UInt32 Errors_GetPrintableErrorText(UInt32 code, StringBuilder buffer, UInt32 size);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Errors_GetLastErrorMessageLength(out UInt32 size);

        // Using DllImport because LibraryImport doesn't support StringBuilder
        [System.Runtime.InteropServices.DllImport(LibraryName, CallingConvention = LibraryCallingConvention, CharSet = CharSet.Ansi)]
        public static extern UInt32 Errors_GetLastErrorMessage(StringBuilder buffer, UInt32 size);

        #endregion

        #region LicenseInfo

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 LicenseInfo_SetLicenseFile(string filename);

        [LibraryImport(LibraryName)]
        public static partial UInt32 LicenseInfo_SetLicenseBuffer(PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 LicenseInfo_IsValid([MarshalAs(UnmanagedType.U1)] out bool data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 LicenseInfo_IsTemporary([MarshalAs(UnmanagedType.U1)] out bool data);

        #endregion

        #region Logging

        // Using DllImport because LibraryImport source generator doesn't support delegate parameters
        [System.Runtime.InteropServices.DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 Logging_SetCallbackLogger(SinkLogDelegate sinkLog, SinkFlushDelegate sinkFlush, IntPtr userdata);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 Logging_SetRotatingFileLogger(string filename, int maxFileSize, int maxFiles);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Logging_Shutdown();

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 Logging_SetPattern(string pattern);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Logging_GetSeverity(out int severity);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Logging_SetSeverity(PdfUtils.PdfLoggingSeverity severity);

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

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 InputStream_CreateFromFile(string filename, out PdfInputStreamSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputStream_CreateFromBuffer(PdfBufferSafeHandle buffer, out PdfInputStreamSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputStream_ToBuffer(PdfInputStreamSafeHandle handle, out PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputStream_GetInputPosition(PdfInputStreamSafeHandle handle, out Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputStream_SetInputPosition(PdfInputStreamSafeHandle handle, Int64 data);

        #endregion

        #region OutputStream

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutputStream_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutputStream_ToUnknown(PdfOutputStreamSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutputStream_FromUnknown(PdfUnknownSafeHandle handle, out PdfOutputStreamSafeHandle data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 OutputStream_CreateFromFile(string filename, out PdfOutputStreamSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutputStream_GetOutputPosition(PdfOutputStreamSafeHandle handle, out Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutputStream_SetOutputPosition(PdfOutputStreamSafeHandle handle, Int64 data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 OutputStream_WriteString(PdfOutputStreamSafeHandle handle, string data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutputStream_WriteBuffer(PdfOutputStreamSafeHandle handle, PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 OutputStream_Flush(PdfOutputStreamSafeHandle handle);

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

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 InputOutputStream_CreateFromFile(string filename, out PdfInputOutputStreamSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_CreateFromMemory(out PdfInputOutputStreamSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_Read(PdfInputOutputStreamSafeHandle handle, Int64 length, IntPtr data, out Int64 readLength);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_ReadBuffer(PdfInputOutputStreamSafeHandle handle, Int64 length, out PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_GetInputPosition(PdfInputOutputStreamSafeHandle handle, out Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_SetInputPosition(PdfInputOutputStreamSafeHandle handle, Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_GetOutputPosition(PdfInputOutputStreamSafeHandle handle, out Int64 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_SetOutputPosition(PdfInputOutputStreamSafeHandle handle, Int64 data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 InputOutputStream_WriteString(PdfInputOutputStreamSafeHandle handle, string data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_WriteBuffer(PdfInputOutputStreamSafeHandle handle, PdfBufferSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 InputOutputStream_Flush(PdfInputOutputStreamSafeHandle handle);

        #endregion

        #region SigningKey

        [LibraryImport(LibraryName)]
        public static partial UInt32 SigningKey_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SigningKey_ToUnknown(PdfSigningKeySafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SigningKey_FromUnknown(PdfUnknownSafeHandle handle, out PdfSigningKeySafeHandle data);

        // Using DllImport because LibraryImport source generator doesn't support delegate parameters
        [System.Runtime.InteropServices.DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 SigningKey_CreateCustom(
            InitializeDelegate initialize,
            UpdateDelegate update,
            FinalDelegate final,
            CleanupDelegate cleanup,
            IntPtr userdata,
            out PdfSigningKeySafeHandle data);

        #endregion

        #region PKCS12Key

        [LibraryImport(LibraryName)]
        public static partial UInt32 PKCS12Key_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PKCS12Key_ToSigningKey(PdfPKCS12KeySafeHandle handle, out PdfSigningKeySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PKCS12Key_FromSigningKey(PdfSigningKeySafeHandle handle, out PdfPKCS12KeySafeHandle data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 PKCS12Key_CreateFromFile(string filename, string password, out PdfPKCS12KeySafeHandle data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 PKCS12Key_CreateFromBuffer(PdfBufferSafeHandle buffer, string password, out PdfPKCS12KeySafeHandle data);

        #endregion
    }
}

#endif
