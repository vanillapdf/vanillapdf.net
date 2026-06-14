namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Encoding types for PDF text strings (PDF spec 7.9.2.2).
    /// </summary>
    public enum PdfTextStringEncodingType
    {
        /// <summary>Undefined, uninitialized default.</summary>
        Undefined = 0,

        /// <summary>
        /// Single-byte encoding defined in PDF spec Table D.2.
        /// No byte order mark is present.
        /// </summary>
        PDFDocEncoding = 1,

        /// <summary>
        /// UTF-16 big-endian encoding.
        /// Indicated by byte order mark 0xFEFF at the start of the string.
        /// </summary>
        UTF16BE = 2,

        /// <summary>
        /// UTF-8 encoding (PDF 2.0).
        /// Indicated by byte order mark 0xEFBBBF at the start of the string.
        /// </summary>
        UTF8 = 3
    }
}
