using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
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

        /// <summary>
        /// Number of color components (1 for GREY, 3 for RGB, 4 for CMYK)
        /// </summary>
        public int ColorComponents
        {
            get { return GetColorComponents(); }
            set { SetColorComponents(value); }
        }

        /// <summary>
        /// Color space of the associated image
        /// </summary>
        public PdfImageColorSpaceType ColorSpace
        {
            get { return GetColorSpace(); }
            set { SetColorSpace(value); }
        }

        /// <summary>
        /// Width of the image in pixels
        /// </summary>
        public int Width
        {
            get { return GetWidth(); }
            set { SetWidth(value); }
        }

        /// <summary>
        /// Height of the image in pixels
        /// </summary>
        public int Height
        {
            get { return GetHeight(); }
            set { SetHeight(value); }
        }

        /// <summary>
        /// Create a new instance of PdfImageMetadataObjectAttribute
        /// </summary>
        /// <returns>New instance of \ref PdfImageMetadataObjectAttribute on success, throws exception on failure</returns>
        public static PdfImageMetadataObjectAttribute Create()
        {
            UInt32 result = NativeMethods.ImageMetadataObjectAttribute_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfImageMetadataObjectAttribute(data);
        }

        private int GetColorComponents()
        {
            UInt32 result = NativeMethods.ImageMetadataObjectAttribute_GetColorComponents(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetColorComponents(int value)
        {
            UInt32 result = NativeMethods.ImageMetadataObjectAttribute_SetColorComponents(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfImageColorSpaceType GetColorSpace()
        {
            UInt32 result = NativeMethods.ImageMetadataObjectAttribute_GetColorSpace(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfImageColorSpaceType>.CheckedCast(data);
        }

        private void SetColorSpace(PdfImageColorSpaceType value)
        {
            UInt32 result = NativeMethods.ImageMetadataObjectAttribute_SetColorSpace(Handle, (int)value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private int GetWidth()
        {
            UInt32 result = NativeMethods.ImageMetadataObjectAttribute_GetWidth(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetWidth(int value)
        {
            UInt32 result = NativeMethods.ImageMetadataObjectAttribute_SetWidth(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private int GetHeight()
        {
            UInt32 result = NativeMethods.ImageMetadataObjectAttribute_GetHeight(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetHeight(int value)
        {
            UInt32 result = NativeMethods.ImageMetadataObjectAttribute_SetHeight(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
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
    }
}
