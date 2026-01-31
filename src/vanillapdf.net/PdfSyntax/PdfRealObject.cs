using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Real objects represent mathematical real numbers
    /// </summary>
    public class PdfRealObject : PdfObject
    {
        internal PdfRealObjectSafeHandle Handle { get; }

        internal PdfRealObject(PdfRealObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Currently stored floating point number
        /// </summary>
        public double Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfRealObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfRealObject</returns>
        public static PdfRealObject Create()
        {
            UInt32 result = NativeMethods.RealObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfRealObject(data);
        }

        private double GetValue()
        {
            UInt32 result = NativeMethods.RealObject_GetValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return value;
        }

        private void SetValue(double value)
        {
            UInt32 result = NativeMethods.RealObject_SetValue(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Custom conversion to double
        /// </summary>
        /// <param name="obj">Handle to object to be converted</param>
        public static implicit operator double(PdfRealObject obj)
        {
            return obj.Value;
        }

        /// <summary>
        /// Convert object to real object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfRealObject if the object can be converted, throws exception on failure</returns>
        public static PdfRealObject FromObject(PdfObject data)
        {
            return new PdfRealObject(data.ObjectHandle);
        }

        /// <summary>
        /// Try to convert object to real object, returning null if type doesn't match.
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfRealObject if the object is a real, null otherwise</returns>
        public static PdfRealObject TryFromObject(PdfObject data)
        {
            var objectType = data.GetObjectType();
            if (objectType != PdfObjectType.Real && objectType != PdfObjectType.Integer) {
                return null;
            }
            return new PdfRealObject(data.ObjectHandle);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }
    }
}
