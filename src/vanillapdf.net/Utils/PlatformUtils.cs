using System;

namespace vanillapdf.net.Utils
{
    internal interface IPlatformUtils
    {
        void LoadLibrary(string rootPath);
        IntPtr GetProcAddress(string procName);
        void ReleaseLibrary();
    }
}
