using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public class PdfDictionaryObject : PdfObject
    {
        internal new PdfDictionaryObjectSafeHandle Handle { get; }

        internal PdfDictionaryObject(PdfDictionaryObjectSafeHandle handle)
        {
            Handle = handle;
        }

        private static PdfObjectSafeHandle ToPdfObjectSafeHandle(PdfDictionaryObjectSafeHandle handle)
        {
            return new PdfObjectSafeHandle();
        }

        static PdfDictionaryObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        protected override void ReleaseManagedResources()
        {
            Handle.Dispose();
        }

        private static class NativeMethods
        {
            public static GetTypeDelgate Object_GetType = LibraryInstance.GetFunction<GetTypeDelgate>("Object_GetType");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetTypeDelgate(PdfObjectSafeHandle handle, out PdfObjectType data);
        }
    }
}
