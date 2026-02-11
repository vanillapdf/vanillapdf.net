using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A CMap shall specify the mapping from  character codes to character selectors.
    /// </summary>
    public class PdfCharacterMap : IDisposable
    {
        internal PdfCharacterMapSafeHandle CharacterMapHandle { get; }

        internal PdfCharacterMap(PdfCharacterMapSafeHandle handle)
        {
            CharacterMapHandle = handle;
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

        public virtual void Dispose()
        {
            CharacterMapHandle?.Dispose();
        }
    }
}
