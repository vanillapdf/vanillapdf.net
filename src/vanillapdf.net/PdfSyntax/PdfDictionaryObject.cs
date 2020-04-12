using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public class PdfDictionaryObject : PdfObject
    {
        internal PdfDictionaryObject(PdfDictionaryObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfDictionaryObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        private static class NativeMethods
        {
        }
    }
}
