using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Base class for all fonts in to representable inside a PDF document.
    /// </summary>
    public class PdfFont : PdfUnknown
    {
        internal PdfFontSafeHandle FontHandle { get; }

        internal PdfFont(PdfFontSafeHandle handle) : base(handle)
        {
            FontHandle = handle;
        }

        static PdfFont()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfFontSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfFontType GetFontType()
        {
            UInt32 result = NativeMethods.Font_GetFontType(FontHandle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfFontType>.CheckedCast(data);
        }

        protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            FontHandle?.Dispose();
        }

        private static class NativeMethods
        {
            public static GetFontTypeDelgate Font_GetFontType = LibraryInstance.GetFunction<GetFontTypeDelgate>("Font_GetFontType");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetFontTypeDelgate(PdfFontSafeHandle handle, out Int32 data);
        }
    }
}
