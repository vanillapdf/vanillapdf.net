using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net
{
    public class PdfXref : PdfUnknown
    {
        internal PdfXrefSafeHandle Handle { get; }

        internal PdfXref(PdfXrefSafeHandle handle)
        {
            Handle = handle;
        }

        static PdfXref()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public static PdfInputStream CreateFromFile(string filename)
        {
            UInt32 result = NativeMethods.InputStream_CreateFromFile(filename, out PdfInputStreamSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfInputStream(data);
        }


        protected override void ReleaseManagedResources()
        {
            Handle.Dispose();
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
