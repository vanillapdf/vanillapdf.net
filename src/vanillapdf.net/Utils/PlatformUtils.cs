using System;

namespace vanillapdf.net.Utils
{
    public interface IPlatformUtils
    {
        void LoadLibrary(string rootPath);
        IntPtr GetProcAddress(string procName);
        void ReleaseLibrary();
    }
}
