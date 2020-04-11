using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace vanillapdf.net.Utils
{
    public static class MiscUtils
    {
        public const CallingConvention LibraryCallingConvention = CallingConvention.Cdecl;

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        internal delegate UInt32 ConvertToUnknownDelegate<T>(T handle, out PdfUnknownSafeHandle data);

        public static void InitializeClasses()
        {
            // Utils
            RuntimeHelpers.RunClassConstructor(typeof(PdfUnknown).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfBuffer).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfInputStream).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfLogging).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfErrors).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfReturnValues).TypeHandle);

            // Syntax
            RuntimeHelpers.RunClassConstructor(typeof(PdfFile).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXref).TypeHandle);

            // Semtantics
            RuntimeHelpers.RunClassConstructor(typeof(PdfCatalog).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDocument).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageTree).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContents).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageAnnotations).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfAnnotation).TypeHandle);
        }
    }
}
