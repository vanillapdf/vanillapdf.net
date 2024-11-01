using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// A name object is an atomic symbol uniquely defined by a sequence of characters
    /// </summary>
    public class PdfNameObject : PdfObject, IEquatable<PdfNameObject>
    {
        internal PdfNameObjectSafeHandle Handle { get; }

        internal PdfNameObject(PdfNameObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfNameObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfNameObjectSafeHandle).TypeHandle);
        }

        public PdfBuffer Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        public UInt64 Hash
        {
            get { return GetHash(); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfNameObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfNameObject on success, throws exception on failure</returns>
        public static PdfNameObject Create()
        {
            UInt32 result = NativeMethods.NameObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(data);
        }

        public static PdfNameObject CreateFromEncodedString(string data)
        {
            UInt32 result = NativeMethods.NameObject_CreateFromEncodedString(data, out var handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(handle);
        }

        public static PdfNameObject CreateFromDecodedString(string data)
        {
            UInt32 result = NativeMethods.NameObject_CreateFromDecodedString(data, out var handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNameObject(handle);
        }

        private PdfBuffer GetValue()
        {
            UInt32 result = NativeMethods.NameObject_GetValue(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(data);
        }

        private void SetValue(PdfBuffer data)
        {
            UInt32 result = NativeMethods.NameObject_SetValue(Handle, data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private UInt64 GetHash()
        {
            UInt32 result = NativeMethods.NameObject_Hash(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        public static implicit operator PdfNameObject(string data)
        {
            return CreateFromDecodedString(data);
        }

        public static implicit operator string(PdfNameObject data)
        {
            return data.ToString();
        }

        internal override PdfObject ConvertTo<T>()
        {
            if (typeof(T) == typeof(PdfNameObject)) {
                return this;
            }

            return base.ConvertTo<T>();
        }

        /// <summary>
        /// Convert object to name object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfNameObject if the object can be converted, throws exception on failure</returns>
        public static PdfNameObject FromObject(PdfObject data)
        {
            // This optimization does have severe side-effects and it's not worth it
            //if (data is PdfNameObject pdfNameObject) {
            //    return pdfNameObject;
            //}

            return new PdfNameObject(data.ObjectHandle);
        }

        #region IEquatable<PdfNameObject>

        /// <inheritdoc/>
        public bool Equals(PdfNameObject other)
        {
            if (other == null) {
                return false;
            }

            UInt32 result = NativeMethods.NameObject_Equals(Handle, other.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        #endregion

        #region object

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is PdfNameObject) {
                return Equals(obj as PdfNameObject);
            }

            return base.Equals(obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (int)GetHash();
        }

        #endregion

        #region PdfUnknown

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #endregion

        private static class NativeMethods
        {
            public static CreateDelgate NameObject_Create = LibraryInstance.GetFunction<CreateDelgate>("NameObject_Create");
            public static CreateFromEncodedStringDelgate NameObject_CreateFromEncodedString = LibraryInstance.GetFunction<CreateFromEncodedStringDelgate>("NameObject_CreateFromEncodedString");
            public static CreateFromDecodedStringDelgate NameObject_CreateFromDecodedString = LibraryInstance.GetFunction<CreateFromDecodedStringDelgate>("NameObject_CreateFromDecodedString");
            public static GetValueDelgate NameObject_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("NameObject_GetValue");
            public static SetValueDelgate NameObject_SetValue = LibraryInstance.GetFunction<SetValueDelgate>("NameObject_SetValue");
            public static EqualsDelgate NameObject_Equals = LibraryInstance.GetFunction<EqualsDelgate>("NameObject_Equals");
            public static HashDelgate NameObject_Hash = LibraryInstance.GetFunction<HashDelgate>("NameObject_Hash");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfNameObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromEncodedStringDelgate(string data, out PdfNameObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateFromDecodedStringDelgate(string data, out PdfNameObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfNameObjectSafeHandle handle, out PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetValueDelgate(PdfNameObjectSafeHandle handle, PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 EqualsDelgate(PdfNameObjectSafeHandle handle, PdfNameObjectSafeHandle other, out bool data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 HashDelgate(PdfNameObjectSafeHandle handle, out UIntPtr data);
        }
    }
}
