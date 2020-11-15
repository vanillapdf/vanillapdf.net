using System;
using System.IO;
using System.Runtime.InteropServices;

namespace vanillapdf.net.Utils
{
    internal class LinuxPlatformUtils : IPlatformUtils
    {
        private const string LINUX_X86_LIBRARY_PATH = "runtimes\\linux-x86\\native\\libvanillapdf.so";
        private const string LINUX_X64_LIBRARY_PATH = "runtimes\\linux-x64\\native\\libvanillapdf.so";

        private IntPtr Handle { get; set; }

        public void LoadLibrary(string rootPath)
        {
            // Ensure proper release of resources in subsequent calls
            ReleaseLibrary();

            // Find the correct library path depending on the process
            string libraryPath = Path.Combine(rootPath, LINUX_X86_LIBRARY_PATH);
            if (Environment.Is64BitProcess) {
                libraryPath = Path.Combine(rootPath, LINUX_X64_LIBRARY_PATH);
            }

            // Call the load library native function
            Handle = NativeMethods.dlopen(libraryPath, NativeMethods.RTLD_NOW_LINUX | NativeMethods.RTLD_LOCAL_LINUX);

            // Could not library or it's dependencies
            if (Handle == IntPtr.Zero) {
                IntPtr error = NativeMethods.dlerror();
                if (error == IntPtr.Zero) {
                    throw new Exception("Unable to load library");
                }
                else {
                    throw new Exception(string.Format("Unable to load library. Error detail: {0}", Marshal.PtrToStringAnsi(error)));
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
                    throw new Exception("Unable to release library");
                }
                else {
                    throw new Exception(string.Format("Unable to release library. Error detail: {0}", Marshal.PtrToStringAnsi(error)));
                }
            }

            Handle = IntPtr.Zero;
        }

        private static class NativeMethods
        {
            public const int RTLD_NOW_LINUX = 0x02;
            public const int RTLD_LOCAL_LINUX = 0;
            public const int RTLD_NOW_MACOSX = 0x02;
            public const int RTLD_LOCAL_MACOSX = 0x04;

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
