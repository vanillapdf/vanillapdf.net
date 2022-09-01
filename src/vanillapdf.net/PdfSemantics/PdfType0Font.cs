using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A font composed of glyphs from a descendant CIDFont.
    /// </summary>
    public class PdfType0Font : PdfFont
    {
        internal PdfType0FontSafeHandle Handle { get; }

        internal PdfType0Font(PdfType0FontSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfType0Font()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfType0FontSafeHandle).TypeHandle);
        }

        public PdfUnicodeCharacterMap GetUnicodeMap()
        {
            UInt32 result = NativeMethods.Type0Font_GetUnicodeMap(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfUnicodeCharacterMap(data);
        }

        /// <summary>
        /// Convert font to Type0 font object
        /// </summary>
        /// <param name="data">Handle to \ref PdfFont to be converted</param>
        /// <returns>A new instance of \ref PdfType0Font if the object can be converted, throws exception on failure</returns>
        public static PdfType0Font FromFont(PdfFont data)
        {
            return new PdfType0Font(data.FontHandle);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static GetUnicodeMapDelgate Type0Font_GetUnicodeMap = LibraryInstance.GetFunction<GetUnicodeMapDelgate>("Type0Font_GetUnicodeMap");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetUnicodeMapDelgate(PdfType0FontSafeHandle handle, out PdfUnicodeCharacterMapSafeHandle data);
        }
    }
}
