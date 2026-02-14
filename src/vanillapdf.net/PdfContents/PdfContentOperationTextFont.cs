using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Set the text font and the text font size.
    /// </summary>
    public class PdfContentOperationTextFont: PdfContentOperation
    {
        internal PdfContentOperationTextFontSafeHandle Handle { get; }

        internal PdfContentOperationTextFont(PdfContentOperationTextFontSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Font name
        /// </summary>
        public PdfNameObject Name
        {
            get { return GetName(); }
            set { SetName(value); }
        }

        /// <summary>
        /// Font scale
        /// </summary>
        public PdfRealObject Scale
        {
            get { return GetScale(); }
            set { SetScale(value); }
        }

        private PdfNameObject GetName()
        {
            UInt32 result = NativeMethods.ContentOperationTextFont_GetName(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(value);
        }

        private void SetName(PdfNameObject value)
        {
            UInt32 result = NativeMethods.ContentOperationTextFont_SetName(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfRealObject GetScale()
        {
            UInt32 result = NativeMethods.ContentOperationTextFont_GetScale(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfRealObject(value);
        }

        private void SetScale(PdfRealObject value)
        {
            UInt32 result = NativeMethods.ContentOperationTextFont_SetScale(Handle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Convert content operation to text font operation
        /// </summary>
        /// <param name="data">Handle to \ref PdfContentOperation to be converted</param>
        /// <returns>A new instance of \ref PdfContentOperationTextFont if the object can be converted, throws exception on failure</returns>
        public static PdfContentOperationTextFont FromContentOperation(PdfContentOperation data)
        {
            return new PdfContentOperationTextFont(data.OperationHandle);
        }

        /// <inheritdoc/>

        public override void Dispose()
        {
            base.Dispose();
            Handle?.Dispose();
        }
    }
}
