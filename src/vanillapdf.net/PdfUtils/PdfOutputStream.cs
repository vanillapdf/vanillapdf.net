using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Output stream can write sequences of characters and represent other kinds of data
    /// </summary>
    public class PdfOutputStream : PdfUnknown
    {
        internal PdfOutputStream(PdfOutputStreamSafeHandle handle) : base(handle)
        {
        }

        static PdfOutputStream()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfOutputStreamSafeHandle).TypeHandle);
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
        /// Create a new instance of \ref PdfOutputStream from specified file
        /// </summary>
        /// <param name="filename">Path where the destination file should be created</param>
        /// <returns>New instance of \ref PdfOutputStream on success, throws exception on failure</returns>
        public static PdfOutputStream CreateFromFile(string filename)
        {
            UInt32 result = NativeMethods.OutputStream_CreateFromFile(filename, out PdfOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfOutputStream(data);
        }

        private Int64 GetOutputPosition()
        {
            UInt32 result = NativeMethods.OutputStream_GetOutputPosition(Handle, out Int64 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetOutputPosition(Int64 data)
        {
            UInt32 result = NativeMethods.OutputStream_SetOutputPosition(Handle, data);
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
            UInt32 result = NativeMethods.OutputStream_WriteString(Handle, data);
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
            UInt32 result = NativeMethods.OutputStream_WriteBuffer(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Flushes all pending data from the stream to it's destination
        /// </summary>
        public void Flush()
        {
            UInt32 result = NativeMethods.OutputStream_Flush(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private static class NativeMethods
        {
            public static CreateFromFileDelgate OutputStream_CreateFromFile = LibraryInstance.GetFunction<CreateFromFileDelgate>("OutputStream_CreateFromFile");
            public static GetOutputPositionDelgate OutputStream_GetOutputPosition = LibraryInstance.GetFunction<GetOutputPositionDelgate>("OutputStream_GetOutputPosition");
            public static SetOutputPositionDelgate OutputStream_SetOutputPosition = LibraryInstance.GetFunction<SetOutputPositionDelgate>("OutputStream_SetOutputPosition");

            public static WriteStringDelgate OutputStream_WriteString = LibraryInstance.GetFunction<WriteStringDelgate>("OutputStream_WriteString");
            public static WriteBufferDelgate OutputStream_WriteBuffer = LibraryInstance.GetFunction<WriteBufferDelgate>("OutputStream_WriteBuffer");
            public static FlushDelgate OutputStream_Flush = LibraryInstance.GetFunction<FlushDelgate>("OutputStream_Flush");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromFileDelgate(string filename, out PdfOutputStreamSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOutputPositionDelgate(PdfOutputStreamSafeHandle handle, out Int64 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetOutputPositionDelgate(PdfOutputStreamSafeHandle handle, Int64 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 WriteStringDelgate(PdfOutputStreamSafeHandle handle, string data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 WriteBufferDelgate(PdfOutputStreamSafeHandle handle, PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FlushDelgate(PdfOutputStreamSafeHandle handle);
        }
    }
}
