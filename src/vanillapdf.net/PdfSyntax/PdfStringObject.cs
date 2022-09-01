using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Reprsents human readable text
    /// </summary>
    public class PdfStringObject : PdfObject
    {
        internal PdfStringObjectSafeHandle StringHandle { get; }

        internal PdfStringObject(PdfStringObjectSafeHandle handle) : base(handle)
        {
            StringHandle = handle;
        }

        static PdfStringObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfStringObjectSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get value of underlying data buffer
        /// </summary>
        public PdfBuffer Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfStringType GetStringType()
        {
            UInt32 result = NativeMethods.StringObject_GetStringType(StringHandle, out PdfStringType data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfStringType>.CheckedCast(data);
        }

        private PdfBuffer GetValue()
        {
            UInt32 result = NativeMethods.StringObject_GetValue(StringHandle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBuffer(value);
        }

        private void SetValue(PdfBuffer value)
        {
            UInt32 result = NativeMethods.StringObject_SetValue(StringHandle, value.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public override PdfObject ConvertTo<T>()
        {
            if (typeof(T) == typeof(PdfStringObject)) {
                return this;
            }

            return base.ConvertTo<T>();
        }

        /// <summary>
        /// Convert object to string object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfStringObject if the object can be converted, throws exception on failure</returns>
        public static PdfStringObject FromObject(PdfObject data)
        {
            return new PdfStringObject(data.ObjectHandle);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            StringHandle?.Dispose();
        }

        private static class NativeMethods
        {
            public static GetStringTypeDelgate StringObject_GetStringType = LibraryInstance.GetFunction<GetStringTypeDelgate>("StringObject_GetStringType");
            public static GetValueDelgate StringObject_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("StringObject_GetValue");
            public static SetValueDelgate StringObject_SetValue = LibraryInstance.GetFunction<SetValueDelgate>("StringObject_SetValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetStringTypeDelgate(PdfStringObjectSafeHandle handle, out PdfStringType data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfStringObjectSafeHandle handle, out PdfBufferSafeHandle value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetValueDelgate(PdfStringObjectSafeHandle handle, PdfBufferSafeHandle value);
        }
    }
}
