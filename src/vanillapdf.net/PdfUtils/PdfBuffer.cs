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
    /// <remarks>
    /// <para>
    /// Performance comparison (GCHandle.Alloc vs fixed + Buffer.MemoryCopy):
    /// </para>
    /// <code>
    /// | Method               | Data Size | Before (GCHandle) | After (fixed) | Improvement |
    /// |----------------------|-----------|-------------------|---------------|-------------|
    /// | CreateFromData_Small |     100 B |           ~363 ns |       ~344 ns |   5% faster |
    /// | CreateFromData_Medium|     10 KB |           ~494 ns |       ~487 ns |   1% faster |
    /// | CreateFromData_Large |      1 MB |         ~41.5 us  |      ~44.5 us |     similar |
    /// | GetData_Small        |     100 B |            ~83 ns |        ~36 ns |  57% faster |
    /// | GetData_Medium       |     10 KB |           ~632 ns |       ~543 ns |  14% faster |
    /// | GetData_Large        |      1 MB |        ~85-93 us  |       ~103 us |     similar |
    /// </code>
    /// <para>
    /// GetDataString optimization (direct native memory read vs byte[] allocation):
    /// </para>
    /// <code>
    /// | Method                | Size  | Before          | After           | Improvement              |
    /// |-----------------------|-------|-----------------|-----------------|--------------------------|
    /// | GetStringData_Small   | 100 c |  ~53 ns, 352 B  |  ~44 ns, 224 B  | 18% faster, 36% less mem |
    /// | GetStringData_Medium  | 10K c | ~1595 ns, 30 KB | ~1016 ns, 20 KB | 36% faster, 33% less mem |
    /// </code>
    /// <para>
    /// Key optimizations:
    /// - Replaced GCHandle.Alloc with fixed statement for pinning
    /// - Replaced native Buffer_CopyTo with Buffer.MemoryCopy (eliminates P/Invoke overhead)
    /// - Added Span&lt;T&gt; and ReadOnlySpan&lt;byte&gt; overloads for zero-copy scenarios
    /// - GetDataString reads directly from native memory, avoiding intermediate byte[] allocation
    /// </para>
    /// </remarks>
    public class PdfBuffer : PdfUnknown, IEquatable<PdfBuffer>
    {
        private readonly object _syncLock = new object();

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
        /// <param name="data">Source data to initialize the buffer with</param>
        /// <returns>New instance of \ref PdfBuffer on success, throws exception on failure</returns>
        public static PdfBuffer CreateFromData(byte[] data) => CreateFromData(data.AsSpan());

        /// <summary>
        /// Create a new instance of \ref PdfBuffer with specified data within a single call
        /// </summary>
        /// <param name="data">Source data span to initialize the buffer with</param>
        /// <returns>New instance of \ref PdfBuffer on success, throws exception on failure</returns>
        public static unsafe PdfBuffer CreateFromData(ReadOnlySpan<byte> data)
        {
            // Use a sentinel for empty spans to get a valid pointer
            // (native code doesn't accept IntPtr.Zero even for zero-length data)
            if (data.IsEmpty) {
                byte sentinel = 0;
                UInt32 emptyResult = NativeMethods.Buffer_CreateFromData((IntPtr)(&sentinel), UIntPtr.Zero, out PdfBufferSafeHandle emptyHandle);
                if (emptyResult != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfBuffer(emptyHandle);
            }

            fixed (byte* ptr = data) {
                UInt32 result = NativeMethods.Buffer_CreateFromData((IntPtr)ptr, new UIntPtr((uint)data.Length), out PdfBufferSafeHandle handle);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfBuffer(handle);
            }
        }

        private unsafe byte[] GetData()
        {
            lock (_syncLock) {
                if (_disposed) throw new ObjectDisposedException(nameof(PdfBuffer));

                UInt32 result = NativeMethods.Buffer_GetData(Handle, out IntPtr data, out UIntPtr size);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                int sizeConverted = (int)size.ToUInt64();
                byte[] allocatedBuffer = new byte[sizeConverted];

                if (sizeConverted > 0) {
                    fixed (byte* dest = allocatedBuffer) {
                        Buffer.MemoryCopy((void*)data, dest, sizeConverted, sizeConverted);
                    }
                }

                return allocatedBuffer;
            }
        }

        private byte GetData(int offset)
        {
            lock (_syncLock) {
                if (_disposed) throw new ObjectDisposedException(nameof(PdfBuffer));

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
        }

        private void SetData(byte[] data) => SetData(data.AsSpan());

        private unsafe void SetData(ReadOnlySpan<byte> data)
        {
            lock (_syncLock) {
                if (_disposed) throw new ObjectDisposedException(nameof(PdfBuffer));

                // Use a sentinel for empty spans to get a valid pointer
                // (native code doesn't accept IntPtr.Zero even for zero-length data)
                if (data.IsEmpty) {
                    byte sentinel = 0;
                    UInt32 emptyResult = NativeMethods.Buffer_SetData(Handle, (IntPtr)(&sentinel), UIntPtr.Zero);
                    if (emptyResult != PdfReturnValues.ERROR_SUCCESS) {
                        throw PdfErrors.GetLastErrorException();
                    }
                    return;
                }

                fixed (byte* ptr = data) {
                    UInt32 result = NativeMethods.Buffer_SetData(Handle, (IntPtr)ptr, new UIntPtr((uint)data.Length));
                    if (result != PdfReturnValues.ERROR_SUCCESS) {
                        throw PdfErrors.GetLastErrorException();
                    }
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

        private unsafe string GetDataString()
        {
            lock (_syncLock) {
                if (_disposed) throw new ObjectDisposedException(nameof(PdfBuffer));

                UInt32 result = NativeMethods.Buffer_GetData(Handle, out IntPtr data, out UIntPtr size);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                int sizeConverted = (int)size.ToUInt64();
                if (sizeConverted == 0) {
                    return string.Empty;
                }

                // Read directly from native memory to avoid byte[] allocation
                return Encoding.ASCII.GetString((byte*)data, sizeConverted);
            }
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
            lock (_syncLock) {
                base.DisposeCustomHandle();
                Handle?.Dispose();
            }
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
            public delegate UInt32 CreateFromDataDelgate(IntPtr data, UIntPtr size, out PdfBufferSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetDataDelgate(PdfBufferSafeHandle handle, out IntPtr data, out UIntPtr size);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetDataDelgate(PdfBufferSafeHandle handle, IntPtr data, UIntPtr size);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CopyToDelgate(PdfBufferSafeHandle handle, IntPtr data, UIntPtr size);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ToInputStreamDelgate(PdfBufferSafeHandle handle, out PdfInputStreamSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 EqualsDelgate(PdfBufferSafeHandle handle, PdfBufferSafeHandle other, out bool data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 HashDelgate(PdfBufferSafeHandle handle, out UIntPtr data);
        }
    }
}
