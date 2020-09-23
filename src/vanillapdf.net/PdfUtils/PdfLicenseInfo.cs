using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public static class PdfLicenseInfo
    {
        static PdfLicenseInfo()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public static void SetLicenseFile(string filename)
        {
            UInt32 result = NativeMethods.LicenseInfo_SetLicenseFile(filename);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public static void SetLicenseBuffer(PdfBuffer data)
        {
            UInt32 result = NativeMethods.LicenseInfo_SetLicenseBuffer(data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public static bool IsValid()
        {
            UInt32 result = NativeMethods.LicenseInfo_IsValid(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static bool IsTemporary()
        {
            UInt32 result = NativeMethods.LicenseInfo_IsTemporary(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private static class NativeMethods
        {
            public static SetLicenseFileDelgate LicenseInfo_SetLicenseFile = LibraryInstance.GetFunction<SetLicenseFileDelgate>("LicenseInfo_SetLicenseFile");
            public static SetLicenseBufferDelgate LicenseInfo_SetLicenseBuffer = LibraryInstance.GetFunction<SetLicenseBufferDelgate>("LicenseInfo_SetLicenseBuffer");
            public static IsValidDelgate LicenseInfo_IsValid = LibraryInstance.GetFunction<IsValidDelgate>("LicenseInfo_IsValid");
            public static IsTemporaryDelgate LicenseInfo_IsTemporary = LibraryInstance.GetFunction<IsTemporaryDelgate>("InputStream_SetInputPosition");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetLicenseFileDelgate(string filename);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetLicenseBufferDelgate(PdfBufferSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IsValidDelgate(out bool data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IsTemporaryDelgate(out bool data);
        }
    }
}
