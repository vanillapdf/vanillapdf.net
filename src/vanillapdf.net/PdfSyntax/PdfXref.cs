using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public class PdfXref : PdfUnknown
    {
        internal PdfXref(PdfXrefSafeHandle handle) : base(handle)
        {
        }

        static PdfXref()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        private static class NativeMethods
        {
            public static LastXrefOffsetDelgate Xref_LastXrefOffset = LibraryInstance.GetFunction<LastXrefOffsetDelgate>("Xref_LastXrefOffset");

            //[UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            //public delegate UInt32 XrefTrailerDictionaryDelgate(PdfXref handle, out PdfDict);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 LastXrefOffsetDelgate(PdfXrefSafeHandle handle, out Int64 data);
        }
    }
}
