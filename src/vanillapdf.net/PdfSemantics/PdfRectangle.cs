using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    public class PdfRectangle : PdfUnknown
    {
        internal PdfRectangle(PdfRectangleSafeHandle handle) : base(handle)
        {
        }

        static PdfRectangle()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfRectangleSafeHandle).TypeHandle);
        }

        public Int64 LowerLeftX
        {
            get { return GetLowerLeftX(); }
            set { SetLowerLeftX(value); }
        }

        public Int64 LowerLeftY
        {
            get { return GetLowerLeftY(); }
            set { SetLowerLeftY(value); }
        }

        public Int64 UpperRightX
        {
            get { return GetUpperRightX(); }
            set { SetUpperRightX(value); }
        }

        public Int64 UpperRightY
        {
            get { return GetUpperRightY(); }
            set { SetUpperRightY(value); }
        }

        public static PdfRectangle Create()
        {
            UInt32 result = NativeMethods.Rectangle_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfRectangle(data);
        }

        private Int64 GetLowerLeftX()
        {
            UInt32 result = NativeMethods.Rectangle_GetLowerLeftX(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetLowerLeftX(Int64 data)
        {
            UInt32 result = NativeMethods.Rectangle_SetLowerLeftX(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int64 GetLowerLeftY()
        {
            UInt32 result = NativeMethods.Rectangle_GetLowerLeftY(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetLowerLeftY(Int64 data)
        {
            UInt32 result = NativeMethods.Rectangle_SetLowerLeftY(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int64 GetUpperRightX()
        {
            UInt32 result = NativeMethods.Rectangle_GetUpperRightX(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetUpperRightX(Int64 data)
        {
            UInt32 result = NativeMethods.Rectangle_SetUpperRightX(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int64 GetUpperRightY()
        {
            UInt32 result = NativeMethods.Rectangle_GetUpperRightY(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetUpperRightY(Int64 data)
        {
            UInt32 result = NativeMethods.Rectangle_SetUpperRightY(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private static class NativeMethods
        {
            public static CreateDelgate Rectangle_Create = LibraryInstance.GetFunction<CreateDelgate>("Rectangle_Create");
            public static GetValueDelgate Rectangle_GetLowerLeftX = LibraryInstance.GetFunction<GetValueDelgate>("Rectangle_GetLowerLeftX");
            public static SetValueDelgate Rectangle_SetLowerLeftX = LibraryInstance.GetFunction<SetValueDelgate>("Rectangle_SetLowerLeftX");
            public static GetValueDelgate Rectangle_GetLowerLeftY = LibraryInstance.GetFunction<GetValueDelgate>("Rectangle_GetLowerLeftY");
            public static SetValueDelgate Rectangle_SetLowerLeftY = LibraryInstance.GetFunction<SetValueDelgate>("Rectangle_SetLowerLeftY");
            public static GetValueDelgate Rectangle_GetUpperRightX = LibraryInstance.GetFunction<GetValueDelgate>("Rectangle_GetUpperRightX");
            public static SetValueDelgate Rectangle_SetUpperRightX = LibraryInstance.GetFunction<SetValueDelgate>("Rectangle_SetUpperRightX");
            public static GetValueDelgate Rectangle_GetUpperRightY = LibraryInstance.GetFunction<GetValueDelgate>("Rectangle_GetUpperRightY");
            public static SetValueDelgate Rectangle_SetUpperRightY = LibraryInstance.GetFunction<SetValueDelgate>("Rectangle_SetUpperRightY");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfRectangleSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfRectangleSafeHandle handle, out Int64 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetValueDelgate(PdfRectangleSafeHandle handle, Int64 data);
        }
    }
}
