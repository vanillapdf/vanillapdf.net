using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    public class PdfInputOutputStream : PdfUnknown
    {
        internal PdfInputOutputStream(PdfInputOutputStreamSafeHandle handle) : base(handle)
        {
        }

        static PdfInputOutputStream()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public Int64 InputPosition
        {
            get { return GetInputPosition(); }
            set { SetInputPosition(value); }
        }

        public Int64 OutputPosition
        {
            get { return GetOutputPosition(); }
            set { SetOutputPosition(value); }
        }

        public static PdfInputOutputStream CreateFromFile(string filename)
        {
            UInt32 result = NativeMethods.InputOutputStream_CreateFromFile(filename, out PdfInputOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfInputOutputStream(data);
        }

        public static PdfInputOutputStream CreateFromMemory()
        {
            UInt32 result = NativeMethods.InputOutputStream_CreateFromMemory(out PdfInputOutputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfInputOutputStream(data);
        }

        internal Int64 GetInputPosition()
        {
            UInt32 result = NativeMethods.InputOutputStream_GetInputPosition(Handle, out Int64 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        internal void SetInputPosition(Int64 data)
        {
            UInt32 result = NativeMethods.InputOutputStream_SetInputPosition(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public byte[] Read(Int64 length)
        {
            GCHandle pinnedArray;

            try {
                byte[] allocatedBuffer = new byte[length];

                pinnedArray = GCHandle.Alloc(allocatedBuffer, GCHandleType.Pinned);

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

        public void WriteString(string data)
        {
            UInt32 result = NativeMethods.InputOutputStream_WriteString(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void WriteBuffer(PdfBuffer data)
        {
            UInt32 result = NativeMethods.InputOutputStream_WriteBuffer(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Flush()
        {
            UInt32 result = NativeMethods.InputOutputStream_Flush(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public static implicit operator PdfInputStream(PdfInputOutputStream data)
        {
            return new PdfInputStream(data.Handle);
        }

        public static explicit operator PdfInputOutputStream(PdfInputStream data)
        {
            return new PdfInputOutputStream(data.Handle);
        }

        public static implicit operator PdfOutputStream(PdfInputOutputStream data)
        {
            return new PdfOutputStream(data.Handle);
        }

        public static explicit operator PdfInputOutputStream(PdfOutputStream data)
        {
            return new PdfInputOutputStream(data.Handle);
        }

        private static class NativeMethods
        {
            public static CreateFromFileDelgate InputOutputStream_CreateFromFile = LibraryInstance.GetFunction<CreateFromFileDelgate>("InputOutputStream_CreateFromFile");
            public static CreateFromMemoryDelgate InputOutputStream_CreateFromMemory = LibraryInstance.GetFunction<CreateFromMemoryDelgate>("InputOutputStream_CreateFromMemory");

            public static ReadDelgate InputOutputStream_Read = LibraryInstance.GetFunction<ReadDelgate>("InputOutputStream_Read");
            public static ReadBufferDelgate InputOutputStream_ReadBuffer = LibraryInstance.GetFunction<ReadBufferDelgate>("InputOutputStream_ReadBuffer");
            public static GetInputPositionDelgate InputOutputStream_GetInputPosition = LibraryInstance.GetFunction<GetInputPositionDelgate>("InputOutputStream_GetInputPosition");
            public static SetInputPositionDelgate InputOutputStream_SetInputPosition = LibraryInstance.GetFunction<SetInputPositionDelgate>("InputOutputStream_SetInputPosition");

            public static GetOutputPositionDelgate InputOutputStream_GetOutputPosition = LibraryInstance.GetFunction<GetOutputPositionDelgate>("InputOutputStream_GetOutputPosition");
            public static SetOutputPositionDelgate InputOutputStream_SetOutputPosition = LibraryInstance.GetFunction<SetOutputPositionDelgate>("InputOutputStream_SetOutputPosition");
            public static WriteStringDelgate InputOutputStream_WriteString = LibraryInstance.GetFunction<WriteStringDelgate>("InputOutputStream_WriteString");
            public static WriteBufferDelgate InputOutputStream_WriteBuffer = LibraryInstance.GetFunction<WriteBufferDelgate>("InputOutputStream_WriteBuffer");
            public static FlushDelgate InputOutputStream_Flush = LibraryInstance.GetFunction<FlushDelgate>("InputOutputStream_Flush");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromFileDelgate(string filename, out PdfInputOutputStreamSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromMemoryDelgate(out PdfInputOutputStreamSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ReadDelgate(PdfInputOutputStreamSafeHandle handle, Int64 length, IntPtr data, out Int64 readLength);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ReadBufferDelgate(PdfInputOutputStreamSafeHandle handle, Int64 length, out PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetInputPositionDelgate(PdfInputOutputStreamSafeHandle handle, out Int64 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetInputPositionDelgate(PdfInputOutputStreamSafeHandle handle, Int64 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOutputPositionDelgate(PdfInputOutputStreamSafeHandle handle, out Int64 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetOutputPositionDelgate(PdfInputOutputStreamSafeHandle handle, Int64 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 WriteStringDelgate(PdfInputOutputStreamSafeHandle handle, string data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 WriteBufferDelgate(PdfInputOutputStreamSafeHandle handle, PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 FlushDelgate(PdfInputOutputStreamSafeHandle handle);
        }
    }
}
