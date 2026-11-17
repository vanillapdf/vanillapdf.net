using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents a rectangular region, specified by its lower-left and upper-right corners.
    /// </summary>
    public class PdfRectangle : IDisposable
    {
        internal PdfRectangleSafeHandle Handle { get; }

        internal PdfRectangle(PdfRectangleSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Lower-left X coordinate of the rectangle, truncated to an integer.
        /// </summary>
        [Obsolete("Rectangle coordinates are real numbers and fractional values are truncated. Use LowerLeftXReal instead.")]
        public Int64 LowerLeftX
        {
            get { return GetLowerLeftX(); }
            set { SetLowerLeftX(value); }
        }

        /// <summary>
        /// Lower-left Y coordinate of the rectangle, truncated to an integer.
        /// </summary>
        [Obsolete("Rectangle coordinates are real numbers and fractional values are truncated. Use LowerLeftYReal instead.")]
        public Int64 LowerLeftY
        {
            get { return GetLowerLeftY(); }
            set { SetLowerLeftY(value); }
        }

        /// <summary>
        /// Upper-right X coordinate of the rectangle, truncated to an integer.
        /// </summary>
        [Obsolete("Rectangle coordinates are real numbers and fractional values are truncated. Use UpperRightXReal instead.")]
        public Int64 UpperRightX
        {
            get { return GetUpperRightX(); }
            set { SetUpperRightX(value); }
        }

        /// <summary>
        /// Upper-right Y coordinate of the rectangle, truncated to an integer.
        /// </summary>
        [Obsolete("Rectangle coordinates are real numbers and fractional values are truncated. Use UpperRightYReal instead.")]
        public Int64 UpperRightY
        {
            get { return GetUpperRightY(); }
            set { SetUpperRightY(value); }
        }

        /// <summary>
        /// Lower-left X coordinate of the rectangle.
        /// </summary>
        public double LowerLeftXReal
        {
            get { return GetLowerLeftXReal(); }
            set { SetLowerLeftXReal(value); }
        }

        /// <summary>
        /// Lower-left Y coordinate of the rectangle.
        /// </summary>
        public double LowerLeftYReal
        {
            get { return GetLowerLeftYReal(); }
            set { SetLowerLeftYReal(value); }
        }

        /// <summary>
        /// Upper-right X coordinate of the rectangle.
        /// </summary>
        public double UpperRightXReal
        {
            get { return GetUpperRightXReal(); }
            set { SetUpperRightXReal(value); }
        }

        /// <summary>
        /// Upper-right Y coordinate of the rectangle.
        /// </summary>
        public double UpperRightYReal
        {
            get { return GetUpperRightYReal(); }
            set { SetUpperRightYReal(value); }
        }

        /// <summary>
        /// Create a new <see cref="PdfRectangle"/> instance.
        /// </summary>
        /// <returns>Handle to the newly created rectangle.</returns>
        public static PdfRectangle Create()
        {
            UInt32 result = NativeMethods.Rectangle_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfRectangle(data);
        }

        private Int64 GetLowerLeftX()
        {
            UInt32 result = NativeMethods.Rectangle_GetLowerLeftX(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetLowerLeftX(Int64 data)
        {
            UInt32 result = NativeMethods.Rectangle_SetLowerLeftX(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int64 GetLowerLeftY()
        {
            UInt32 result = NativeMethods.Rectangle_GetLowerLeftY(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetLowerLeftY(Int64 data)
        {
            UInt32 result = NativeMethods.Rectangle_SetLowerLeftY(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int64 GetUpperRightX()
        {
            UInt32 result = NativeMethods.Rectangle_GetUpperRightX(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetUpperRightX(Int64 data)
        {
            UInt32 result = NativeMethods.Rectangle_SetUpperRightX(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int64 GetUpperRightY()
        {
            UInt32 result = NativeMethods.Rectangle_GetUpperRightY(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetUpperRightY(Int64 data)
        {
            UInt32 result = NativeMethods.Rectangle_SetUpperRightY(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private double GetLowerLeftXReal()
        {
            UInt32 result = NativeMethods.Rectangle_GetLowerLeftXReal(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetLowerLeftXReal(double data)
        {
            UInt32 result = NativeMethods.Rectangle_SetLowerLeftXReal(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private double GetLowerLeftYReal()
        {
            UInt32 result = NativeMethods.Rectangle_GetLowerLeftYReal(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetLowerLeftYReal(double data)
        {
            UInt32 result = NativeMethods.Rectangle_SetLowerLeftYReal(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private double GetUpperRightXReal()
        {
            UInt32 result = NativeMethods.Rectangle_GetUpperRightXReal(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetUpperRightXReal(double data)
        {
            UInt32 result = NativeMethods.Rectangle_SetUpperRightXReal(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private double GetUpperRightYReal()
        {
            UInt32 result = NativeMethods.Rectangle_GetUpperRightYReal(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetUpperRightYReal(double data)
        {
            UInt32 result = NativeMethods.Rectangle_SetUpperRightYReal(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <inheritdoc/>

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
