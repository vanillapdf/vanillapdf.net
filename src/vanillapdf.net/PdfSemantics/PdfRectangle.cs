using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents a rectangular region using integer coordinates.
    /// </summary>
    public class PdfRectangle : PdfUnknown
    {
        internal PdfRectangleSafeHandle Handle { get; }

        internal PdfRectangle(PdfRectangleSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Lower-left X coordinate of the rectangle.
        /// </summary>
        public Int64 LowerLeftX
        {
            get { return GetLowerLeftX(); }
            set { SetLowerLeftX(value); }
        }

        /// <summary>
        /// Lower-left Y coordinate of the rectangle.
        /// </summary>
        public Int64 LowerLeftY
        {
            get { return GetLowerLeftY(); }
            set { SetLowerLeftY(value); }
        }

        /// <summary>
        /// Upper-right X coordinate of the rectangle.
        /// </summary>
        public Int64 UpperRightX
        {
            get { return GetUpperRightX(); }
            set { SetUpperRightX(value); }
        }

        /// <summary>
        /// Upper-right Y coordinate of the rectangle.
        /// </summary>
        public Int64 UpperRightY
        {
            get { return GetUpperRightY(); }
            set { SetUpperRightY(value); }
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

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }
    }
}
