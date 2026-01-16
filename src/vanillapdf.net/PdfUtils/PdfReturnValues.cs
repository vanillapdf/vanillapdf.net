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
            switch (value)
            {
                case ERROR_SUCCESS: return nameof(ERROR_SUCCESS);
                case ERROR_PARAMETER_VALUE: return nameof(ERROR_PARAMETER_VALUE);
                case ERROR_NOT_SUPPORTED: return nameof(ERROR_NOT_SUPPORTED);
                case ERROR_USER_CANCELLED: return nameof(ERROR_USER_CANCELLED);
                case ERROR_ZLIB_DATA: return nameof(ERROR_ZLIB_DATA);
                case ERROR_INVALID_LICENSE: return nameof(ERROR_INVALID_LICENSE);
                case ERROR_LICENSE_REQUIRED: return nameof(ERROR_LICENSE_REQUIRED);
                case ERROR_INSUFFICIENT_SPACE: return nameof(ERROR_INSUFFICIENT_SPACE);
                case ERROR_GENERAL: return nameof(ERROR_GENERAL);
                case ERROR_CONVERSION: return nameof(ERROR_CONVERSION);
                case ERROR_FILE_DISPOSED: return nameof(ERROR_FILE_DISPOSED);
                case ERROR_FILE_NOT_INITIALIZED: return nameof(ERROR_FILE_NOT_INITIALIZED);
                case ERROR_OBJECT_MISSING: return nameof(ERROR_OBJECT_MISSING);
                case ERROR_PARSE_EXCEPTION: return nameof(ERROR_PARSE_EXCEPTION);
                case ERROR_INVALID_PASSWORD: return nameof(ERROR_INVALID_PASSWORD);
                case ERROR_DUPLICATE_KEY: return nameof(ERROR_DUPLICATE_KEY);
                case ERROR_OPTIONAL_ENTRY_MISSING: return nameof(ERROR_OPTIONAL_ENTRY_MISSING);
                case ERROR_SEMANTIC_CONTEXT: return nameof(ERROR_SEMANTIC_CONTEXT);
                default: throw new PdfManagedException($"Unknown return value: {value}");
            }
        }

        #region Global error states

        /// <summary>
        /// Indicates that the operation completed successfully.
        /// </summary>
        public const UInt32 ERROR_SUCCESS = 0;

        /// <summary>
        /// An invalid parameter value to function call was passed.
        /// </summary>
        public const UInt32 ERROR_PARAMETER_VALUE = 1;

        /// <summary>
        /// Operation is currently not supported.
        /// </summary>
        public const UInt32 ERROR_NOT_SUPPORTED = 2;

        /// <summary>
        /// Operation was cancelled by user.
        /// </summary>
        public const UInt32 ERROR_USER_CANCELLED = 3;

        /// <summary>
        /// Error in compressed data.
        /// </summary>
        public const UInt32 ERROR_ZLIB_DATA = 4;

        /// <summary>
        /// Presented license file is not valid.
        /// </summary>
        public const UInt32 ERROR_INVALID_LICENSE = 5;

        /// <summary>
        /// Error accessing licensed feature without a valid license file.
        /// </summary>
        public const UInt32 ERROR_LICENSE_REQUIRED = 6;

        /// <summary>
        /// The space usually for buffer was not sufficient for requested operation.
        /// </summary>
        public const UInt32 ERROR_INSUFFICIENT_SPACE = 7;

        /// <summary>
        /// An unknown error has occurred.
        /// </summary>
        public const UInt32 ERROR_GENERAL = 0xFFFFFFFF;

        #endregion

        #region Syntax errors

        /// <summary>
        /// An invalid object type was passed to function.
        /// </summary>
        public const UInt32 ERROR_CONVERSION = 0x00010000;

        /// <summary>
        /// The source file was already disposed.
        /// </summary>
        public const UInt32 ERROR_FILE_DISPOSED = 0x00010001;

        /// <summary>
        /// The source file was not yet initialized.
        /// </summary>
        public const UInt32 ERROR_FILE_NOT_INITIALIZED = 0x00010002;

        /// <summary>
        /// A dependent object was not found.
        /// </summary>
        public const UInt32 ERROR_OBJECT_MISSING = 0x00010003;

        /// <summary>
        /// Error during low-level file processing, the document might be damaged.
        ///
        /// If the document can be correctly processed with other readers,
        /// please let us know by sending the document for observations.
        /// In case the document contains sensitive information,
        /// consider document anonymizer utility.
        /// </summary>
        public const UInt32 ERROR_PARSE_EXCEPTION = 0x00010004;

        /// <summary>
        /// Invalid protection password or key.
        ///
        /// The source file was encrypted, while the entered password did not match.
        /// </summary>
        public const UInt32 ERROR_INVALID_PASSWORD = 0x00010005;

        /// <summary>
        /// Trying to insert a new entry into dictionary with duplicate key.
        /// </summary>
        public const UInt32 ERROR_DUPLICATE_KEY = 0x00010006;

        #endregion

        #region Semantic errors

        /// <summary>
        /// The requested object is empty or missing.
        /// </summary>
        public const UInt32 ERROR_OPTIONAL_ENTRY_MISSING = 0x10000000;

        /// <summary>
        /// The underlying type of the object was different than expected.
        /// </summary>
        public const UInt32 ERROR_SEMANTIC_CONTEXT = 0x10000001;

        #endregion
    }
}
