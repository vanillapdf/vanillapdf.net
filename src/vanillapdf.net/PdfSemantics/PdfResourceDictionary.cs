using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// TODO
    /// </summary>
    public class PdfResourceDictionary : PdfUnknown
    {
        internal PdfResourceDictionary(PdfResourceDictionarySafeHandle handle) : base(handle)
        {
        }

        static PdfResourceDictionary()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfResourceDictionarySafeHandle).TypeHandle);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns>TODO</returns>
        public PdfFontMap GetFontMap()
        {
            UInt32 result = NativeMethods.ResourceDictionary_GetFontMap(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFontMap(data);
        }

        private static class NativeMethods
        {
            public static GetFontMapDelgate ResourceDictionary_GetFontMap = LibraryInstance.GetFunction<GetFontMapDelgate>("ResourceDictionary_GetFontMap");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetFontMapDelgate(PdfResourceDictionarySafeHandle handle, out PdfFontMapSafeHandle data);
        }
    }
}
