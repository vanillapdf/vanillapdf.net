using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Represents floating point number object in the PDF structure
    /// </summary>
    public class PdfRealObject : PdfObject
    {
        internal PdfRealObject(PdfRealObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfRealObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
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

        public static implicit operator double(PdfRealObject obj)
        {
            return obj.Value;
        }

        public static PdfRealObject FromObject(PdfObject data)
        {
            return new PdfRealObject(data.Handle);
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
