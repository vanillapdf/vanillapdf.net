using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.nunit
{
    [SetUpFixture]
    public class OneTimeSetup
    {
        public const int STABILITY_REPEAT_COUNT = 1000;
        public static readonly UInt64 PLATFORM_MAXIMUM_VALUE;

        public static readonly List<string> TEST_DOCUMENTS = new List<string>() {
            Path.Combine("Resources", "minimalist.pdf"),
            Path.Combine("Resources", "19005-1_FAQ.PDF")
        };

        static OneTimeSetup()
        {
            if (Environment.Is64BitProcess) {
                PLATFORM_MAXIMUM_VALUE = UInt64.MaxValue;
            } else {
                PLATFORM_MAXIMUM_VALUE = UInt32.MaxValue;
            }
        }

        [OneTimeSetUp]
        public static void InitializeLibrary()
        {
            var testDirectory = TestContext.CurrentContext.TestDirectory;

            // For .NET 7+, set up the DllImportResolver to load from test directory
            LibraryInstance.SetDllImportResolver((libraryName, assembly, searchPath) =>
            {
                string libraryPath = GetLibraryPath(testDirectory, libraryName);
                if (File.Exists(libraryPath)) {
                    return NativeLibrary.Load(libraryPath);
                }
                return IntPtr.Zero;
            });

            // Still need to initialize for constants and other platform-specific setup
            LibraryInstance.Initialize(testDirectory);
        }

        private static string GetLibraryPath(string rootPath, string libraryName)
        {
            string rid = RuntimeInformation.RuntimeIdentifier;
            string runtimesPath = Path.Combine(rootPath, "runtimes", rid, "native");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                // Try RID-specific path first
                string ridPath = Path.Combine(runtimesPath, $"lib{libraryName}.dll");
                if (File.Exists(ridPath)) return ridPath;

                // Fallback paths for different architectures
                string arch = Environment.Is64BitProcess ? "win-x64" : "win-x86";
                ridPath = Path.Combine(rootPath, "runtimes", arch, "native", $"lib{libraryName}.dll");
                if (File.Exists(ridPath)) return ridPath;

                // Direct path
                return Path.Combine(rootPath, $"{libraryName}.dll");
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                string ridPath = Path.Combine(runtimesPath, $"lib{libraryName}.so");
                if (File.Exists(ridPath)) return ridPath;

                string arch = RuntimeInformation.ProcessArchitecture == Architecture.Arm64 ? "linux-arm64" : "linux-x64";
                ridPath = Path.Combine(rootPath, "runtimes", arch, "native", $"lib{libraryName}.so");
                if (File.Exists(ridPath)) return ridPath;

                return Path.Combine(rootPath, $"lib{libraryName}.so");
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                string ridPath = Path.Combine(runtimesPath, $"lib{libraryName}.dylib");
                if (File.Exists(ridPath)) return ridPath;

                string arch = RuntimeInformation.ProcessArchitecture == Architecture.Arm64 ? "osx-arm64" : "osx-x64";
                ridPath = Path.Combine(rootPath, "runtimes", arch, "native", $"lib{libraryName}.dylib");
                if (File.Exists(ridPath)) return ridPath;

                return Path.Combine(rootPath, $"lib{libraryName}.dylib");
            }
            return Path.Combine(rootPath, libraryName);
        }
    }
}
