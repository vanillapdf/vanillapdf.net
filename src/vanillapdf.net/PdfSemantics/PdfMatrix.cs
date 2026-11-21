using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents a transformation matrix [a b c d e f] mapping one coordinate space into another.
    /// </summary>
    public class PdfMatrix : IDisposable
    {
        internal PdfMatrixSafeHandle Handle { get; }

        internal PdfMatrix(PdfMatrixSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// The A coefficient - horizontal scaling.
        /// </summary>
        public double A
        {
            get => GetA();
            set => SetA(value);
        }

        /// <summary>
        /// The B coefficient - vertical skewing.
        /// </summary>
        public double B
        {
            get => GetB();
            set => SetB(value);
        }

        /// <summary>
        /// The C coefficient - horizontal skewing.
        /// </summary>
        public double C
        {
            get => GetC();
            set => SetC(value);
        }

        /// <summary>
        /// The D coefficient - vertical scaling.
        /// </summary>
        public double D
        {
            get => GetD();
            set => SetD(value);
        }

        /// <summary>
        /// The E coefficient - horizontal translation.
        /// </summary>
        public double E
        {
            get => GetE();
            set => SetE(value);
        }

        /// <summary>
        /// The F coefficient - vertical translation.
        /// </summary>
        public double F
        {
            get => GetF();
            set => SetF(value);
        }

        /// <summary>
        /// Create a new identity matrix [1 0 0 1 0 0].
        /// </summary>
        /// <returns>A new <see cref="PdfMatrix"/> instance.</returns>
        public static PdfMatrix Create()
        {
            UInt32 result = NativeMethods.Matrix_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfMatrix(data);
        }

        private double GetA()
        {
            UInt32 result = NativeMethods.Matrix_GetA(Handle, out double data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetA(double value)
        {
            UInt32 result = NativeMethods.Matrix_SetA(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private double GetB()
        {
            UInt32 result = NativeMethods.Matrix_GetB(Handle, out double data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetB(double value)
        {
            UInt32 result = NativeMethods.Matrix_SetB(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private double GetC()
        {
            UInt32 result = NativeMethods.Matrix_GetC(Handle, out double data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetC(double value)
        {
            UInt32 result = NativeMethods.Matrix_SetC(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private double GetD()
        {
            UInt32 result = NativeMethods.Matrix_GetD(Handle, out double data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetD(double value)
        {
            UInt32 result = NativeMethods.Matrix_SetD(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private double GetE()
        {
            UInt32 result = NativeMethods.Matrix_GetE(Handle, out double data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetE(double value)
        {
            UInt32 result = NativeMethods.Matrix_SetE(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private double GetF()
        {
            UInt32 result = NativeMethods.Matrix_GetF(Handle, out double data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetF(double value)
        {
            UInt32 result = NativeMethods.Matrix_SetF(Handle, value);
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
