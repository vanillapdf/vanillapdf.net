using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// As an alternative to the image XObjects a sampled image may be
    /// specified in the form of an inline image.
    /// </summary>
    public class PdfContentObjectInlineImage : PdfContentObject
    {
        internal PdfContentObjectInlineImageSafeHandle Handle { get; }

        internal PdfContentObjectInlineImage(PdfContentObjectInlineImageSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// The meta-data dictionary describing the inline image.
        /// </summary>
        public PdfDictionaryObject Dictionary
        {
            get => GetDictionary();
        }

        /// <summary>
        /// The raw image data of the inline image.
        /// </summary>
        public PdfBuffer Data
        {
            get => GetData();
        }

        /// <summary>
        /// Convert content object to inline image object
        /// </summary>
        /// <param name="data">Handle to \ref PdfContentObject to be converted</param>
        /// <returns>A new instance of \ref PdfContentObjectInlineImage if the object can be converted, throws exception on failure</returns>
        public static PdfContentObjectInlineImage FromContentObject(PdfContentObject data)
        {
            return new PdfContentObjectInlineImage(data.ObjectHandle);
        }

        private PdfDictionaryObject GetDictionary()
        {
            UInt32 result = NativeMethods.ContentObjectInlineImage_GetDictionary(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDictionaryObject(data);
        }

        private PdfBuffer GetData()
        {
            UInt32 result = NativeMethods.ContentObjectInlineImage_GetData(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        /// <inheritdoc/>

        public override void Dispose()
        {
            base.Dispose();
            Handle?.Dispose();
        }
    }
}
