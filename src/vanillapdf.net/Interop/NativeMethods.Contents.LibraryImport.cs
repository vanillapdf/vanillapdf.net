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
        public static partial UInt32 ContentParser_Create(PdfFileSafeHandle sourceFile, PdfInputStreamSafeHandle inputStream, out PdfContentParserSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentParser_ReadInstructionCollection(PdfContentParserSafeHandle handle, out PdfContentInstructionCollectionSafeHandle data);

        #endregion

        #region ContentInstruction

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstruction_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstruction_GetInstructionType(PdfContentInstructionSafeHandle handle, out Int32 data);

        #endregion

        #region ContentInstructionCollection

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollection_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollection_GetSize(PdfContentInstructionCollectionSafeHandle handle, out UIntPtr data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollection_At(PdfContentInstructionCollectionSafeHandle handle, UIntPtr at, out PdfContentInstructionSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollection_Append(PdfContentInstructionCollectionSafeHandle handle, PdfContentInstructionSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollection_Insert(PdfContentInstructionCollectionSafeHandle handle, UIntPtr at, PdfContentInstructionSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollection_Remove(PdfContentInstructionCollectionSafeHandle handle, UIntPtr at);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollection_Clear(PdfContentInstructionCollectionSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollection_GetIterator(PdfContentInstructionCollectionSafeHandle handle, out PdfContentInstructionCollectionIteratorSafeHandle data);

        #endregion

        #region ContentInstructionCollectionIterator

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollectionIterator_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollectionIterator_GetValue(PdfContentInstructionCollectionIteratorSafeHandle handle, out PdfContentInstructionSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollectionIterator_Next(PdfContentInstructionCollectionIteratorSafeHandle handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentInstructionCollectionIterator_IsValid(PdfContentInstructionCollectionIteratorSafeHandle handle, [MarshalAs(UnmanagedType.I1)] out bool data);

        #endregion

        #region ContentOperator

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperator_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperator_GetOperatorType(PdfContentOperatorSafeHandle handle, out Int32 data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperator_GetValue(PdfContentOperatorSafeHandle handle, out PdfBufferSafeHandle data);

        #endregion

        #region ContentObject

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObject_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObject_ToInstruction(PdfContentObjectSafeHandle handle, out PdfContentInstructionSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObject_FromInstruction(PdfContentInstructionSafeHandle handle, out PdfContentObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObject_GetObjectType(PdfContentObjectSafeHandle handle, out Int32 data);

        #endregion

        #region ContentObjectText

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObjectText_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObjectText_ToContentObject(PdfContentObjectTextSafeHandle handle, out PdfContentObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObjectText_FromContentObject(PdfContentObjectSafeHandle handle, out PdfContentObjectTextSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObjectText_GetOperationsSize(PdfContentObjectTextSafeHandle handle, out UIntPtr data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentObjectText_GetOperationAt(PdfContentObjectTextSafeHandle handle, UIntPtr at, out PdfContentOperationSafeHandle data);

        #endregion

        #region ContentOperation

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperation_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperation_ToInstruction(PdfContentOperationSafeHandle handle, out PdfContentInstructionSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperation_FromInstruction(PdfContentInstructionSafeHandle handle, out PdfContentOperationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperation_GetOperationType(PdfContentOperationSafeHandle handle, out Int32 data);

        #endregion

        #region ContentOperationGeneric

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationGeneric_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationGeneric_ToContentOperation(PdfContentOperationGenericSafeHandle handle, out PdfContentOperationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationGeneric_FromContentOperation(PdfContentOperationSafeHandle handle, out PdfContentOperationGenericSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationGeneric_GetOperator(PdfContentOperationGenericSafeHandle handle, out PdfContentOperatorSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationGeneric_GetOperandsSize(PdfContentOperationGenericSafeHandle handle, out UIntPtr data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationGeneric_GetOperandAt(PdfContentOperationGenericSafeHandle handle, UIntPtr at, out PdfObjectSafeHandle data);

        #endregion

        #region ContentOperationTextShow

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShow_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShow_ToContentOperation(PdfContentOperationTextShowSafeHandle handle, out PdfContentOperationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShow_FromContentOperation(PdfContentOperationSafeHandle handle, out PdfContentOperationTextShowSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShow_GetValue(PdfContentOperationTextShowSafeHandle handle, out PdfStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShow_SetValue(PdfContentOperationTextShowSafeHandle handle, PdfStringObjectSafeHandle data);

        #endregion

        #region ContentOperationTextShowArray

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShowArray_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShowArray_ToContentOperation(PdfContentOperationTextShowArraySafeHandle handle, out PdfContentOperationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShowArray_FromContentOperation(PdfContentOperationSafeHandle handle, out PdfContentOperationTextShowArraySafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShowArray_GetValue(PdfContentOperationTextShowArraySafeHandle handle, out PdfArrayObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextShowArray_SetValue(PdfContentOperationTextShowArraySafeHandle handle, PdfArrayObjectSafeHandle data);

        #endregion

        #region ContentOperationTextFont

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextFont_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextFont_ToContentOperation(PdfContentOperationTextFontSafeHandle handle, out PdfContentOperationSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextFont_FromContentOperation(PdfContentOperationSafeHandle handle, out PdfContentOperationTextFontSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextFont_GetName(PdfContentOperationTextFontSafeHandle handle, out PdfNameObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextFont_SetName(PdfContentOperationTextFontSafeHandle handle, PdfNameObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextFont_GetScale(PdfContentOperationTextFontSafeHandle handle, out PdfRealObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 ContentOperationTextFont_SetScale(PdfContentOperationTextFontSafeHandle handle, PdfRealObjectSafeHandle data);

        #endregion

        #region BaseFontRange

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_Release(IntPtr handle);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_Create(out PdfBaseFontRangeSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_GetRangeLow(PdfBaseFontRangeSafeHandle handle, out PdfHexadecimalStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_SetRangeLow(PdfBaseFontRangeSafeHandle handle, PdfHexadecimalStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_GetRangeHigh(PdfBaseFontRangeSafeHandle handle, out PdfHexadecimalStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_SetRangeHigh(PdfBaseFontRangeSafeHandle handle, PdfHexadecimalStringObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_GetDestination(PdfBaseFontRangeSafeHandle handle, out PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_SetDestination(PdfBaseFontRangeSafeHandle handle, PdfObjectSafeHandle data);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_Contains(PdfBaseFontRangeSafeHandle handle, PdfBufferSafeHandle data, [MarshalAs(UnmanagedType.I1)] out bool result);

        [LibraryImport(LibraryName)]
        public static partial UInt32 BaseFontRange_GetMappedValue(PdfBaseFontRangeSafeHandle handle, PdfBufferSafeHandle data, out PdfBufferSafeHandle result);

        #endregion
    }
}

#endif
