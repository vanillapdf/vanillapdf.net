using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public class PdfOutputStream : PdfUnknown
    {
        internal PdfOutputStream(PdfOutputStreamSafeHandle handle) : base(handle)
        {
        }

        static PdfOutputStream()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public Int64 OutputPosition
        {
            get { return GetOutputPosition(); }
            set { SetOutputPosition(value); }
        }

        public static PdfOutputStream CreateFromFile(string filename)
        {
            UInt32 result = NativeMethods.OutputStream_CreateFromFile(filename, out PdfOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfOutputStream(data);
        }

        public Int64 GetOutputPosition()
        {
            UInt32 result = NativeMethods.OutputStream_GetOutputPosition(Handle, out Int64 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public void SetOutputPosition(Int64 data)
        {
            UInt32 result = NativeMethods.OutputStream_SetOutputPosition(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void WriteString(string data)
        {
            UInt32 result = NativeMethods.OutputStream_WriteString(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void WriteBuffer(PdfBuffer data)
        {
            UInt32 result = NativeMethods.OutputStream_WriteBuffer(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

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
