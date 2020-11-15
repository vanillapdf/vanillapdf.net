using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfContents;
using vanillapdf.net.PdfSemantics;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    internal static class MiscUtils
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
            RuntimeHelpers.RunClassConstructor(typeof(PdfPKCS12Key).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfInputStream).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfOutputStream).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfInputOutputStream).TypeHandle);

            // Syntax
            RuntimeHelpers.RunClassConstructor(typeof(PdfFile).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfFileWriter).TypeHandle);
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
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageContents).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageAnnotations).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentInstruction).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfContentObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentObjectText).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperation).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperationTextShow).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperationTextShowArray).TypeHandle);

            // Safe handles
            RuntimeHelpers.RunClassConstructor(typeof(PdfUnknownSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfBufferSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfSigningKeySafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPKCS12KeySafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfInputStreamSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfOutputStreamSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfInputOutputStreamSafeHandle).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfFileSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfFileWriterSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefChainSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefChainIteratorSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefIteratorSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefEntrySafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefFreeEntrySafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefUsedEntrySafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfXrefCompressedEntrySafeHandle).TypeHandle);

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

            RuntimeHelpers.RunClassConstructor(typeof(PdfCatalogSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageTreeSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageContentsSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageAnnotationsSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentInstructionSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentObjectSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentObjectTextSafeHandle).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperationSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperationTextShowSafeHandle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperationTextShowArraySafeHandle).TypeHandle);
        }

        public static UInt64 PlatformIntegerConversion(int value)
        {
            if (Environment.Is64BitProcess) {
                return (UInt64)value;
            } else {
                return (UInt32)value;
            }
        }
    }
}
