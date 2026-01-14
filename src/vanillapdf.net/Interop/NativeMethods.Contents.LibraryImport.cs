#if NET7_0_OR_GREATER

using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.Interop
{
    internal static partial class NativeMethods
    {
        #region ContentParser

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentParser_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentParser_ToUnknown(PdfContentParserSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentParser_FromUnknown(PdfUnknownSafeHandle handle, out PdfContentParserSafeHandle data);

        #endregion

        #region ContentInstruction

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstruction_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstruction_ToUnknown(PdfContentInstructionSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstruction_FromUnknown(PdfUnknownSafeHandle handle, out PdfContentInstructionSafeHandle data);

        #endregion

        #region ContentInstructionCollection

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollection_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollection_ToUnknown(PdfContentInstructionCollectionSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollection_FromUnknown(PdfUnknownSafeHandle handle, out PdfContentInstructionCollectionSafeHandle data);

        #endregion

        #region ContentInstructionCollectionIterator

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollectionIterator_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollectionIterator_ToUnknown(PdfContentInstructionCollectionIteratorSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollectionIterator_FromUnknown(PdfUnknownSafeHandle handle, out PdfContentInstructionCollectionIteratorSafeHandle data);

        #endregion

        #region ContentOperator

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperator_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperator_ToUnknown(PdfContentOperatorSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperator_FromUnknown(PdfUnknownSafeHandle handle, out PdfContentOperatorSafeHandle data);

        #endregion

        #region ContentObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObject_ToInstruction(PdfContentObjectSafeHandle handle, out PdfContentInstructionSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObject_FromInstruction(PdfContentInstructionSafeHandle handle, out PdfContentObjectSafeHandle data);

        #endregion

        #region ContentObjectText

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObjectText_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObjectText_ToContentObject(PdfContentObjectTextSafeHandle handle, out PdfContentObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObjectText_FromContentObject(PdfContentObjectSafeHandle handle, out PdfContentObjectTextSafeHandle data);

        #endregion

        #region ContentOperation

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperation_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperation_ToInstruction(PdfContentOperationSafeHandle handle, out PdfContentInstructionSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperation_FromInstruction(PdfContentInstructionSafeHandle handle, out PdfContentOperationSafeHandle data);

        #endregion

        #region ContentOperationGeneric

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationGeneric_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationGeneric_ToContentOperation(PdfContentOperationGenericSafeHandle handle, out PdfContentOperationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationGeneric_FromContentOperation(PdfContentOperationSafeHandle handle, out PdfContentOperationGenericSafeHandle data);

        #endregion

        #region ContentOperationTextShow

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShow_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShow_ToContentOperation(PdfContentOperationTextShowSafeHandle handle, out PdfContentOperationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShow_FromContentOperation(PdfContentOperationSafeHandle handle, out PdfContentOperationTextShowSafeHandle data);

        #endregion

        #region ContentOperationTextShowArray

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShowArray_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShowArray_ToContentOperation(PdfContentOperationTextShowArraySafeHandle handle, out PdfContentOperationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShowArray_FromContentOperation(PdfContentOperationSafeHandle handle, out PdfContentOperationTextShowArraySafeHandle data);

        #endregion

        #region ContentOperationTextFont

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextFont_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextFont_ToContentOperation(PdfContentOperationTextFontSafeHandle handle, out PdfContentOperationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextFont_FromContentOperation(PdfContentOperationSafeHandle handle, out PdfContentOperationTextFontSafeHandle data);

        #endregion

        #region BaseFontRange

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_ToUnknown(PdfBaseFontRangeSafeHandle handle, out PdfUnknownSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_FromUnknown(PdfUnknownSafeHandle handle, out PdfBaseFontRangeSafeHandle data);

        #endregion
    }
}

#endif
