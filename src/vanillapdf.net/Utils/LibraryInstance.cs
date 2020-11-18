using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace vanillapdf.net.Utils
{
    /// <summary>
    /// Class for managing native library
    /// </summary>
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

        /// <summary>
        /// Determine if the native library is already loaded in the memory
        /// </summary>
        /// <returns>true if loaded, false otherwise</returns>
        public static bool IsInitialized()
        {
            return (m_handle != null);
        }

        /// <summary>
        /// Loads native library from the current assembly path
        /// </summary>
        public static void Initialize()
        {
            // Root path of the entry assembly
            string rootAssemblyPath = Assembly.GetEntryAssembly().Location;
            string rootPath = Path.GetDirectoryName(rootAssemblyPath);

            Initialize(rootPath);
        }

        /// <summary>
        /// Loads native library from the specified folder
        /// </summary>
        /// <param name="rootPath">Folder to search for native library</param>
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

            throw new PdfManagedException("Unsupported platform");
        }

        /// <summary>
        /// Release native memory handle
        /// </summary>
        public static void Release()
        {
            // Not yet initialized
            if (m_handle == null) {
                return;
            }

            m_handle.ReleaseLibrary();
            m_handle = null;
        }

        /// <summary>
        /// Find procedure in the native library by it's name
        /// </summary>
        /// <typeparam name="T">Function delegate type</typeparam>
        /// <param name="procName">Name of the symbol exported by the native library</param>
        /// <returns>If the procedure is found the function returns delegate to specified function, otherwise throws Exception</returns>
        public static T GetFunction<T>(string procName)
        {
            IntPtr procAddress = Handle.GetProcAddress(procName);
            if (procAddress == IntPtr.Zero) {
                throw new PdfManagedException($"Could not find procedure {procName}");
            }

            return Marshal.GetDelegateForFunctionPointer<T>(procAddress);
        }

        /// <summary>
        /// Finds the constant symbol exported by the native library
        /// </summary>
        /// <param name="constantName">Name of the symbol exported by the native library</param>
        /// <returns>If the constant is found the function returns it's numeric value, otherwise throws Exception</returns>
        public static UInt32 GetConstant(string constantName)
        {
            IntPtr constantAddress = Handle.GetProcAddress(constantName);
            if (constantAddress == IntPtr.Zero) {
                throw new PdfManagedException($"Could not find procedure {constantName}");
            }

            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; ++i) {
                bytes[i] = Marshal.ReadByte(constantAddress, i);
            }

            return BitConverter.ToUInt32(bytes, 0);
        }
    }
}
