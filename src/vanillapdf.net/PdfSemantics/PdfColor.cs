using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents a color value for annotations and other PDF elements.
    /// </summary>
    public class PdfColor : IDisposable
    {
        internal PdfColorSafeHandle Handle { get; }

        internal PdfColor(PdfColorSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Create a transparent color (no color).
        /// </summary>
        public static PdfColor CreateTransparent()
        {
            UInt32 result = NativeMethods.Color_CreateTransparent(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfColor(data);
        }

        /// <summary>
        /// Create a grayscale color.
        /// </summary>
        /// <param name="gray">The gray component (0.0 to 1.0).</param>
        public static PdfColor CreateGray(double gray)
        {
            UInt32 result = NativeMethods.Color_CreateGray(gray, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfColor(data);
        }

        /// <summary>
        /// Create an RGB color.
        /// </summary>
        /// <param name="red">The red component (0.0 to 1.0).</param>
        /// <param name="green">The green component (0.0 to 1.0).</param>
        /// <param name="blue">The blue component (0.0 to 1.0).</param>
        public static PdfColor CreateRGB(double red, double green, double blue)
        {
            UInt32 result = NativeMethods.Color_CreateRGB(red, green, blue, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfColor(data);
        }

        /// <summary>
        /// Create a CMYK color.
        /// </summary>
        /// <param name="cyan">The cyan component (0.0 to 1.0).</param>
        /// <param name="magenta">The magenta component (0.0 to 1.0).</param>
        /// <param name="yellow">The yellow component (0.0 to 1.0).</param>
        /// <param name="black">The black component (0.0 to 1.0).</param>
        public static PdfColor CreateCMYK(double cyan, double magenta, double yellow, double black)
        {
            UInt32 result = NativeMethods.Color_CreateCMYK(cyan, magenta, yellow, black, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfColor(data);
        }

        /// <summary>
        /// Get the color space type.
        /// </summary>
        public PdfColorSpaceType ColorSpace
        {
            get
            {
                UInt32 result = NativeMethods.Color_GetColorSpace(Handle, out Int32 data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return EnumUtil<PdfColorSpaceType>.CheckedCast(data);
            }
        }

        /// <summary>
        /// Get the red component (for RGB colors).
        /// </summary>
        public double Red
        {
            get
            {
                UInt32 result = NativeMethods.Color_GetRed(Handle, out double data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return data;
            }
        }

        /// <summary>
        /// Get the green component (for RGB colors).
        /// </summary>
        public double Green
        {
            get
            {
                UInt32 result = NativeMethods.Color_GetGreen(Handle, out double data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return data;
            }
        }

        /// <summary>
        /// Get the blue component (for RGB colors).
        /// </summary>
        public double Blue
        {
            get
            {
                UInt32 result = NativeMethods.Color_GetBlue(Handle, out double data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return data;
            }
        }

        /// <summary>
        /// Get the gray component (for grayscale colors).
        /// </summary>
        public double Gray
        {
            get
            {
                UInt32 result = NativeMethods.Color_GetGray(Handle, out double data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return data;
            }
        }

        /// <summary>
        /// Get the cyan component (for CMYK colors).
        /// </summary>
        public double Cyan
        {
            get
            {
                UInt32 result = NativeMethods.Color_GetCyan(Handle, out double data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return data;
            }
        }

        /// <summary>
        /// Get the magenta component (for CMYK colors).
        /// </summary>
        public double Magenta
        {
            get
            {
                UInt32 result = NativeMethods.Color_GetMagenta(Handle, out double data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return data;
            }
        }

        /// <summary>
        /// Get the yellow component (for CMYK colors).
        /// </summary>
        public double Yellow
        {
            get
            {
                UInt32 result = NativeMethods.Color_GetYellow(Handle, out double data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return data;
            }
        }

        /// <summary>
        /// Get the black component (for CMYK colors).
        /// </summary>
        public double Black
        {
            get
            {
                UInt32 result = NativeMethods.Color_GetBlack(Handle, out double data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
                return data;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
