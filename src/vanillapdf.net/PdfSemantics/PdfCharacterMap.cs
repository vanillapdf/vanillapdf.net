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
    public class PdfCharacterMap : PdfUnknown
    {
        internal PdfCharacterMap(PdfCharacterMapSafeHandle handle) : base(handle)
        {
        }

        static PdfCharacterMap()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfCharacterMapSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfCharacterMapType GetCharacterMapType()
        {
            UInt32 result = NativeMethods.CharacterMap_GetCharacterMapType(Handle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfCharacterMapType>.CheckedCast(data);
        }

        private static class NativeMethods
        {
            public static GetCharacterMapTypeDelgate CharacterMap_GetCharacterMapType = LibraryInstance.GetFunction<GetCharacterMapTypeDelgate>("CharacterMap_GetCharacterMapType");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetCharacterMapTypeDelgate(PdfCharacterMapSafeHandle handle, out Int32 data);
        }
    }
}
