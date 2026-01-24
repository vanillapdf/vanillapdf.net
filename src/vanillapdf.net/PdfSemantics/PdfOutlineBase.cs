using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Base class for document outlines
    /// </summary>
    public class PdfOutlineBase : PdfUnknown
    {
        internal PdfOutlineBaseSafeHandle OutlineBaseHandle { get; }

        internal PdfOutlineBase(PdfOutlineBaseSafeHandle handle) : base(handle)
        {
            OutlineBaseHandle = handle;
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfOutlineType GetOutlineType()
        {
            UInt32 result = NativeMethods.OutlineBase_GetOutlineType(OutlineBaseHandle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfOutlineType>.CheckedCast(data);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            OutlineBaseHandle?.Dispose();
        }
    }
}
