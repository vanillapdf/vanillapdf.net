#if NET7_0_OR_GREATER

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using vanillapdf.net.Utils;

namespace vanillapdf.net.Interop
{
    internal static partial class NativeMethods
    {
        #region Errors

        [LibraryImport(LibraryName)]
        public static partial UInt32 Errors_GetLastError(out UInt32 code);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Errors_GetPrintableErrorTextLength(UInt32 code, out UInt32 size);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Errors_GetPrintableErrorText(UInt32 code, byte[] buffer, UInt32 size);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Errors_GetLastErrorMessageLength(out UInt32 size);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Errors_GetLastErrorMessage(byte[] buffer, UInt32 size);

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

        [LibraryImport(LibraryName)]
        public static partial UInt32 Logging_SetCallbackLogger(IntPtr sinkLog, IntPtr sinkFlush, IntPtr userdata);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 Logging_SetRotatingFileLogger(string filename, int maxFileSize, int maxFiles);

        [LibraryImport(LibraryName)]
        public static partial UInt32 Logging_Shutdown();

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
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
        public static partial UInt32 SigningKey_CreateCustom(
            IntPtr initialize,
            IntPtr update,
            IntPtr final,
            IntPtr cleanup,
            IntPtr userdata,
            out PdfSigningKeySafeHandle data);

        #endregion

        #region ObjectDiagnostics

        [LibraryImport(LibraryName)]
        public static partial UInt32 ObjectDiagnostics_GetActiveObjectCount(out Int64 result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ObjectDiagnostics_GetPeakObjectCount(out Int64 result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ObjectDiagnostics_GetTotalObjectsCreated(out Int64 result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ObjectDiagnostics_ResetCounters();

        #endregion

        #region PKCS12Key

        [LibraryImport(LibraryName)]
        public static partial UInt32 PKCS12Key_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PKCS12Key_ToSigningKey(PdfPKCS12KeySafeHandle handle, out PdfSigningKeySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 PKCS12Key_FromSigningKey(PdfSigningKeySafeHandle handle, out PdfPKCS12KeySafeHandle data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 PKCS12Key_CreateFromFile(
            string filename,
            [MarshalUsing(typeof(AnsiStringMarshaller))] string password,
            out PdfPKCS12KeySafeHandle data);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
        public static partial UInt32 PKCS12Key_CreateFromBuffer(PdfBufferSafeHandle buffer, string password, out PdfPKCS12KeySafeHandle data);

        #endregion

        #region TrustedCertificateStore

        [LibraryImport(LibraryName)]
        public static partial UInt32 TrustedCertificateStore_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TrustedCertificateStore_Create(out TrustedCertificateStoreSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TrustedCertificateStore_AddCertificateFromPEM(TrustedCertificateStoreSafeHandle handle, PdfBufferSafeHandle pemData);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TrustedCertificateStore_AddCertificateFromDER(TrustedCertificateStoreSafeHandle handle, PdfBufferSafeHandle derData);

        [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
        public static partial UInt32 TrustedCertificateStore_LoadFromDirectory(TrustedCertificateStoreSafeHandle handle, string directoryPath);

        [LibraryImport(LibraryName)]
        public static partial UInt32 TrustedCertificateStore_LoadSystemDefaults(TrustedCertificateStoreSafeHandle handle);

        #endregion

        #region SignatureVerificationSettings

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationSettings_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationSettings_Create(out SignatureVerificationSettingsSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationSettings_GetSkipCertificateValidation(SignatureVerificationSettingsSafeHandle handle, out int value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationSettings_SetSkipCertificateValidation(SignatureVerificationSettingsSafeHandle handle, int value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationSettings_GetCheckSigningTimeFlag(SignatureVerificationSettingsSafeHandle handle, [MarshalAs(UnmanagedType.U1)] out bool value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationSettings_SetCheckSigningTimeFlag(SignatureVerificationSettingsSafeHandle handle, int value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationSettings_GetAllowWeakAlgorithmsFlag(SignatureVerificationSettingsSafeHandle handle, out int value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationSettings_SetAllowWeakAlgorithmsFlag(SignatureVerificationSettingsSafeHandle handle, int value);

        #endregion

        #region SignatureVerificationResult

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationResult_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationResult_GetStatus(SignatureVerificationResultSafeHandle handle, out int status);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationResult_GetMessage(SignatureVerificationResultSafeHandle handle, out PdfBufferSafeHandle buffer);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationResult_IsSignatureValid(SignatureVerificationResultSafeHandle handle, out int value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationResult_IsDocumentIntact(SignatureVerificationResultSafeHandle handle, out int value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationResult_IsCertificateTrusted(SignatureVerificationResultSafeHandle handle, out int value);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationResult_GetSignerCertificate(SignatureVerificationResultSafeHandle handle, out PdfBufferSafeHandle buffer);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationResult_GetCertificateChainCount(SignatureVerificationResultSafeHandle handle, out UIntPtr count);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationResult_GetCertificateChainAt(SignatureVerificationResultSafeHandle handle, UIntPtr index, out PdfBufferSafeHandle buffer);

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerificationResult_GetSignerCommonName(SignatureVerificationResultSafeHandle handle, out PdfBufferSafeHandle buffer);

        #endregion

        #region SignatureVerifier

        [LibraryImport(LibraryName)]
        public static partial UInt32 SignatureVerifier_Verify(
            PdfBufferSafeHandle signedData,
            PdfBufferSafeHandle signatureContents,
            TrustedCertificateStoreSafeHandle trustedStore,
            SignatureVerificationSettingsSafeHandle settings,
            out SignatureVerificationResultSafeHandle result);

        #endregion
    }
}

#endif
