using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Stream object represents compressed data inside document
    /// </summary>
    public class PdfStreamObject : PdfObject
    {
        internal PdfStreamObjectSafeHandle Handle { get; }

        internal PdfStreamObject(PdfStreamObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Stream object's header dictionary
        /// </summary>
        public PdfDictionaryObject Header
        {
            get { return GetHeader(); }
            set { SetHeader(value); }
        }

        /// <summary>
        /// Binary data content of the stream's body
        /// </summary>
        public PdfBuffer Body
        {
            get { return GetBody(); }
            set { SetBody(value); }
        }

        /// <summary>
        /// Binary data content of the stream's body in the raw compressed format
        /// </summary>
        public PdfBuffer BodyRaw
        {
            get { return GetBodyRaw(); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfStreamObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfStreamObject on success, throws exception on failure</returns>
        public static PdfStreamObject Create()
        {
            UInt32 result = NativeMethods.StreamObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfStreamObject(data);
        }

        private PdfDictionaryObject GetHeader()
        {
            UInt32 result = NativeMethods.StreamObject_GetHeader(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObject(value);
        }

        private void SetHeader(PdfDictionaryObject data)
        {
            UInt32 result = NativeMethods.StreamObject_SetHeader(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfBuffer GetBodyRaw()
        {
            UInt32 result = NativeMethods.StreamObject_GetBodyRaw(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(value);
        }

        private PdfBuffer GetBody()
        {
            UInt32 result = NativeMethods.StreamObject_GetBody(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(value);
        }

        private void SetBody(PdfBuffer data)
        {
            UInt32 result = NativeMethods.StreamObject_SetBody(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Convert object to stream object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfStreamObject if the object can be converted, throws exception on failure</returns>
        public static PdfStreamObject FromObject(PdfObject data)
        {
            // This optimization does have severe side-effects and it's not worth it
            //if (data is PdfStreamObject pdfStreamObject) {
            //    return pdfStreamObject;
            //}

            return new PdfStreamObject(data.ObjectHandle);
        }

        #region PdfUnknown

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #endregion
    }
}
