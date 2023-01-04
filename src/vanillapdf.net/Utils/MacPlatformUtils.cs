using System;
using System.Runtime.InteropServices;

namespace vanillapdf.net.Utils
{
    internal class MacPlatformUtils : IPlatformUtils
    {
        private const string MAC_X64_LIBRARY_PATH = "runtimes/osx-x64/native/libvanillapdf.dylib";

        private IntPtr Handle { get; set; }

        public void LoadLibrary(string rootPath)
        {
            // Ensure proper release of resources in subsequent calls
            ReleaseLibrary();

            string libraryPath = null;
            if (RuntimeInformation.ProcessArchitecture == Architecture.X64) {
                libraryPath = MAC_X64_LIBRARY_PATH;
            }

            if (libraryPath == null) {
                throw new PdfManagedException($"Process architecture {RuntimeInformation.ProcessArchitecture} is not supported");
            }

            // Call the load library native function
            Handle = NativeMethods.dlopen(libraryPath, NativeMethods.RTLD_NOW_MACOSX | NativeMethods.RTLD_LOCAL_MACOSX);

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

        private static class NativeMethods
        {
            public const int RTLD_NOW_MACOSX = 0x02;
            public const int RTLD_LOCAL_MACOSX = 0x04;

            [DllImport("libdl.dylib", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr dlerror();

            [DllImport("libdl.dylib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern IntPtr dlopen(string filename, int flag);

            [DllImport("libdl.dylib", CallingConvention = CallingConvention.Cdecl)]
            public static extern int dlclose(IntPtr handle);

            [DllImport("libdl.dylib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern IntPtr dlsym(IntPtr handle, string symbol);
        }
    }
}
