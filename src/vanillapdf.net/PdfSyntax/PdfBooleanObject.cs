using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Boolean objects represent the logical values of true and false
    /// </summary>
    public class PdfBooleanObject : PdfObject
    {
        internal PdfBooleanObjectSafeHandle Handle { get; }

        internal PdfBooleanObject(PdfBooleanObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Currently stored boolean value
        /// </summary>
        public bool Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfBooleanObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfBooleanObject on success, throws exception on failure</returns>
        public static PdfBooleanObject Create()
        {
            UInt32 result = NativeMethods.BooleanObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBooleanObject(data);
        }

        private bool GetValue()
        {
            UInt32 result = NativeMethods.BooleanObject_GetValue(Handle, out bool data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetValue(bool data)
        {
            UInt32 result = NativeMethods.BooleanObject_SetValue(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Custom conversion to bool
        /// </summary>
        /// <param name="obj">Handle to object to be converted</param>
        public static implicit operator bool(PdfBooleanObject obj)
        {
            return obj.Value;
        }

        /// <summary>
        /// Convert object to boolean object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfBooleanObject if the object can be converted, throws exception on failure</returns>
        public static PdfBooleanObject FromObject(PdfObject data)
        {
            return new PdfBooleanObject(data.ObjectHandle);
        }

        /// <summary>
        /// Try to convert object to boolean object, returning null if type doesn't match.
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfBooleanObject if the object is a boolean, null otherwise</returns>
        public static PdfBooleanObject TryFromObject(PdfObject data)
        {
            if (data.GetObjectType() != PdfObjectType.Boolean) {
                return null;
            }

            return new PdfBooleanObject(data.ObjectHandle);
        }

        /// <inheritdoc/>

        public override void Dispose()
        {
            base.Dispose();
            Handle?.Dispose();
        }
    }
}
