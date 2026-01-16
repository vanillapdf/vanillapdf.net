using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Base class for representing object attributes that are augumenting specific properties with additional metadata
    /// </summary>
    public class PdfBaseObjectAttribute : PdfUnknown
    {
        internal PdfBaseObjectAttributeSafeHandle BaseAttributeHandle { get; }

        internal PdfBaseObjectAttribute(PdfBaseObjectAttributeSafeHandle handle) : base(handle)
        {
            BaseAttributeHandle = handle;
        }

        /// <summary>
        /// Get derived type of current object attribute
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfObjectAttributeType GetObjectAttributeType()
        {
            UInt32 result = NativeMethods.BaseObjectAttribute_GetAttributeType(BaseAttributeHandle, out PdfObjectAttributeType data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfObjectAttributeType>.CheckedCast(data);
        }

        #region PdfUnknown

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            BaseAttributeHandle?.Dispose();
        }

        #endregion
    }
}
