using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace vanillapdf.net.Utils
{
    internal class WindowsPlatformUtils : IPlatformUtils
    {
        private const string WIN_X86_LIBRARY_PATH = "runtimes\\win-x86\\native\\libvanillapdf.dll";
        private const string WIN_X64_LIBRARY_PATH = "runtimes\\win-x64\\native\\libvanillapdf.dll";

        private IntPtr Handle { get; set; }

        public void LoadLibrary(string rootPath)
        {
            // Ensure proper release of resources in subsequent calls
            ReleaseLibrary();

            // Find the correct library path depending on the process
            string libraryPath = Path.Combine(rootPath, WIN_X86_LIBRARY_PATH);
            if (Environment.Is64BitProcess) {
                libraryPath = Path.Combine(rootPath, WIN_X64_LIBRARY_PATH);
            }

            // Call the load library native function
            Handle = NativeMethods.LoadLibraryEx(libraryPath, IntPtr.Zero, NativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR | NativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_SYSTEM32);

            // Could not library or it's dependencies
            if (Handle == IntPtr.Zero) {
                int error = Marshal.GetLastWin32Error();
                throw new Win32Exception(error);
            }
        }

        public IntPtr GetProcAddress(string procName)
        {
            return NativeMethods.GetProcAddress(Handle, procName);
        }

        public void ReleaseLibrary()
        {
            // Not yet initialized
            if (Handle == IntPtr.Zero) {
                return;
            }

            bool freed = NativeMethods.FreeLibrary(Handle);
            if (!freed) {
                int error = Marshal.GetLastWin32Error();
                throw new Win32Exception(error);
            }

            Handle = IntPtr.Zero;
        }

        private static class NativeMethods
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool FreeLibrary(IntPtr hModule);

            [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

            [Flags]
            public enum LoadLibraryFlags : uint
            {
                DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
                LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
                LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
                LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
                LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
                LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
                LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
                LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
                LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
                LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
                LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
            }
        }
    }
}
