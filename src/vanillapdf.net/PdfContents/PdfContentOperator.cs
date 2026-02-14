using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// An operator is a PDF keyword specifying some action that shall be performed, such as painting a graphical shape on the page.
    /// </summary>
    public class PdfContentOperator : IDisposable
    {
        internal PdfContentOperatorSafeHandle Handle { get; }

        internal PdfContentOperator(PdfContentOperatorSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfContentOperatorType GetOperatorType()
        {
            UInt32 result = NativeMethods.ContentOperator_GetOperatorType(Handle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfContentOperatorType>.CheckedCast(data);
        }

        /// <summary>
        /// Get data buffer containing text representation of the content operator
        /// </summary>
        /// <returns>Handle to a data buffer on success, throws exception on failure</returns>
        public PdfBuffer GetValue()
        {
            UInt32 result = NativeMethods.ContentOperator_GetValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(value);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var operatorType = GetOperatorType();
            return operatorType.ToString();
        }

        /// <inheritdoc/>

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
