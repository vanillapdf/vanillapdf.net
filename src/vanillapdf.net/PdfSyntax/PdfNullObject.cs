using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// The null object has a type and value that are unequal to those of any other object
    /// </summary>
    public class PdfNullObject : PdfObject
    {
        internal PdfNullObjectSafeHandle Handle { get; }

        internal PdfNullObject(PdfNullObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a new instance of \ref PdfNullObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfNullObject on success, throws exception on failure</returns>
        public static PdfNullObject Create()
        {
            UInt32 result = NativeMethods.NullObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNullObject(data);
        }

        /// <summary>
        /// Convert object to null object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfNullObject if the object can be converted, throws exception on failure</returns>
        public static PdfNullObject FromObject(PdfObject data)
        {
            return new PdfNullObject(data.ObjectHandle);
        }

        /// <summary>
        /// Try to convert object to null object, returning null if type doesn't match.
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfNullObject if the object is null type, null otherwise</returns>
        public static PdfNullObject TryFromObject(PdfObject data)
        {
            if (data.GetObjectType() != PdfObjectType.Null) {
                return null;
            }
            return new PdfNullObject(data.ObjectHandle);
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
