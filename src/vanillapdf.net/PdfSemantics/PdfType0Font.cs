using System;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A font composed of glyphs from a descendant CIDFont.
    /// </summary>
    public class PdfType0Font : PdfFont
    {
        internal PdfType0FontSafeHandle Handle { get; }

        internal PdfType0Font(PdfType0FontSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Convert font to Type0 font object
        /// </summary>
        /// <param name="data">Handle to \ref PdfFont to be converted</param>
        /// <returns>A new instance of \ref PdfType0Font if the object can be converted, throws exception on failure</returns>
        public static PdfType0Font FromFont(PdfFont data)
        {
            return new PdfType0Font(data.FontHandle);
        }

        /// <inheritdoc/>

        public override void Dispose()
        {
            base.Dispose();
            Handle?.Dispose();
        }
    }
}
