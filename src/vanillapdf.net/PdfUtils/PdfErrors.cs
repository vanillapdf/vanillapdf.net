using System;
using System.Text;
using vanillapdf.net.Interop;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Class for obtaining error messages from native library
    /// </summary>
    public static class PdfErrors
    {
        /// <summary>
        /// Get last error code that occurred in the native library
        /// </summary>
        /// <returns>Error code of the last failed operation on success, throws exception if the error cannot be returned</returns>
        public static UInt32 GetLastError()
        {
            UInt32 result = NativeMethods.Errors_GetLastError(out UInt32 code);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw new PdfManagedException($"Could not get last error: {result}");
            }

            return code;
        }

        /// <summary>
        /// Get text representation of the error code
        /// </summary>
        /// <param name="error">Error code</param>
        /// <returns>Text representation of the error code on success, throws exception of failure</returns>
        public static string GetPrintableErrorText(UInt32 error)
        {
            UInt32 lengthResult = NativeMethods.Errors_GetPrintableErrorTextLength(error, out UInt32 length);
            if (lengthResult != PdfReturnValues.ERROR_SUCCESS) {
                throw new PdfManagedException($"Could not get last error message length: {lengthResult}");
            }

            int convertedLength = Convert.ToInt32(length);
            StringBuilder sb = new StringBuilder(convertedLength);

            UInt32 messageResult = NativeMethods.Errors_GetPrintableErrorText(error, sb, length);
            if (messageResult != PdfReturnValues.ERROR_SUCCESS) {
                throw new PdfManagedException($"Could not get last error message: {messageResult}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Get additional text message of the last error
        /// </summary>
        /// <returns>Additional text message on success, throws exception on failure</returns>
        public static string GetLastErrorMessage()
        {
            UInt32 lengthResult = NativeMethods.Errors_GetLastErrorMessageLength(out UInt32 length);
            if (lengthResult != PdfReturnValues.ERROR_SUCCESS) {
                throw new PdfManagedException("Could not get last error message length");
            }

            int convertedLength = Convert.ToInt32(length);
            StringBuilder sb = new StringBuilder(convertedLength);

            UInt32 messageResult = NativeMethods.Errors_GetLastErrorMessage(sb, length);
            if (messageResult != PdfReturnValues.ERROR_SUCCESS) {
                throw new PdfManagedException("Could not get last error message");
            }

            return sb.ToString();
        }

        internal static PdfUnmanagedException GetLastErrorException()
        {
            uint value = GetLastError();
            string message = GetLastErrorMessage();

            return PdfUnmanagedException.GetException(value, message);
        }

        internal static PdfUnmanagedException GetErrorException(uint value)
        {
            string message = GetPrintableErrorText(value);
            return PdfUnmanagedException.GetException(value, message);
        }

    }
}
