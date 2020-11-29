using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Serves as container for transferring arbitrary binary data
    /// </summary>
    public class PdfBuffer : PdfUnknown, IEquatable<PdfBuffer>
    {
        internal PdfBuffer(PdfBufferSafeHandle handle) : base(handle)
        {
        }

        static PdfBuffer()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        /// <summary>
        /// Binary data that are stored in the buffer
        /// </summary>
        public byte[] Data
        {
            get { return GetData(); }
            set { SetData(value); }
        }

        /// <summary>
        /// ANSI string representation of the binary data
        /// </summary>
        public string StringData
        {
            get { return GetDataString(); }
            set { SetDataString(value); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfBuffer with default value
        /// </summary>
        /// <returns>New instance of \ref PdfBuffer on success, throws exception on failure</returns>
        public static PdfBuffer Create()
        {
            UInt32 result = NativeMethods.Buffer_Create(out PdfBufferSafeHandle handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(handle);
        }

        private byte[] GetData()
        {
            UInt32 result = NativeMethods.Buffer_GetData(Handle, out IntPtr data, out UIntPtr size);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            // TODO: might overflow
            var rawSize = size.ToUInt64();
            var sizeConverted = Convert.ToInt32(rawSize);

            byte[] allocatedBuffer = new byte[sizeConverted];
            Marshal.Copy(data, allocatedBuffer, 0, sizeConverted);

            return allocatedBuffer;
        }

        private void SetData(byte[] data)
        {
            IntPtr allocator = IntPtr.Zero;
            UInt32 result = PdfReturnValues.ERROR_GENERAL;

            try {
                allocator = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, allocator, data.Length);

                var dataSize = Convert.ToUInt64(data.Length);
                result = NativeMethods.Buffer_SetData(Handle, allocator, new UIntPtr(dataSize));

            }
            finally {
                if (allocator != IntPtr.Zero) {
                    Marshal.FreeHGlobal(allocator);
                    allocator = IntPtr.Zero;
                }
            }

            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private string GetDataString()
        {
            var data = GetData();
            return Encoding.ASCII.GetString(data);
        }

        private void SetDataString(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            SetData(bytes);
        }

        /// <summary>
        /// Custom equality operator to compare buffer contents
        /// </summary>
        /// <param name="other">Other buffer to be compared</param>
        /// <returns>True when the buffer data are equal, false if not equal, throws exception on failure</returns>
        public bool Equals(PdfBuffer other)
        {
            if (other == null) {
                return false;
            }

            UInt32 result = NativeMethods.Buffer_Equals(Handle, other.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Custom hash value calculation
        /// </summary>
        /// <returns>Integral value representing hash of the binary data</returns>
        public UInt64 Hash()
        {
            UInt32 result = NativeMethods.Buffer_Hash(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Create a new instance of \ref PdfInputStream from the current data of the \ref PdfBuffer
        /// </summary>
        /// <returns>New instance of \ref PdfInputStream on success, throws exception on failure</returns>
        public PdfInputStream ToInputStream()
        {
            UInt32 result = NativeMethods.Buffer_ToInputStream(Handle, out PdfInputStreamSafeHandle handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfInputStream(handle);
        }

        /// <summary>
        /// Custom equality operator to support \ref PdfBuffer
        /// </summary>
        /// <param name="obj">Other object to be compared</param>
        /// <returns>True if the compared objects are equal, false if not equal, throws exception on failure</returns>
        public override bool Equals(object obj)
        {
            if (obj is PdfBuffer) {
                return Equals(obj as PdfBuffer);
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Custom hash value calculation
        /// </summary>
        /// <returns>Integral value representing hash of the binary data</returns>
        public override int GetHashCode()
        {
            return (int)Hash();
        }

        private static class NativeMethods
        {
            public static CreateDelgate Buffer_Create = LibraryInstance.GetFunction<CreateDelgate>("Buffer_Create");
            public static GetDataDelgate Buffer_GetData = LibraryInstance.GetFunction<GetDataDelgate>("Buffer_GetData");
            public static SetDataDelgate Buffer_SetData = LibraryInstance.GetFunction<SetDataDelgate>("Buffer_SetData");
            public static ToInputStreamDelgate Buffer_ToInputStream = LibraryInstance.GetFunction<ToInputStreamDelgate>("Buffer_ToInputStream");

            public static EqualsDelgate Buffer_Equals = LibraryInstance.GetFunction<EqualsDelgate>("Buffer_Equals");
            public static HashDelgate Buffer_Hash = LibraryInstance.GetFunction<HashDelgate>("Buffer_Hash");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfBufferSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetDataDelgate(PdfBufferSafeHandle handle, out IntPtr data, out UIntPtr size);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetDataDelgate(PdfBufferSafeHandle handle, IntPtr data, UIntPtr size);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ToInputStreamDelgate(PdfBufferSafeHandle handle, out PdfInputStreamSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 EqualsDelgate(PdfBufferSafeHandle handle, PdfBufferSafeHandle other, out bool data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 HashDelgate(PdfBufferSafeHandle handle, out UIntPtr data);
        }
    }
}
