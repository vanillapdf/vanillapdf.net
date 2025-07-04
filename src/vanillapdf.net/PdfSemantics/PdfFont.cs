﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
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
        /// Create instance of PdfFont from associated PdfDictionaryObject
        /// </summary>
        /// <param name="dictionary">Backend PdfDictionaryObject containing the data for PdfFont</param>
        /// <returns>New instance of PdfFont on success, throws exception on failure</returns>
        public static PdfFont CreateFromObject(PdfDictionaryObject dictionary)
        {
            UInt32 result = NativeMethods.Font_CreateFromObject(dictionary.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFont(data);
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

        /// <summary>
        /// A stream containing a CMap file that maps character codes to Unicode values
        /// (see 9.10, "Extraction of Text Content").
        /// </summary>
        /// <returns>New instance of PdfUnicodeCharacterMap on success, throws exception on failure</returns>
        public PdfUnicodeCharacterMap GetUnicodeMap()
        {
            UInt32 result = NativeMethods.Font_GetUnicodeMap(FontHandle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfUnicodeCharacterMap(data);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            FontHandle?.Dispose();
        }

        private static class NativeMethods
        {
            public static CreateFromObjectDelgate Font_CreateFromObject = LibraryInstance.GetFunction<CreateFromObjectDelgate>("Font_CreateFromObject");
            public static GetFontTypeDelgate Font_GetFontType = LibraryInstance.GetFunction<GetFontTypeDelgate>("Font_GetFontType");
            public static GetUnicodeMapDelgate Font_GetUnicodeMap = LibraryInstance.GetFunction<GetUnicodeMapDelgate>("Font_GetUnicodeMap");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromObjectDelgate(PdfDictionaryObjectSafeHandle handle, out PdfFontSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetFontTypeDelgate(PdfFontSafeHandle handle, out Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetUnicodeMapDelgate(PdfFontSafeHandle handle, out PdfUnicodeCharacterMapSafeHandle data);
        }
    }
}
