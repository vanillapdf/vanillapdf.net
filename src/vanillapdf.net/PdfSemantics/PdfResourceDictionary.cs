using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A content stream's named resources shall be defined
    /// by a resource dictionary, which shall enumerate the
    /// named resources needed by the operators in the
    /// content stream and the names by which they can be referred to.
    /// </summary>
    public class PdfResourceDictionary : PdfUnknown
    {
        internal PdfResourceDictionarySafeHandle Handle { get; }

        internal PdfResourceDictionary(PdfResourceDictionarySafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// A dictionary that maps resource names to font dictionaries.
        /// </summary>
        /// <returns>Handle to \ref PdfFontMap object on success, throws exception on failure</returns>
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

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }
    }
}
