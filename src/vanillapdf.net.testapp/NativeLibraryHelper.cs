using System;
using System.IO;
using System.Runtime.InteropServices;

namespace vanillapdf.net.testapp
{
    /// <summary>
    /// Helper class to configure native library resolution.
    /// This is a workaround until the runtime packages properly copy native libraries
    /// to the output root for .NET Framework.
    /// See: https://github.com/vanillapdf/vanillapdf/issues/219
    /// </summary>
    internal static class NativeLibraryHelper
    {
#if NETFRAMEWORK
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool SetDllDirectory(string lpPathName);

        public static void Initialize()
        {
            // .NET Framework doesn't probe runtimes/<rid>/native/ automatically.
            // Add the appropriate runtime directory to the DLL search path.
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var rid = Environment.Is64BitProcess ? "win-x64" : "win-x86";
            var nativePath = Path.Combine(baseDir, "runtimes", rid, "native");

            if (Directory.Exists(nativePath))
            {
                SetDllDirectory(nativePath);
                Console.WriteLine($"[.NET Framework] Added native library path: {nativePath}");
            }
        }
#else
        public static void Initialize()
        {
            // .NET Core/.NET 5+ probes runtimes/<rid>/native/ automatically.
            // No action needed.
        }
#endif
    }
}
