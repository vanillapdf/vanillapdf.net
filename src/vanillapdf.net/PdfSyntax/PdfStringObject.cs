using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Reprsents human readable text
    /// </summary>
    public class PdfStringObject : PdfObject
    {
        internal PdfStringObjectSafeHandle StringHandle { get; }

        internal PdfStringObject(PdfStringObjectSafeHandle handle) : base(handle)
        {
            StringHandle = handle;
        }

        /// <summary>
        /// Get value of underlying data buffer
        /// </summary>
        public PdfBuffer Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfStringType GetStringType()
        {
            UInt32 result = NativeMethods.StringObject_GetStringType(StringHandle, out PdfStringType data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfStringType>.CheckedCast(data);
        }

        private PdfBuffer GetValue()
        {
            UInt32 result = NativeMethods.StringObject_GetValue(StringHandle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(value);
        }

        private void SetValue(PdfBuffer value)
        {
            UInt32 result = NativeMethods.StringObject_SetValue(StringHandle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Convert object to string object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfStringObject if the object can be converted, throws exception on failure</returns>
        public static PdfStringObject FromObject(PdfObject data)
        {
            return new PdfStringObject(data.ObjectHandle);
        }

        /// <summary>
        /// Try to convert object to string object, returning null if type doesn't match.
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfStringObject if the object is a string, null otherwise</returns>
        public static PdfStringObject TryFromObject(PdfObject data)
        {
            if (data.GetObjectType() != PdfObjectType.String) {
                return null;
            }
            return new PdfStringObject(data.ObjectHandle);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            StringHandle?.Dispose();
        }
    }
}
