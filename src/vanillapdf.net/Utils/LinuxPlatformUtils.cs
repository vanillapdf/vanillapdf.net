using System;
using System.IO;
using System.Runtime.InteropServices;

namespace vanillapdf.net.Utils
{
    internal class LinuxPlatformUtils : IPlatformUtils
    {
        private const string LINUX_X86_LIBRARY_PATH = "runtimes/linux-x86/native/libvanillapdf.so";
        private const string LINUX_X64_LIBRARY_PATH = "runtimes/linux-x64/native/libvanillapdf.so";

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
