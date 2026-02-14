using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Interop;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Universal class for IO operations
    /// </summary>
    public class PdfInputOutputStream : IDisposable
    {
        internal PdfInputOutputStreamSafeHandle Handle { get; }

        internal PdfInputOutputStream(PdfInputOutputStreamSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Current offset position in the input stream
        /// </summary>
        public Int64 InputPosition
        {
            get { return GetInputPosition(); }
            set { SetInputPosition(value); }
        }

        /// <summary>
        /// Current offset position in the output stream
        /// </summary>
        public Int64 OutputPosition
        {
            get { return GetOutputPosition(); }
            set { SetOutputPosition(value); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfInputOutputStream from specified file
        /// </summary>
        /// <param name="filename">Path to file to be read</param>
        /// <returns>New instance of \ref PdfInputOutputStream on success, throws exception on failure</returns>
        public static PdfInputOutputStream CreateFromFile(string filename)
        {
            UInt32 result = NativeMethods.InputOutputStream_CreateFromFile(filename, out PdfInputOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfInputOutputStream(data);
        }

        /// <summary>
        /// Create a new instance of \ref PdfInputOutputStream stored in memory
        /// </summary>
        /// <returns>New instance of \ref PdfInputOutputStream on success, throws exception on failure</returns>
        public static PdfInputOutputStream CreateFromMemory()
        {
            UInt32 result = NativeMethods.InputOutputStream_CreateFromMemory(out PdfInputOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfInputOutputStream(data);
        }

        private Int64 GetInputPosition()
        {
            UInt32 result = NativeMethods.InputOutputStream_GetInputPosition(Handle, out Int64 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetInputPosition(Int64 data)
        {
            UInt32 result = NativeMethods.InputOutputStream_SetInputPosition(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Read bytes from input stream
        /// </summary>
        /// <param name="length">Maximum length of the data to be read from the stream</param>
        /// <returns>Binary data up to maximum <p>length</p> on success, throws exception on failure</returns>
        public byte[] Read(Int64 length)
        {
            byte[] allocatedBuffer = new byte[length];
            GCHandle pinnedArray = GCHandle.Alloc(allocatedBuffer, GCHandleType.Pinned);

            try {
                UInt32 result = NativeMethods.InputOutputStream_Read(Handle, length, pinnedArray.AddrOfPinnedObject(), out Int64 readLength);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                var convertedLength = Convert.ToInt32(readLength);
                Array.Resize(ref allocatedBuffer, convertedLength);

                return allocatedBuffer;
            } finally {
                if (pinnedArray.IsAllocated) {
                    pinnedArray.Free();
                }
            }
        }

        /// <summary>
        /// Read bytes from input stream
        /// </summary>
        /// <param name="length">Maximum length of the data to be read from the stream</param>
        /// <returns>Binary data up to maximum <p>length</p> on success, throws exception on failure</returns>
        public PdfBuffer ReadBuffer(Int64 length)
        {
            UInt32 result = NativeMethods.InputOutputStream_ReadBuffer(Handle, length, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        private Int64 GetOutputPosition()
        {
            UInt32 result = NativeMethods.InputOutputStream_GetOutputPosition(Handle, out Int64 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetOutputPosition(Int64 data)
        {
            UInt32 result = NativeMethods.InputOutputStream_SetOutputPosition(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Appends string data to current output stream instance
        /// </summary>
        /// <param name="data">Data to be written into the output stream</param>
        public void WriteString(string data)
        {
            UInt32 result = NativeMethods.InputOutputStream_WriteString(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Appends buffer data to current output stream instance
        /// </summary>
        /// <param name="data">Data to be written into the output stream</param>
        public void WriteBuffer(PdfBuffer data)
        {
            UInt32 result = NativeMethods.InputOutputStream_WriteBuffer(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Flushes all pending data from the stream to it's destination
        /// </summary>
        public void Flush()
        {
            UInt32 result = NativeMethods.InputOutputStream_Flush(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Convert to \ref PdfInputStream
        /// </summary>
        /// <param name="data">Handle to \ref PdfInputOutputStream to be converted</param>
        public static implicit operator PdfInputStream(PdfInputOutputStream data)
        {
            return new PdfInputStream(data.Handle);
        }

        /// <summary>
        /// Convert to \ref PdfInputOutputStream
        /// </summary>
        /// <param name="data">Handle to \ref PdfInputStream to be converted</param>
        public static explicit operator PdfInputOutputStream(PdfInputStream data)
        {
            return new PdfInputOutputStream(data.Handle);
        }

        /// <summary>
        /// Convert to \ref PdfOutputStream
        /// </summary>
        /// <param name="data">Handle to \ref PdfInputOutputStream to be converted</param>
        public static implicit operator PdfOutputStream(PdfInputOutputStream data)
        {
            return new PdfOutputStream(data.Handle);
        }

        /// <summary>
        /// Convert to \ref PdfInputOutputStream
        /// </summary>
        /// <param name="data">Handle to \ref PdfOutputStream to be converted</param>
        public static explicit operator PdfInputOutputStream(PdfOutputStream data)
        {
            return new PdfInputOutputStream(data.Handle);
        }

        /// <inheritdoc/>

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
