using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Class for managing licensed features
    /// </summary>
    public static class PdfLicenseInfo
    {
        static PdfLicenseInfo()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        /// <summary>
        /// Set license from file
        /// </summary>
        /// <param name="filename">path to file containing license information</param>
        public static void SetLicenseFile(string filename)
        {
            UInt32 result = NativeMethods.LicenseInfo_SetLicenseFile(filename);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Set license from buffer
        /// </summary>
        /// <param name="data">buffer containing license information</param>
        public static void SetLicenseBuffer(PdfBuffer data)
        {
            UInt32 result = NativeMethods.LicenseInfo_SetLicenseBuffer(data.Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Check if the presented license is valid
        /// </summary>
        /// <returns>true if the license is valid, false otherwise</returns>
        public static bool IsValid()
        {
            UInt32 result = NativeMethods.LicenseInfo_IsValid(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        /// <summary>
        /// Check if the license is temporary
        /// Temporary license means it is only for a limited time-frame
        /// </summary>
        /// <returns>true if the license is temporary, false otherwise</returns>
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
            public static IsTemporaryDelgate LicenseInfo_IsTemporary = LibraryInstance.GetFunction<IsTemporaryDelgate>("LicenseInfo_IsTemporary");

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
