using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// The Unicode standard defines a system for numbering all
    /// of the common characters used in a large number of languages.
    /// </summary>
    public class PdfUnicodeCharacterMap : PdfCharacterMap
    {
        internal PdfUnicodeCharacterMapSafeHandle Handle { get; }

        internal PdfUnicodeCharacterMap(PdfUnicodeCharacterMapSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get unicode representation of key
        /// </summary>
        /// <param name="key">Handle to \ref PdfBuffer containing the data to be searched in the dictionary</param>
        /// <returns>A new instance of \ref PdfBuffer if the has mapping inside the dictionary, throws exception on failure</returns>
        public PdfBuffer GetMappedValue(PdfBuffer key)
        {
            UInt32 result = NativeMethods.UnicodeCharacterMap_GetMappedValue(Handle, key.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        public override void Dispose()
        {
            base.Dispose();
            Handle?.Dispose();
        }
    }
}
