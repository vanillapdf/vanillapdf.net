using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Stream operation for showing a simple text string
    /// </summary>
    public class PdfContentOperationTextShow : PdfContentOperation
    {
        internal PdfContentOperationTextShowSafeHandle Handle { get; }

        internal PdfContentOperationTextShow(PdfContentOperationTextShowSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Text string to be displayed in the document
        /// </summary>
        public PdfStringObject Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        private PdfStringObject GetValue()
        {
            UInt32 result = NativeMethods.ContentOperationTextShow_GetValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStringObject(value);
        }

        private void SetValue(PdfStringObject value)
        {
            UInt32 result = NativeMethods.ContentOperationTextShow_SetValue(Handle, value.StringHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Convert content operation to text showing operation
        /// </summary>
        /// <param name="data">Handle to \ref PdfContentOperation to be converted</param>
        /// <returns>A new instance of \ref PdfContentOperationTextShow if the object can be converted, throws exception on failure</returns>
        public static PdfContentOperationTextShow FromContentOperation(PdfContentOperation data)
        {
            return new PdfContentOperationTextShow(data.OperationHandle);
        }

        public override void Dispose()
        {
            base.Dispose();
            Handle?.Dispose();
        }
    }
}
