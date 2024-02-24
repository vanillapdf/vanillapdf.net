using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Serves as container for transferring arbitrary binary data
    /// </summary>
    public class PdfBuffer : PdfUnknown, IEquatable<PdfBuffer>
    {
        internal PdfBufferSafeHandle Handle { get; }

        internal PdfBuffer(PdfBufferSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfBuffer()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfBufferSafeHandle).TypeHandle);
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
        /// Read and modify the values at the specific offset within the buffer
        /// </summary>
        /// <param name="i">Offset of the byte within the buffer</param>
        /// <returns>Byte data representation of the value at the specific offset</returns>
        public byte this[int i]
        {
            get { return GetData(i); }
            set { SetData(i, value); }
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

        /// <summary>
        /// Create a new instance of \ref PdfBuffer with specified data within a single call
        /// </summary>
        /// <returns>New instance of \ref PdfBuffer on success, throws exception on failure</returns>
        public static PdfBuffer CreateFromData(byte[] data)
        {
            GCHandle pinnedArray = GCHandle.Alloc(data, GCHandleType.Pinned);

            try {
                var dataSize = Convert.ToUInt64(data.Length);

                UInt32 result = NativeMethods.Buffer_CreateFromData(out PdfBufferSafeHandle handle, pinnedArray.AddrOfPinnedObject(), new UIntPtr(dataSize));
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfBuffer(handle);
            }
            finally {
                if (pinnedArray.IsAllocated) {
                    pinnedArray.Free();
                }
            }
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

            // Marshal.Copy throws exception if the data is null pointer
            if (sizeConverted > 0) {
                Debug.Assert(data != IntPtr.Zero);
                Marshal.Copy(data, allocatedBuffer, 0, sizeConverted);
            }

            return allocatedBuffer;
        }

        private byte GetData(int offset)
        {
            UInt32 result = NativeMethods.Buffer_GetData(Handle, out IntPtr data, out UIntPtr size);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            // TODO: might overflow
            var rawSize = size.ToUInt64();
            var sizeConverted = Convert.ToInt32(rawSize);

            if (offset > sizeConverted) {
                throw new PdfManagedException($"Index {offset} is out of bounds, data length {sizeConverted}");
            }

            return Marshal.ReadByte(data, offset);
        }

        private void SetData(byte[] data)
        {
            GCHandle pinnedArray = GCHandle.Alloc(data, GCHandleType.Pinned);

            try {
                var dataSize = Convert.ToUInt64(data.Length);

                UInt32 result = NativeMethods.Buffer_SetData(Handle, pinnedArray.AddrOfPinnedObject(), new UIntPtr(dataSize));
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }
            }
            finally {
                if (pinnedArray.IsAllocated) {
                    pinnedArray.Free();
                }
            }
        }

        private void SetData(int offset, byte value)
        {
            // Currently there is no native API for such functionality
            // It's not really that big of a overhead, so let's use existing functions

            // Read the existing data into buffer
            var currentData = GetData();

            // Adjust the value at the offset
            currentData[offset] = value;

            // Set the new data
            SetData(currentData);
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

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is PdfBuffer) {
                return Equals(obj as PdfBuffer);
            }

            return base.Equals(obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (int)Hash();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return StringData;
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static CreateDelgate Buffer_Create = LibraryInstance.GetFunction<CreateDelgate>("Buffer_Create");
            public static CreateFromDataDelgate Buffer_CreateFromData = LibraryInstance.GetFunction<CreateFromDataDelgate>("Buffer_CreateFromData");
            public static GetDataDelgate Buffer_GetData = LibraryInstance.GetFunction<GetDataDelgate>("Buffer_GetData");
            public static SetDataDelgate Buffer_SetData = LibraryInstance.GetFunction<SetDataDelgate>("Buffer_SetData");
            public static ToInputStreamDelgate Buffer_ToInputStream = LibraryInstance.GetFunction<ToInputStreamDelgate>("Buffer_ToInputStream");

            public static EqualsDelgate Buffer_Equals = LibraryInstance.GetFunction<EqualsDelgate>("Buffer_Equals");
            public static HashDelgate Buffer_Hash = LibraryInstance.GetFunction<HashDelgate>("Buffer_Hash");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfBufferSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromDataDelgate(out PdfBufferSafeHandle handle, IntPtr data, UIntPtr size);

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
