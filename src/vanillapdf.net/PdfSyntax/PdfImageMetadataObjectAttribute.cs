using System.Runtime.CompilerServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Attribute object that contains information about image colorspace and components
    /// </summary>
    public class PdfImageMetadataObjectAttribute : PdfBaseObjectAttribute
    {
        internal PdfImageMetadataObjectAttributeSafeHandle Handle { get; }

        internal PdfImageMetadataObjectAttribute(PdfImageMetadataObjectAttributeSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfImageMetadataObjectAttribute()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfImageMetadataObjectAttributeSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Convert base attribute to image metadata attribute
        /// </summary>
        /// <param name="data">Handle to PdfBaseObjectAttribute to be converted</param>
        /// <returns>A new instance of PdfImageMetadataObjectAttribute if the object can be converted, throws exception on failure</returns>
        public static PdfImageMetadataObjectAttribute FromBaseAttribute(PdfBaseObjectAttribute data)
        {
            return new PdfImageMetadataObjectAttribute(data.BaseAttributeHandle);
        }

        #region PdfUnknown

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #endregion

        private static class NativeMethods
        {
        }
    }
}
