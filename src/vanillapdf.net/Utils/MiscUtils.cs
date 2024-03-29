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

            RuntimeHelpers.RunClassConstructor(typeof(PdfBaseObjectAttribute).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfImageMetadataObjectAttribute).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfObjectAttributeList).TypeHandle);

            // Semtantics
            RuntimeHelpers.RunClassConstructor(typeof(PdfDate).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfRectangle).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfDocument).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDocumentSignatureSettings).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfCatalog).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageTree).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageContents).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfPageAnnotations).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentInstruction).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfOutlineBase).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfOutline).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfOutlineItem).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfContentObject).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentObjectText).TypeHandle);

            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperation).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperationTextShow).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperationTextShowArray).TypeHandle);
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
