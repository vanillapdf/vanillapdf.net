using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Integer objects represent mathematical integers
    /// </summary>
    public class PdfIntegerObject : PdfObject
    {
        internal PdfIntegerObjectSafeHandle Handle { get; }

        internal PdfIntegerObject(PdfIntegerObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfIntegerObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfIntegerObjectSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Signed numeric value
        /// </summary>
        public Int64 IntegerValue
        {
            get { return GetIntegerValue(); }
            set { SetIntegerValue(value); }
        }

        /// <summary>
        /// Unsigned numeric value
        /// </summary>
        public UInt64 UnsignedIntegerValue
        {
            get { return GetUnsignedIntegerValue(); }
            set { SetUnsignedIntegerValue(value); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfIntegerObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfIntegerObject on success, throws exception on failure</returns>
        public static PdfIntegerObject Create()
        {
            UInt32 result = NativeMethods.IntegerObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfIntegerObject(data);
        }

        public override PdfObject ConvertTo<T>()
        {
            if (typeof(T) == typeof(PdfIntegerObject)) {
                return this;
            }

            if (typeof(T) == typeof(PdfRealObject)) {
                return PdfRealObject.FromObject(this);
            }

            return base.ConvertTo<T>();
        }

        private Int64 GetIntegerValue()
        {
            UInt32 result = NativeMethods.IntegerObject_GetIntegerValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return value;
        }

        private void SetIntegerValue(Int64 value)
        {
            UInt32 result = NativeMethods.IntegerObject_SetIntegerValue(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private UInt64 GetUnsignedIntegerValue()
        {
            UInt32 result = NativeMethods.IntegerObject_GetUnsignedIntegerValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return value;
        }

        private void SetUnsignedIntegerValue(UInt64 value)
        {
            UInt32 result = NativeMethods.IntegerObject_SetUnsignedIntegerValue(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Custom conversion to Int64
        /// </summary>
        /// <param name="obj">Handle to object to be converted</param>
        public static implicit operator Int64(PdfIntegerObject obj)
        {
            return obj.IntegerValue;
        }

        /// <summary>
        /// Custom conversion to UInt64
        /// </summary>
        /// <param name="obj">Handle to object to be converted</param>
        public static implicit operator UInt64(PdfIntegerObject obj)
        {
            return obj.UnsignedIntegerValue;
        }

        /// <summary>
        /// Convert object to integer object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfIntegerObject if the object can be converted, throws exception on failure</returns>
        public static PdfIntegerObject FromObject(PdfObject data)
        {
            return new PdfIntegerObject(data.ObjectHandle);
        }

        protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static CreateDelgate IntegerObject_Create = LibraryInstance.GetFunction<CreateDelgate>("IntegerObject_Create");
            public static GetIntegerValueDelgate IntegerObject_GetIntegerValue = LibraryInstance.GetFunction<GetIntegerValueDelgate>("IntegerObject_GetIntegerValue");
            public static SetIntegerValueDelgate IntegerObject_SetIntegerValue = LibraryInstance.GetFunction<SetIntegerValueDelgate>("IntegerObject_SetIntegerValue");

            public static GetUnsignedIntegerValueDelgate IntegerObject_GetUnsignedIntegerValue = LibraryInstance.GetFunction<GetUnsignedIntegerValueDelgate>("IntegerObject_GetUnsignedIntegerValue");
            public static SetUnsignedIntegerValueDelgate IntegerObject_SetUnsignedIntegerValue = LibraryInstance.GetFunction<SetUnsignedIntegerValueDelgate>("IntegerObject_SetUnsignedIntegerValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfIntegerObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetIntegerValueDelgate(PdfIntegerObjectSafeHandle handle, out Int64 value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetIntegerValueDelgate(PdfIntegerObjectSafeHandle handle, Int64 value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetUnsignedIntegerValueDelgate(PdfIntegerObjectSafeHandle handle, out UInt64 value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetUnsignedIntegerValueDelgate(PdfIntegerObjectSafeHandle handle, UInt64 value);
        }
    }
}
