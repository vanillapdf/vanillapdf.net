namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Color space types for PDF colors
    /// </summary>
    public enum PdfColorSpaceType
    {
        /// <summary>Transparent (no color)</summary>
        Transparent = 0,

        /// <summary>DeviceGray color space (1 component)</summary>
        DeviceGray,

        /// <summary>DeviceRGB color space (3 components)</summary>
        DeviceRGB,

        /// <summary>DeviceCMYK color space (4 components)</summary>
        DeviceCMYK,
    };
}
