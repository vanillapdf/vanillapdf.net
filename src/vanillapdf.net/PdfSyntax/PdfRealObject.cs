using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Real objects represent mathematical real numbers
    /// </summary>
    public class PdfRealObject : PdfObject
    {
        internal PdfRealObjectSafeHandle Handle { get; }

        internal PdfRealObject(PdfRealObjectSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfRealObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfRealObjectSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Currently stored floating point number
        /// </summary>
        public double Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfRealObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfRealObject</returns>
        public static PdfRealObject Create()
        {
            UInt32 result = NativeMethods.RealObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfRealObject(data);
        }

        private double GetValue()
        {
            UInt32 result = NativeMethods.RealObject_GetValue(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return value;
        }

        private void SetValue(double value)
        {
            UInt32 result = NativeMethods.RealObject_SetValue(Handle, value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public override PdfObject ConvertTo<T>()
        {
            if (typeof(T) == typeof(PdfRealObject)) {
                return this;
            }

            if (typeof(T) == typeof(PdfIntegerObject)) {
                return PdfIntegerObject.FromObject(this);
            }

            return base.ConvertTo<T>();
        }

        /// <summary>
        /// Custom conversion to double
        /// </summary>
        /// <param name="obj">Handle to object to be converted</param>
        public static implicit operator double(PdfRealObject obj)
        {
            return obj.Value;
        }

        /// <summary>
        /// Convert object to real object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfRealObject if the object can be converted, throws exception on failure</returns>
        public static PdfRealObject FromObject(PdfObject data)
        {
            return new PdfRealObject(data.ObjectHandle);
        }

        protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static CreateDelgate RealObject_Create = LibraryInstance.GetFunction<CreateDelgate>("RealObject_Create");
            public static GetValueDelgate RealObject_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("RealObject_GetValue");
            public static SetValueDelgate RealObject_SetValue = LibraryInstance.GetFunction<SetValueDelgate>("RealObject_SetValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfRealObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfRealObjectSafeHandle handle, out double value);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetValueDelgate(PdfRealObjectSafeHandle handle, double value);
        }
    }
}
