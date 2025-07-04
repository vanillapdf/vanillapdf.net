﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A CMap shall specify the mapping from  character codes to character selectors.
    /// </summary>
    public class PdfCharacterMap : PdfUnknown
    {
        internal PdfCharacterMapSafeHandle CharacterMapHandle { get; }

        internal PdfCharacterMap(PdfCharacterMapSafeHandle handle) : base(handle)
        {
            CharacterMapHandle = handle;
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
            UInt32 result = NativeMethods.CharacterMap_GetCharacterMapType(CharacterMapHandle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfCharacterMapType>.CheckedCast(data);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            CharacterMapHandle?.Dispose();
        }

        private static class NativeMethods
        {
            public static GetCharacterMapTypeDelgate CharacterMap_GetCharacterMapType = LibraryInstance.GetFunction<GetCharacterMapTypeDelgate>("CharacterMap_GetCharacterMapType");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetCharacterMapTypeDelgate(PdfCharacterMapSafeHandle handle, out Int32 data);
        }
    }
}
