using System;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Base exception class for all unmanaged derived exceptions
    /// </summary>
    public abstract class PdfUnmanagedException : PdfBaseException
    {
        /// <summary>
        /// Native error code associated with the reason of error
        /// </summary>
        public UInt32 ErrorCode { get; protected set; }

        internal PdfUnmanagedException(string message, UInt32 errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        internal static PdfUnmanagedException GetException(uint value, string errorMessage = null)
        {
            // Global errors
            if (value == PdfReturnValues.ERROR_PARAMETER_VALUE) {
                return PdfParameterValueException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_NOT_SUPPORTED) {
                return PdfNotSupportedException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_USER_CANCELLED) {
                return PdfUserCancelledException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_ZLIB_DATA) {
                return PdfZlibDataException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_INVALID_LICENSE) {
                return PdfInvalidLicenseException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_LICENSE_REQUIRED) {
                return PdfLicenseRequiredException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_INSUFFICIENT_SPACE) {
                // TODO
            }

            if (value == PdfReturnValues.ERROR_GENERAL) {
                return PdfGeneralException.Create(errorMessage);
            }

            // Syntax errors
            if (value == PdfReturnValues.ERROR_CONVERSION) {
                return PdfConversionException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_FILE_DISPOSED) {
                return PdfFileDisposedException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_FILE_NOT_INITIALIZED) {
                return PdfFileNotInitializedException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return PdfObjectMissingException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_PARSE_EXCEPTION) {
                return PdfParseException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_INVALID_PASSWORD) {
                return PdfInvalidPasswordException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_DUPLICATE_KEY) {
                // TODO
            }

            // Semantic errors

            if (value == PdfReturnValues.ERROR_OPTIONAL_ENTRY_MISSING) {
                // TODO
            }

            if (value == PdfReturnValues.ERROR_SEMANTIC_CONTEXT) {
                // TODO
            }

            throw new PdfManagedException($"Unknown return value: {value}");
        }
    }

    /// <summary>
    /// An invalid unexpected parameter was passed to native function
    /// </summary>
    public class PdfParameterValueException : PdfUnmanagedException
    {
        internal static PdfParameterValueException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_PARAMETER_VALUE;
            return new PdfParameterValueException(message, code);
        }

        private PdfParameterValueException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    /// <summary>
    /// The required feature is not supported in the current library version
    /// </summary>
    public class PdfNotSupportedException : PdfUnmanagedException
    {
        internal static PdfNotSupportedException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_NOT_SUPPORTED;
            return new PdfNotSupportedException(message, code);
        }

        private PdfNotSupportedException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    /// <summary>
    /// Usually happens in the callback functions and server as a way to terminate current function
    /// </summary>
    public class PdfUserCancelledException : PdfUnmanagedException
    {
        internal static PdfUserCancelledException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_USER_CANCELLED;
            return new PdfUserCancelledException(message, code);
        }

        private PdfUserCancelledException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    /// <summary>
    /// Represents error during decompression
    /// </summary>
    public class PdfZlibDataException : PdfUnmanagedException
    {
        internal static PdfZlibDataException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_ZLIB_DATA;
            return new PdfZlibDataException(message, code);
        }

        private PdfZlibDataException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    /// <summary>
    /// The license file presented is not valid
    /// </summary>
    public class PdfInvalidLicenseException : PdfUnmanagedException
    {
        internal static PdfInvalidLicenseException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_INVALID_LICENSE;
            return new PdfInvalidLicenseException(message, code);
        }

        private PdfInvalidLicenseException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    /// <summary>
    /// The license file is required to access premium functionality
    /// </summary>
    public class PdfLicenseRequiredException : PdfUnmanagedException
    {
        internal static PdfLicenseRequiredException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_LICENSE_REQUIRED;
            return new PdfLicenseRequiredException(message, code);
        }

        private PdfLicenseRequiredException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    /// <summary>
    /// General error state that cannot be precisely defined
    /// </summary>
    public class PdfGeneralException : PdfUnmanagedException
    {
        internal static PdfGeneralException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_GENERAL;
            return new PdfGeneralException(message, code);
        }

        private PdfGeneralException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    /// <summary>
    /// Trying to access incorrect object type
    /// </summary>
    public class PdfConversionException : PdfUnmanagedException
    {
        internal static PdfConversionException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_CONVERSION;
            return new PdfConversionException(message, code);
        }

        private PdfConversionException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    /// <summary>
    /// Trying to access already released file
    /// </summary>
    public class PdfFileDisposedException : PdfUnmanagedException
    {
        internal static PdfFileDisposedException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_FILE_DISPOSED;
            return new PdfFileDisposedException(message, code);
        }

        private PdfFileDisposedException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    /// <summary>
    /// Accessing file properties before it was properly initialized
    /// @see \ref PdfSyntax.PdfFile.Initialize
    /// </summary>
    public class PdfFileNotInitializedException : PdfUnmanagedException
    {
        internal static PdfFileNotInitializedException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_FILE_NOT_INITIALIZED;
            return new PdfFileNotInitializedException(message, code);
        }

        private PdfFileNotInitializedException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    /// <summary>
    /// Trying to access object which is not present in the structure
    /// </summary>
    public class PdfObjectMissingException : PdfUnmanagedException
    {
        internal static PdfObjectMissingException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_OBJECT_MISSING;
            return new PdfObjectMissingException(message, code);
        }

        private PdfObjectMissingException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    /// <summary>
    /// Low-level parsing error
    /// </summary>
    public class PdfParseException : PdfUnmanagedException
    {
        internal static PdfParseException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_PARSE_EXCEPTION;
            return new PdfParseException(message, code);
        }

        private PdfParseException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    /// <summary>
    /// Invalid password was used to decode encrypted file
    /// </summary>
    public class PdfInvalidPasswordException : PdfUnmanagedException
    {
        internal static PdfInvalidPasswordException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_INVALID_PASSWORD;
            return new PdfInvalidPasswordException(message, code);
        }

        private PdfInvalidPasswordException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }
}
