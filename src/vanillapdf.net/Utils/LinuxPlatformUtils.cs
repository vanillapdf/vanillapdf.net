using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace vanillapdf.net.Utils
{
    internal class LinuxPlatformUtils : IPlatformUtils
    {
        private const string ROCKY_8_X64_LIBRARY_PATH = "runtimes/rocky.8-x64/native/libvanillapdf.so";

        private const string UBUTNTU_2004_X64_LIBRARY_PATH = "runtimes/ubuntu.20.04-x64/native/libvanillapdf.so";
        private const string UBUTNTU_2004_ARM_LIBRARY_PATH = "runtimes/ubuntu.20.04-arm/native/libvanillapdf.so";
        private const string UBUTNTU_2004_ARM64_LIBRARY_PATH = "runtimes/ubuntu.20.04-arm64/native/libvanillapdf.so";

        private const string UBUTNTU_2204_X64_LIBRARY_PATH = "runtimes/ubuntu.22.04-x64/native/libvanillapdf.so";
        private const string UBUTNTU_2204_ARM_LIBRARY_PATH = "runtimes/ubuntu.22.04-arm/native/libvanillapdf.so";
        private const string UBUTNTU_2204_ARM64_LIBRARY_PATH = "runtimes/ubuntu.22.04-arm64/native/libvanillapdf.so";

        private IntPtr Handle { get; set; }

        public void LoadLibrary(string rootPath)
        {
            // Ensure proper release of resources in subsequent calls
            ReleaseLibrary();

            string libraryPath = null;
            if (RuntimeInformation.ProcessArchitecture == Architecture.X64) {
                libraryPath = UBUTNTU_2004_X64_LIBRARY_PATH;

                if (IsUbuntu2204()) {
                    libraryPath = UBUTNTU_2204_X64_LIBRARY_PATH;
                }

                if (IsRhel()) {
                    libraryPath = ROCKY_8_X64_LIBRARY_PATH;
                }
            }

            if (RuntimeInformation.ProcessArchitecture == Architecture.Arm) {
                libraryPath = UBUTNTU_2004_ARM_LIBRARY_PATH;

                if (IsUbuntu2204()) {
                    libraryPath = UBUTNTU_2204_ARM_LIBRARY_PATH;
                }
            }

            if (RuntimeInformation.ProcessArchitecture == Architecture.Arm64) {
                libraryPath = UBUTNTU_2004_ARM64_LIBRARY_PATH;

                if (IsUbuntu2204()) {
                    libraryPath = UBUTNTU_2204_ARM64_LIBRARY_PATH;
                }
            }

            if (libraryPath == null) {
                throw new PdfManagedException($"Process architecture {RuntimeInformation.ProcessArchitecture} is not supported");
            }

            // Call the load library native function
            Handle = NativeMethods.dlopen(libraryPath, NativeMethods.RTLD_NOW_LINUX | NativeMethods.RTLD_LOCAL_LINUX);

            // Could not load library or it's dependencies
            if (Handle == IntPtr.Zero) {
                IntPtr error = NativeMethods.dlerror();
                if (error == IntPtr.Zero) {
                    throw new PdfManagedException($"Unable to load library: \"{libraryPath}\"");
                }
                else {
                    throw new PdfManagedException(string.Format("Unable to load library. Error detail: {0}", Marshal.PtrToStringAnsi(error)));
                }
            }
        }

        public IntPtr GetProcAddress(string procName)
        {
            return NativeMethods.dlsym(Handle, procName);
        }

        public void ReleaseLibrary()
        {
            // Not yet initialized
            if (Handle == IntPtr.Zero) {
                return;
            }

            int closed = NativeMethods.dlclose(Handle);
            if (closed != 0) {
                IntPtr error = NativeMethods.dlerror();
                if (error == IntPtr.Zero) {
                    throw new PdfManagedException("Unable to release library");
                }
                else {
                    throw new PdfManagedException(string.Format("Unable to release library. Error detail: {0}", Marshal.PtrToStringAnsi(error)));
                }
            }

            Handle = IntPtr.Zero;
        }

        private bool IsUbuntu2204()
        {
            if (File.Exists("/etc/os-release")) {
                var releaseString = File.ReadAllText("/etc/os-release");

                Match idMatch = Regex.Match(releaseString, "^ID=\"?(\\w*?)\"?$", RegexOptions.Multiline);
                Match versionMatch = Regex.Match(releaseString, "^VERSION_ID=\"(.*)\"$", RegexOptions.Multiline);

                // Failed to find ID in os-release file
                if (!idMatch.Success || idMatch.Groups.Count != 2 || idMatch.Groups[1].Captures.Count != 1) {
                    return false;
                }

                // Failed to find VERSION_ID in os-release file
                if (!versionMatch.Success || versionMatch.Groups.Count != 2 || versionMatch.Groups[1].Captures.Count != 1) {
                    return false;
                }

                string idValue = idMatch.Groups[1].Captures[0].Value;
                string versionValue = versionMatch.Groups[1].Captures[0].Value;

                // Current distro is not ubuntu
                if (idValue != "ubuntu") {
                    return false;
                }

                int versionCompareResult = String.Compare(versionValue, "22.04", comparisonType: StringComparison.Ordinal);
                if (versionCompareResult >= 0) {
                    return true;
                }

            }

            return false;
        }

        private bool IsRhel()
        {
            if (File.Exists("/etc/os-release")) {
                var releaseString = File.ReadAllText("/etc/os-release");

                Match idMatch = Regex.Match(releaseString, "^ID=\"?(\\w*?)\"?$", RegexOptions.Multiline);
                Match versionMatch = Regex.Match(releaseString, "^VERSION_ID=\"(.*)\"$", RegexOptions.Multiline);

                // Failed to find ID in os-release file
                if (!idMatch.Success || idMatch.Groups.Count != 2 || idMatch.Groups[1].Captures.Count != 1) {
                    return false;
                }

                // Failed to find VERSION_ID in os-release file
                if (!versionMatch.Success || versionMatch.Groups.Count != 2 || versionMatch.Groups[1].Captures.Count != 1) {
                    return false;
                }

                string idValue = idMatch.Groups[1].Captures[0].Value;
                string versionValue = versionMatch.Groups[1].Captures[0].Value;

                // Current distro is not ubuntu
                if (idValue == "rhel" || idValue == "rocky") {
                    return true;
                }

            }

            // Such file should be present only on RHEL
            if (File.Exists("/etc/redhat-release")) {
                return true;
            }

            return false;
        }

        private static class NativeMethods
        {
            public const int RTLD_NOW_LINUX = 0x02;
            public const int RTLD_LOCAL_LINUX = 0;
            public const int RTLD_NOW_MACOSX = 0x02;
            public const int RTLD_LOCAL_MACOSX = 0x04;

            public delegate IntPtr dlerror_delegate();
            public delegate IntPtr dlopen_delegate(string filename, int flag);
            public delegate int dlclose_delegate(IntPtr handle);
            public delegate IntPtr dlsym_delegate(IntPtr handle, string symbol);

            public static dlerror_delegate dlerror;
            public static dlopen_delegate dlopen;
            public static dlclose_delegate dlclose;
            public static dlsym_delegate dlsym;

            static NativeMethods()
            {
                try {
                    Marshal.PrelinkAll(typeof(libc_NativeMethods));

                    dlerror = libc_NativeMethods.dlerror;
                    dlopen = libc_NativeMethods.dlopen;
                    dlclose = libc_NativeMethods.dlclose;
                    dlsym = libc_NativeMethods.dlsym;

                    return;
                } catch {

                }

                try {
                    Marshal.PrelinkAll(typeof(libdl_NativeMethods));

                    dlerror = libdl_NativeMethods.dlerror;
                    dlopen = libdl_NativeMethods.dlopen;
                    dlclose = libdl_NativeMethods.dlclose;
                    dlsym = libdl_NativeMethods.dlsym;

                    return;
                }
                catch {

                }

                throw new PdfManagedException("No valid dynamic link support library was found");
            }
        }

        private static class libc_NativeMethods
        {
            [DllImport("libc", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr dlerror();

            [DllImport("libc", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern IntPtr dlopen(string filename, int flag);

            [DllImport("libc", CallingConvention = CallingConvention.Cdecl)]
            public static extern int dlclose(IntPtr handle);

            [DllImport("libc", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern IntPtr dlsym(IntPtr handle, string symbol);
        }

        private static class libdl_NativeMethods
        {
            [DllImport("libdl", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr dlerror();

            [DllImport("libdl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern IntPtr dlopen(string filename, int flag);

            [DllImport("libdl", CallingConvention = CallingConvention.Cdecl)]
            public static extern int dlclose(IntPtr handle);

            [DllImport("libdl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern IntPtr dlsym(IntPtr handle, string symbol);
        }
    }
}
