using System;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Class holds commonly used return values from the native code
    /// </summary>
    public static class PdfReturnValues
    {
        internal static string GetValueName(uint value)
        {
            if (value == ERROR_SUCCESS) {
                return VANILLAPDF_ERROR_SUCCESS;
            }

            if (value == ERROR_PARAMETER_VALUE) {
                return VANILLAPDF_ERROR_PARAMETER_VALUE;
            }

            if (value == ERROR_NOT_SUPPORTED) {
                return VANILLAPDF_ERROR_NOT_SUPPORTED;
            }

            if (value == ERROR_GENERAL) {
                return VANILLAPDF_ERROR_GENERAL;
            }

            if (value == ERROR_CONVERSION) {
                return VANILLAPDF_ERROR_CONVERSION;
            }

            if (value == ERROR_FILE_DISPOSED) {
                return VANILLAPDF_ERROR_FILE_DISPOSED;
            }

            if (value == ERROR_FILE_NOT_INITIALIZED) {
                return VANILLAPDF_ERROR_FILE_NOT_INITIALIZED;
            }

            if (value == ERROR_OBJECT_MISSING) {
                return VANILLAPDF_ERROR_OBJECT_MISSING;
            }

            if (value == ERROR_PARSE_EXCEPTION) {
                return VANILLAPDF_ERROR_PARSE_EXCEPTION;
            }

            if (value == ERROR_INVALID_PASSWORD) {
                return VANILLAPDF_ERROR_INVALID_PASSWORD;
            }

            throw new PdfManagedException($"Unknown return value: {value}");
        }

        #region Global error states

        /// <summary>
        /// Indicates that the operation completed successfully.
        /// </summary>
        public static UInt32 ERROR_SUCCESS = LibraryInstance.GetConstant(VANILLAPDF_ERROR_SUCCESS);

        /// <summary>
        /// An invalid parameter value to function call was passed.
        /// </summary>
        public static UInt32 ERROR_PARAMETER_VALUE = LibraryInstance.GetConstant(VANILLAPDF_ERROR_PARAMETER_VALUE);

        /// <summary>
        /// Operation is currently not supported.
        /// </summary>
        public static UInt32 ERROR_NOT_SUPPORTED = LibraryInstance.GetConstant(VANILLAPDF_ERROR_NOT_SUPPORTED);

        /// <summary>
        /// Operation was cancelled by user.
        /// </summary>
        public static UInt32 ERROR_USER_CANCELLED = LibraryInstance.GetConstant(VANILLAPDF_ERROR_USER_CANCELLED);

        /// <summary>
        /// Error in compressed data.
        /// </summary>
        public static UInt32 ERROR_ZLIB_DATA = LibraryInstance.GetConstant(VANILLAPDF_ERROR_ZLIB_DATA);

        /// <summary>
        /// Presented license file is not valid.
        /// </summary>
        public static UInt32 ERROR_INVALID_LICENSE = LibraryInstance.GetConstant(VANILLAPDF_ERROR_INVALID_LICENSE);

        /// <summary>
        /// Error accessing licensed feature without a valid license file.
        /// </summary>
        public static UInt32 ERROR_LICENSE_REQUIRED = LibraryInstance.GetConstant(VANILLAPDF_ERROR_LICENSE_REQUIRED);

        /// <summary>
        /// An unknown error has occurred.
        /// </summary>
        public static UInt32 ERROR_GENERAL = LibraryInstance.GetConstant(VANILLAPDF_ERROR_GENERAL);

        #endregion

        #region Syntax errors

        /// <summary>
        /// An invalid object type was passed to function.
        /// </summary>
        public static UInt32 ERROR_CONVERSION = LibraryInstance.GetConstant(VANILLAPDF_ERROR_CONVERSION);

        /// <summary>
        /// The source file was already disposed.
        /// </summary>
        public static UInt32 ERROR_FILE_DISPOSED = LibraryInstance.GetConstant(VANILLAPDF_ERROR_FILE_DISPOSED);

        /// <summary>
        /// The source file was not yet initialized.
        /// </summary>
        public static UInt32 ERROR_FILE_NOT_INITIALIZED = LibraryInstance.GetConstant(VANILLAPDF_ERROR_FILE_NOT_INITIALIZED);

        /// <summary>
        /// A dependent object was not found.
        /// </summary>
        public static UInt32 ERROR_OBJECT_MISSING = LibraryInstance.GetConstant(VANILLAPDF_ERROR_OBJECT_MISSING);

        /// <summary>
        /// Error during low-level file processing, the document might be damaged.
        /// 
        /// If the document can be correctly processed with other readers,
        /// please let us know by sending the document for observations.
        /// In case the document contains sensitive information,
        /// consider document anonymizer utility.
        /// </summary>
        public static UInt32 ERROR_PARSE_EXCEPTION = LibraryInstance.GetConstant(VANILLAPDF_ERROR_PARSE_EXCEPTION);

        /// <summary>
        /// Invalid protection password or key.
        /// 
        /// The source file was encrypted, while the entered password did not match.
        /// </summary>
        public static UInt32 ERROR_INVALID_PASSWORD = LibraryInstance.GetConstant(VANILLAPDF_ERROR_INVALID_PASSWORD);

        /// <summary>
        /// Trying to insert a new entry into dictionary with duplicate key.
        /// </summary>
        public static UInt32 ERROR_DUPLICATE_KEY = LibraryInstance.GetConstant(VANILLAPDF_ERROR_DUPLICATE_KEY);

        #endregion

        private const string VANILLAPDF_ERROR_SUCCESS = "VANILLAPDF_ERROR_SUCCESS";
        private const string VANILLAPDF_ERROR_PARAMETER_VALUE = "VANILLAPDF_ERROR_PARAMETER_VALUE";
        private const string VANILLAPDF_ERROR_NOT_SUPPORTED = "VANILLAPDF_ERROR_NOT_SUPPORTED";
        private const string VANILLAPDF_ERROR_USER_CANCELLED = "VANILLAPDF_ERROR_USER_CANCELLED";
        private const string VANILLAPDF_ERROR_ZLIB_DATA = "VANILLAPDF_ERROR_ZLIB_DATA";
        private const string VANILLAPDF_ERROR_INVALID_LICENSE = "VANILLAPDF_ERROR_INVALID_LICENSE";
        private const string VANILLAPDF_ERROR_LICENSE_REQUIRED = "VANILLAPDF_ERROR_LICENSE_REQUIRED";
        private const string VANILLAPDF_ERROR_GENERAL = "VANILLAPDF_ERROR_GENERAL";

        private const string VANILLAPDF_ERROR_CONVERSION = "VANILLAPDF_ERROR_CONVERSION";
        private const string VANILLAPDF_ERROR_FILE_DISPOSED = "VANILLAPDF_ERROR_FILE_DISPOSED";
        private const string VANILLAPDF_ERROR_FILE_NOT_INITIALIZED = "VANILLAPDF_ERROR_FILE_NOT_INITIALIZED";
        private const string VANILLAPDF_ERROR_OBJECT_MISSING = "VANILLAPDF_ERROR_OBJECT_MISSING";
        private const string VANILLAPDF_ERROR_PARSE_EXCEPTION = "VANILLAPDF_ERROR_PARSE_EXCEPTION";
        private const string VANILLAPDF_ERROR_INVALID_PASSWORD = "VANILLAPDF_ERROR_INVALID_PASSWORD";
        private const string VANILLAPDF_ERROR_DUPLICATE_KEY = "VANILLAPDF_ERROR_DUPLICATE_KEY";

    }
}
