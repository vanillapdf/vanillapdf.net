using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Interop;

namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Information about the native Vanilla.PDF library version, build and author.
    /// </summary>
    public static class PdfLibraryInfo
    {
        /// <summary>
        /// Library major version.
        /// A change in the major version indicates backward incompatibility.
        /// </summary>
        public static int VersionMajor {
            get { return GetVersionMajor(); }
        }

        /// <summary>
        /// Library minor version.
        /// A change in the minor version indicates backward compatibility, while some new features were added.
        /// </summary>
        public static int VersionMinor {
            get { return GetVersionMinor(); }
        }

        /// <summary>
        /// Library patch version.
        /// A change in the patch version indicates no interface changes, only bugfixes.
        /// </summary>
        public static int VersionPatch {
            get { return GetVersionPatch(); }
        }

        /// <summary>
        /// Library build version.
        /// A change in the build version should not affect any kind of functionality.
        /// </summary>
        public static int VersionBuild {
            get { return GetVersionBuild(); }
        }

        /// <summary>
        /// Library author name.
        /// </summary>
        public static string Author {
            get { return GetAuthor(); }
        }

        /// <summary>
        /// Day of month when the library was built.
        /// </summary>
        public static int BuildDay {
            get { return GetBuildDay(); }
        }

        /// <summary>
        /// Month of the year when the library was built.
        /// </summary>
        public static int BuildMonth {
            get { return GetBuildMonth(); }
        }

        /// <summary>
        /// Year when the library was built.
        /// </summary>
        public static int BuildYear {
            get { return GetBuildYear(); }
        }

        private static int GetVersionMajor()
        {
            UInt32 result = NativeMethods.LibraryInfo_GetVersionMajor(out int data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private static int GetVersionMinor()
        {
            UInt32 result = NativeMethods.LibraryInfo_GetVersionMinor(out int data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private static int GetVersionPatch()
        {
            UInt32 result = NativeMethods.LibraryInfo_GetVersionPatch(out int data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private static int GetVersionBuild()
        {
            UInt32 result = NativeMethods.LibraryInfo_GetVersionBuild(out int data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private static string GetAuthor()
        {
            UInt32 result = NativeMethods.LibraryInfo_GetAuthor(out IntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            // The author is a static, null-terminated ASCII string owned by the
            // native library, so the memory must not be freed here.
            return Marshal.PtrToStringAnsi(data);
        }

        private static int GetBuildDay()
        {
            UInt32 result = NativeMethods.LibraryInfo_GetBuildDay(out int data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private static int GetBuildMonth()
        {
            UInt32 result = NativeMethods.LibraryInfo_GetBuildMonth(out int data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private static int GetBuildYear()
        {
            UInt32 result = NativeMethods.LibraryInfo_GetBuildYear(out int data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }
}
