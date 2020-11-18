using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Represents boolean object in the PDF structure
    /// </summary>
    public class PdfBooleanObject : PdfObject
    {
        internal PdfBooleanObject(PdfBooleanObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfBooleanObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        /// <summary>
        /// Currently stored boolean value
        /// </summary>
        public bool Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Create a new instance of \ref PdfBooleanObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfBooleanObject</returns>
        public static PdfBooleanObject Create()
        {
            UInt32 result = NativeMethods.BooleanObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfBooleanObject(data);
        }

        private bool GetValue()
        {
            UInt32 result = NativeMethods.BooleanObject_GetValue(Handle, out bool data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetValue(bool data)
        {
            UInt32 result = NativeMethods.BooleanObject_SetValue(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public static implicit operator bool(PdfBooleanObject obj)
        {
            return obj.Value;
        }

        public static PdfBooleanObject FromObject(PdfObject data)
        {
            return new PdfBooleanObject(data.Handle);
        }

        private static class NativeMethods
        {
            public static CreateDelgate BooleanObject_Create = LibraryInstance.GetFunction<CreateDelgate>("BooleanObject_Create");
            public static GetValueDelgate BooleanObject_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("BooleanObject_GetValue");
            public static SetValueDelgate BooleanObject_SetValue = LibraryInstance.GetFunction<SetValueDelgate>("BooleanObject_SetValue");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfBooleanObjectSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfBooleanObjectSafeHandle handle, out bool data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetValueDelgate(PdfBooleanObjectSafeHandle handle, bool data);
        }
    }
}
