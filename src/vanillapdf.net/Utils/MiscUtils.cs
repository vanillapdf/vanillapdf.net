using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    public static class MiscUtils
    {
        public const CallingConvention LibraryCallingConvention = CallingConvention.Cdecl;

        // NOTE:
        // There is some issue with templated delegates
        // It possible to compile, but it fails in runtime with error

        //[UnmanagedFunctionPointer(LibraryCallingConvention)]
        //internal delegate UInt32 ConvertToDelegate<T, U>(T handle, out U data);

        //[UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        //internal delegate UInt32 ConvertFromDelegate<T, U>(U handle, out T data);

        public static void InitializeClasses()
        {
            // Utils
            RuntimeHelpers.RunClassConstructor(typeof(PdfLogging).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfErrors).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfReturnValues).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfUnknown).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfBuffer).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfSigningKey).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfInputStream).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfOutputStream).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfInputOutputStream).TypeHandle);

            // Syntax
            RuntimeHelpers.RunClassConstructor(typeof(PdfFile).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefChain).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefChainIterator).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXref).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefIterator).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefEntry).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefFreeEntry).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefUsedEntry).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefCompressedEntry).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfIntegerObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfBooleanObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDictionaryObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDictionaryObjectIterator).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfIndirectReferenceObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfNameObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfNullObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfRealObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfStreamObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfStringObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfLiteralStringObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfHexadecimalStringObject).TypeHandle);

            // Semtantics
            RuntimeHelpers.RunClassConstructor(typeof(PdfDocument).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDocumentSignatureSettings).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfCatalog).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageTree).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContents).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageAnnotations).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfAnnotation).TypeHandle);

            // Safe handles
            RuntimeHelpers.RunClassConstructor(typeof(PdfUnknownSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfBufferSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfSigningKeySafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfInputStreamSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfOutputStreamSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfInputOutputStreamSafeHandle).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfFile).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefChain).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefChainIterator).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXref).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefIterator).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefEntry).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefFreeEntry).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefUsedEntry).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefCompressedEntry).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfIntegerObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfBooleanObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDictionaryObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDictionaryObjectIteratorSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfIndirectReferenceObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfNameObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfNullObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfRealObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfStreamObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfStringObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfLiteralStringObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfHexadecimalStringObjectSafeHandle).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfDocumentSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDocumentSignatureSettingsSafeHandle).TypeHandle);
            //RuntimeHelpers.RunClassConstructor(typeof(PdfAnnotationSafeHandle).TypeHandle);
            //RuntimeHelpers.RunClassConstructor(typeof(PdfCatalogSafeHandle).TypeHandle);
            //RuntimeHelpers.RunClassConstructor(typeof(PdfContentsSafeHandle).TypeHandle);
            //RuntimeHelpers.RunClassConstructor(typeof(PdfPageAnnotationsSafeHandle).TypeHandle);
            //RuntimeHelpers.RunClassConstructor(typeof(PdfPageObjectSafeHandle).TypeHandle);
            //RuntimeHelpers.RunClassConstructor(typeof(PdfPageTreeSafeHandle).TypeHandle);
        }
    }
}
