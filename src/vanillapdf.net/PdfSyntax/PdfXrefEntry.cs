using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public class PdfXrefEntry : PdfUnknown
    {
        internal PdfXrefEntry(PdfXrefEntrySafeHandle handle) : base(handle)
        {
        }

        static PdfXrefEntry()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        private static class NativeMethods
        {
        }
    }
}
