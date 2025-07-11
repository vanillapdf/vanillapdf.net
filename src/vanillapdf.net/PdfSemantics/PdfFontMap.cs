﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A dictionary that maps resource names to font dictionaries.
    /// </summary>
    public class PdfFontMap : PdfUnknown
    {
        internal PdfFontMapSafeHandle Handle { get; }

        internal PdfFontMap(PdfFontMapSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfFontMap()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfFontMapSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Determine whether a font with the given name exists in the map.
        /// </summary>
        /// <param name="key">Name of the font resource.</param>
        /// <returns><c>true</c> if the font is present; otherwise <c>false</c>.</returns>
        public bool Contains(PdfNameObject key)
        {
            UInt32 result = NativeMethods.FontMap_Contains(Handle, key.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Retrieve the font associated with the specified name.
        /// </summary>
        /// <param name="key">Name of the font resource.</param>
        /// <returns>A <see cref="PdfFont"/> instance when found.</returns>
        public PdfFont Find(PdfNameObject key)
        {
            UInt32 result = NativeMethods.FontMap_Find(Handle, key.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfFont(data);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static ContainsDelgate FontMap_Contains = LibraryInstance.GetFunction<ContainsDelgate>("FontMap_Contains");
            public static FindDelgate FontMap_Find = LibraryInstance.GetFunction<FindDelgate>("FontMap_Find");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ContainsDelgate(PdfFontMapSafeHandle handle, PdfNameObjectSafeHandle key, out bool data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FindDelgate(PdfFontMapSafeHandle handle, PdfNameObjectSafeHandle key, out PdfFontSafeHandle data);
        }
    }
}
