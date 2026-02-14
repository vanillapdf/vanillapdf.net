#if NETSTANDARD2_0

using System;
using System.Runtime.InteropServices;
using vanillapdf.net.Utils;

namespace vanillapdf.net.Interop
{
    internal static partial class NativeMethods
    {
        #region ContentParser

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentParser_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentParser_Create(PdfFileSafeHandle sourceFile, PdfInputStreamSafeHandle inputStream, out PdfContentParserSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentParser_ReadInstructionCollection(PdfContentParserSafeHandle handle, out PdfContentInstructionCollectionSafeHandle data);

        #endregion

        #region ContentInstruction

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstruction_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstruction_GetInstructionType(PdfContentInstructionSafeHandle handle, out Int32 data);

        #endregion

        #region ContentInstructionCollection

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstructionCollection_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstructionCollection_GetSize(PdfContentInstructionCollectionSafeHandle handle, out UIntPtr data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstructionCollection_At(PdfContentInstructionCollectionSafeHandle handle, UIntPtr at, out PdfContentInstructionSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstructionCollection_Append(PdfContentInstructionCollectionSafeHandle handle, PdfContentInstructionSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstructionCollection_Insert(PdfContentInstructionCollectionSafeHandle handle, UIntPtr at, PdfContentInstructionSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstructionCollection_Remove(PdfContentInstructionCollectionSafeHandle handle, UIntPtr at);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstructionCollection_Clear(PdfContentInstructionCollectionSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstructionCollection_GetIterator(PdfContentInstructionCollectionSafeHandle handle, out PdfContentInstructionCollectionIteratorSafeHandle data);

        #endregion

        #region ContentInstructionCollectionIterator

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstructionCollectionIterator_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstructionCollectionIterator_GetValue(PdfContentInstructionCollectionIteratorSafeHandle handle, out PdfContentInstructionSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentInstructionCollectionIterator_Next(PdfContentInstructionCollectionIteratorSafeHandle handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern UInt32 ContentInstructionCollectionIterator_IsValid(PdfContentInstructionCollectionIteratorSafeHandle handle, [MarshalAs(UnmanagedType.I1)] out bool data);

        #endregion

        #region ContentOperator

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperator_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperator_GetOperatorType(PdfContentOperatorSafeHandle handle, out Int32 data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperator_GetValue(PdfContentOperatorSafeHandle handle, out PdfBufferSafeHandle data);

        #endregion

        #region ContentObject

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentObject_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentObject_ToInstruction(PdfContentObjectSafeHandle handle, out PdfContentInstructionSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentObject_FromInstruction(PdfContentInstructionSafeHandle handle, out PdfContentObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentObject_GetObjectType(PdfContentObjectSafeHandle handle, out Int32 data);

        #endregion

        #region ContentObjectText

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentObjectText_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentObjectText_ToContentObject(PdfContentObjectTextSafeHandle handle, out PdfContentObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentObjectText_FromContentObject(PdfContentObjectSafeHandle handle, out PdfContentObjectTextSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentObjectText_GetOperationsSize(PdfContentObjectTextSafeHandle handle, out UIntPtr data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentObjectText_GetOperationAt(PdfContentObjectTextSafeHandle handle, UIntPtr at, out PdfContentOperationSafeHandle data);

        #endregion

        #region ContentOperation

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperation_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperation_ToInstruction(PdfContentOperationSafeHandle handle, out PdfContentInstructionSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperation_FromInstruction(PdfContentInstructionSafeHandle handle, out PdfContentOperationSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperation_GetOperationType(PdfContentOperationSafeHandle handle, out Int32 data);

        #endregion

        #region ContentOperationGeneric

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationGeneric_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationGeneric_ToContentOperation(PdfContentOperationGenericSafeHandle handle, out PdfContentOperationSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationGeneric_FromContentOperation(PdfContentOperationSafeHandle handle, out PdfContentOperationGenericSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationGeneric_GetOperator(PdfContentOperationGenericSafeHandle handle, out PdfContentOperatorSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationGeneric_GetOperandsSize(PdfContentOperationGenericSafeHandle handle, out UIntPtr data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationGeneric_GetOperandAt(PdfContentOperationGenericSafeHandle handle, UIntPtr at, out PdfObjectSafeHandle data);

        #endregion

        #region ContentOperationTextShow

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextShow_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextShow_ToContentOperation(PdfContentOperationTextShowSafeHandle handle, out PdfContentOperationSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextShow_FromContentOperation(PdfContentOperationSafeHandle handle, out PdfContentOperationTextShowSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextShow_GetValue(PdfContentOperationTextShowSafeHandle handle, out PdfStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextShow_SetValue(PdfContentOperationTextShowSafeHandle handle, PdfStringObjectSafeHandle data);

        #endregion

        #region ContentOperationTextShowArray

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextShowArray_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextShowArray_ToContentOperation(PdfContentOperationTextShowArraySafeHandle handle, out PdfContentOperationSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextShowArray_FromContentOperation(PdfContentOperationSafeHandle handle, out PdfContentOperationTextShowArraySafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextShowArray_GetValue(PdfContentOperationTextShowArraySafeHandle handle, out PdfArrayObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextShowArray_SetValue(PdfContentOperationTextShowArraySafeHandle handle, PdfArrayObjectSafeHandle data);

        #endregion

        #region ContentOperationTextFont

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextFont_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextFont_ToContentOperation(PdfContentOperationTextFontSafeHandle handle, out PdfContentOperationSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextFont_FromContentOperation(PdfContentOperationSafeHandle handle, out PdfContentOperationTextFontSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextFont_GetName(PdfContentOperationTextFontSafeHandle handle, out PdfNameObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextFont_SetName(PdfContentOperationTextFontSafeHandle handle, PdfNameObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextFont_GetScale(PdfContentOperationTextFontSafeHandle handle, out PdfRealObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 ContentOperationTextFont_SetScale(PdfContentOperationTextFontSafeHandle handle, PdfRealObjectSafeHandle data);

        #endregion

        #region BaseFontRange

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseFontRange_Release(IntPtr handle);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseFontRange_Create(out PdfBaseFontRangeSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseFontRange_GetRangeLow(PdfBaseFontRangeSafeHandle handle, out PdfHexadecimalStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseFontRange_SetRangeLow(PdfBaseFontRangeSafeHandle handle, PdfHexadecimalStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseFontRange_GetRangeHigh(PdfBaseFontRangeSafeHandle handle, out PdfHexadecimalStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseFontRange_SetRangeHigh(PdfBaseFontRangeSafeHandle handle, PdfHexadecimalStringObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseFontRange_GetDestination(PdfBaseFontRangeSafeHandle handle, out PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseFontRange_SetDestination(PdfBaseFontRangeSafeHandle handle, PdfObjectSafeHandle data);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern UInt32 BaseFontRange_Contains(PdfBaseFontRangeSafeHandle handle, PdfBufferSafeHandle data, [MarshalAs(UnmanagedType.I1)] out bool result);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern UInt32 BaseFontRange_GetMappedValue(PdfBaseFontRangeSafeHandle handle, PdfBufferSafeHandle data, out PdfBufferSafeHandle result);

        #endregion
    }
}

#endif
