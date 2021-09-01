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
    public class PdfUnicodeCharacterMap : PdfCharacterMap
    {
        internal PdfUnicodeCharacterMap(PdfUnicodeCharacterMapSafeHandle handle) : base(handle)
        {
        }

        static PdfUnicodeCharacterMap()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfUnicodeCharacterMapSafeHandle).TypeHandle);
        }

        public PdfBuffer GetMappedValue(PdfBuffer key)
        {
            UInt32 result = NativeMethods.UnicodeCharacterMap_GetMappedValue(Handle, key.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        private static class NativeMethods
        {
            public static GetMappedValueDelgate UnicodeCharacterMap_GetMappedValue = LibraryInstance.GetFunction<GetMappedValueDelgate>("UnicodeCharacterMap_GetMappedValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetMappedValueDelgate(PdfUnicodeCharacterMapSafeHandle handle, PdfBufferSafeHandle key, out PdfBufferSafeHandle data);
        }
    }
}
