using System;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    /// <summary>
    /// Base exception class for all unmanaged derived exceptions
    /// </summary>
    public abstract class PdfUnmanagedException : PdfBaseException
    {
        public UInt32 ErrorCode { get; protected set; }

        public PdfUnmanagedException(string message, UInt32 errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public static PdfUnmanagedException GetException(uint value, string errorMessage = null)
        {
            if (value == PdfReturnValues.ERROR_PARAMETER_VALUE) {
                return PdfParameterValueException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_NOT_SUPPORTED) {
                return PdfNotSupportedException.Create(errorMessage);
            }

            if (value == PdfReturnValues.USER_CANCELLED) {
                return PdfUserCancelledException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ZLIB_DATA) {
                return PdfZlibDataException.Create(errorMessage);
            }

            if (value == PdfReturnValues.INVALID_LICENSE) {
                return PdfInvalidLicenseException.Create(errorMessage);
            }

            if (value == PdfReturnValues.LICENSE_REQUIRED) {
                return PdfLicenseRequiredException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_GENERAL) {
                return PdfGeneralException.Create(errorMessage);
            }

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

            if (value == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return PdfObjectMissingException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_PARSE_EXCEPTION) {
                return PdfParseException.Create(errorMessage);
            }

            if (value == PdfReturnValues.ERROR_INVALID_PASSWORD) {
                return PdfInvalidPasswordException.Create(errorMessage);
            }

            throw new Exception("Unknown return value");
        }
    }

    public class PdfParameterValueException : PdfUnmanagedException
    {
        public static PdfParameterValueException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_PARAMETER_VALUE;
            return new PdfParameterValueException(message, code);
        }

        private PdfParameterValueException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    public class PdfNotSupportedException : PdfUnmanagedException
    {
        public static PdfNotSupportedException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_NOT_SUPPORTED;
            return new PdfNotSupportedException(message, code);
        }

        private PdfNotSupportedException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    public class PdfUserCancelledException : PdfUnmanagedException
    {
        public static PdfUserCancelledException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_NOT_SUPPORTED;
            return new PdfUserCancelledException(message, code);
        }

        private PdfUserCancelledException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    public class PdfZlibDataException : PdfUnmanagedException
    {
        public static PdfZlibDataException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_NOT_SUPPORTED;
            return new PdfZlibDataException(message, code);
        }

        private PdfZlibDataException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    public class PdfInvalidLicenseException : PdfUnmanagedException
    {
        public static PdfInvalidLicenseException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_NOT_SUPPORTED;
            return new PdfInvalidLicenseException(message, code);
        }

        private PdfInvalidLicenseException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    public class PdfLicenseRequiredException : PdfUnmanagedException
    {
        public static PdfLicenseRequiredException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_NOT_SUPPORTED;
            return new PdfLicenseRequiredException(message, code);
        }

        private PdfLicenseRequiredException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    public class PdfGeneralException : PdfUnmanagedException
    {
        public static PdfGeneralException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_GENERAL;
            return new PdfGeneralException(message, code);
        }

        private PdfGeneralException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    public class PdfConversionException : PdfUnmanagedException
    {
        public static PdfConversionException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_CONVERSION;
            return new PdfConversionException(message, code);
        }

        private PdfConversionException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    public class PdfFileDisposedException : PdfUnmanagedException
    {
        public static PdfFileDisposedException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_FILE_DISPOSED;
            return new PdfFileDisposedException(message, code);
        }

        private PdfFileDisposedException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    public class PdfFileNotInitializedException : PdfUnmanagedException
    {
        public static PdfFileNotInitializedException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_FILE_NOT_INITIALIZED;
            return new PdfFileNotInitializedException(message, code);
        }

        private PdfFileNotInitializedException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    public class PdfObjectMissingException : PdfUnmanagedException
    {
        public static PdfObjectMissingException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_OBJECT_MISSING;
            return new PdfObjectMissingException(message, code);
        }

        private PdfObjectMissingException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    public class PdfParseException : PdfUnmanagedException
    {
        public static PdfParseException Create(string message)
        {
            UInt32 code = PdfReturnValues.ERROR_OBJECT_MISSING;
            return new PdfParseException(message, code);
        }

        private PdfParseException(string message, UInt32 errorCode)
            : base(message, errorCode)
        {
        }
    }

    public class PdfInvalidPasswordException : PdfUnmanagedException
    {
        public static PdfInvalidPasswordException Create(string message)
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
