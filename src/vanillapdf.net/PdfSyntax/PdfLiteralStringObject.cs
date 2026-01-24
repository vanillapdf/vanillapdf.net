using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// A literal string is preferable for printable data
    /// </summary>
    public class PdfLiteralStringObject : PdfStringObject
    {
        internal PdfLiteralStringObjectSafeHandle Handle { get; }

        internal PdfLiteralStringObject(PdfLiteralStringObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new instance of \ref PdfLiteralStringObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfLiteralStringObject on success, throws exception on failure</returns>
        public static PdfLiteralStringObject Create()
        {
            UInt32 result = NativeMethods.LiteralStringObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        /// <summary>
        /// Create a new instance of \ref PdfLiteralStringObject from specified data in PDF format
        /// </summary>
        /// <param name="value">Handle to \ref PdfBuffer containing data in PDF format</param>
        /// <returns>New instance of \ref PdfLiteralStringObject on success, throws exception on failure</returns>
        public static PdfLiteralStringObject CreateFromEncodedBuffer(PdfBuffer value)
        {
            UInt32 result = NativeMethods.LiteralStringObject_CreateFromEncodedBuffer(value.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        /// <summary>
        /// Create a new instance of \ref PdfLiteralStringObject from specified data in PDF format
        /// </summary>
        /// <param name="value">A string containing data in PDF format</param>
        /// <returns>New instance of \ref PdfLiteralStringObject on success, throws exception on failure</returns>
        public static PdfLiteralStringObject CreateFromEncodedString(string value)
        {
            UInt32 result = NativeMethods.LiteralStringObject_CreateFromEncodedString(value, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        /// <summary>
        /// Create a new instance of \ref PdfLiteralStringObject from specified data in PDF format
        /// </summary>
        /// <param name="value">Handle to \ref PdfBuffer containing raw data after PDF parsing</param>
        /// <returns>New instance of \ref PdfLiteralStringObject on success, throws exception on failure</returns>
        public static PdfLiteralStringObject CreateFromDecodedBuffer(PdfBuffer value)
        {
            UInt32 result = NativeMethods.LiteralStringObject_CreateFromDecodedBuffer(value.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        /// <summary>
        /// Create a new instance of \ref PdfLiteralStringObject from specified data in PDF format
        /// </summary>
        /// <param name="value">A string containing raw data after PDF parsing</param>
        /// <returns>New instance of \ref PdfLiteralStringObject on success, throws exception on failure</returns>
        public static PdfLiteralStringObject CreateFromDecodedString(string value)
        {
            UInt32 result = NativeMethods.LiteralStringObject_CreateFromDecodedString(value, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfLiteralStringObject(data);
        }

        /// <summary>
        /// Implicit conversion from <see cref="string"/> to <see cref="PdfLiteralStringObject"/>.
        /// </summary>
        public static implicit operator PdfLiteralStringObject(string value)
        {
            return CreateFromDecodedString(value);
        }

        internal override PdfObject ConvertTo<T>()
        {
            if (typeof(T) == typeof(PdfLiteralStringObject)) {
                return this;
            }

            return base.ConvertTo<T>();
        }

        /// <summary>
        /// Convert string object to literal string object
        /// </summary>
        /// <param name="data">Handle to \ref PdfStringObject to be converted</param>
        /// <returns>A new instance of \ref PdfLiteralStringObject if the object can be converted, throws exception on failure</returns>
        public static PdfLiteralStringObject FromString(PdfStringObject data)
        {
            // This optimization does have severe side-effects and it's not worth it
            //if (data is PdfLiteralStringObject pdfLiteralStringObject) {
            //    return pdfLiteralStringObject;
            //}

            return new PdfLiteralStringObject(data.StringHandle);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }
    }
}
