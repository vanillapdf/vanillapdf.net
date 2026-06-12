using System;
using vanillapdf.net.Interop;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Utilities for working with PDF text string encodings (PDF spec 7.9.2.2).
    /// </summary>
    public static class PdfTextStringEncoding
    {
        /// <summary>
        /// Detect the encoding of raw text string data by inspecting its byte order mark.
        /// Data without a byte order mark is reported as <see cref="PdfTextStringEncodingType.PDFDocEncoding"/>.
        /// </summary>
        /// <param name="data">Raw bytes of the text string to inspect.</param>
        /// <returns>The detected <see cref="PdfTextStringEncodingType"/>.</returns>
        public static PdfTextStringEncodingType Detect(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            UInt32 result = NativeMethods.TextStringEncoding_Detect(data, new UIntPtr((uint)data.Length), out Int32 value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfTextStringEncodingType>.CheckedCast(value);
        }

        /// <summary>
        /// Convert a single PDFDocEncoding byte (PDF spec Table D.2) to its Unicode code point.
        /// </summary>
        /// <param name="value">The PDFDocEncoding byte to convert.</param>
        /// <returns>The Unicode code point the byte maps to.</returns>
        public static UInt32 PDFDocEncodingByteToUnicode(byte value)
        {
            UInt32 result = NativeMethods.TextStringEncoding_PDFDocEncodingByteToUnicode(value, out UInt32 codepoint);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return codepoint;
        }
    }
}
