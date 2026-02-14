using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A dictionary that maps resource names to font dictionaries.
    /// </summary>
    public class PdfFontMap : IDisposable
    {
        internal PdfFontMapSafeHandle Handle { get; }

        internal PdfFontMap(PdfFontMapSafeHandle handle)
        {
            Handle = handle;
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

        /// <inheritdoc/>

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
