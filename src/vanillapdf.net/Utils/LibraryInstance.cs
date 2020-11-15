using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace vanillapdf.net.Utils
{
    public static class LibraryInstance
    {
        private static IPlatformUtils m_handle;
        internal static IPlatformUtils Handle
        {
            get
            {
                if (m_handle == null) {
                    Initialize();
                }

                return m_handle;
            }

            private set
            {
                m_handle = value;
            }
        }

        public static bool IsInitialized()
        {
            return (m_handle != null);
        }

        public static void Initialize()
        {
            // Root path of the entry assembly
            string rootAssemblyPath = Assembly.GetEntryAssembly().Location;
            string rootPath = Path.GetDirectoryName(rootAssemblyPath);

            Initialize(rootPath);
        }

        public static void Initialize(string rootPath)
        {
            // Already initialized
            if (m_handle != null) {
                return;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                m_handle = new WindowsPlatformUtils();
                m_handle.LoadLibrary(rootPath);
                return;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                m_handle = new LinuxPlatformUtils();
                m_handle.LoadLibrary(rootPath);
                return;
            }

            //if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
            //}

            throw new Exception("Unsupported platform");
        }

        public static void Release()
        {
            // Not yet initialized
            if (m_handle == null) {
                return;
            }

            m_handle.ReleaseLibrary();
            m_handle = null;
        }

        public static T GetFunction<T>(string procName)
        {
            IntPtr procAddress = Handle.GetProcAddress(procName);
            if (procAddress == IntPtr.Zero) {
                throw new Exception($"Could not find procedure {procName}");
            }

            return Marshal.GetDelegateForFunctionPointer<T>(procAddress);
        }

        public static UInt32 GetConstant(string constantName)
        {
            IntPtr constantAddress = Handle.GetProcAddress(constantName);
            if (constantAddress == IntPtr.Zero) {
                throw new Exception($"Could not find procedure {constantName}");
            }

            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; ++i) {
                bytes[i] = Marshal.ReadByte(constantAddress, i);
            }

            return BitConverter.ToUInt32(bytes, 0);
        }
    }
}
