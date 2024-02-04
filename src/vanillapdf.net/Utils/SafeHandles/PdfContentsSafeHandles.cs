using System;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using static vanillapdf.net.Utils.MiscUtils;

namespace vanillapdf.net.Utils
{
    internal sealed class PdfContentInstructionSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentInstruction_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentInstruction_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentInstruction_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentInstructionSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfContentInstructionSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfContentInstructionSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentInstructionSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfContentInstructionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfContentInstructionCollectionSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentInstructionCollection_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentInstructionCollection_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentInstructionCollection_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentInstructionCollectionSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfContentInstructionCollectionSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfContentInstructionCollectionSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentInstructionCollectionSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfContentInstructionCollectionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfContentInstructionCollectionIteratorSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentInstructionCollectionIterator_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentInstructionCollectionIterator_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentInstructionCollectionIterator_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentInstructionCollectionIteratorSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfContentInstructionCollectionIteratorSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfContentInstructionCollectionIteratorSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentInstructionCollectionIteratorSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfContentInstructionCollectionIteratorSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    #region ContentOperators

    internal sealed class PdfContentOperatorSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentOperator_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentOperator_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentOperator_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentOperatorSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfContentOperatorSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfContentOperatorSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentOperatorSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfContentOperatorSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    #endregion

    internal sealed class PdfContentObjectSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentObject_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToInstruction = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentObject_ToInstruction");
        private static ConvertFromUnknownDelegate Convert_FromInstruction = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentObject_FromInstruction");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentObjectSafeHandle handle, out PdfContentInstructionSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfContentInstructionSafeHandle handle, out PdfContentObjectSafeHandle data);

        public static implicit operator PdfContentInstructionSafeHandle(PdfContentObjectSafeHandle handle)
        {
            UInt32 result = Convert_ToInstruction(handle, out PdfContentInstructionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentObjectSafeHandle(PdfContentInstructionSafeHandle handle)
        {
            UInt32 result = Convert_FromInstruction(handle, out PdfContentObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfContentObjectSafeHandle handle)
        {
            return (PdfContentInstructionSafeHandle)handle;
        }

        public static implicit operator PdfContentObjectSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfContentInstructionSafeHandle)handle;
        }
    }

    internal sealed class PdfContentObjectTextSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentObjectText_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate ContentObjectText_ToContentObject = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentObjectText_ToContentObject");
        private static ConvertFromUnknownDelegate ContentObjectText_FromContentObject = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentObjectText_FromContentObject");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentObjectTextSafeHandle handle, out PdfContentObjectSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfContentObjectSafeHandle handle, out PdfContentObjectTextSafeHandle data);

        public static implicit operator PdfContentObjectSafeHandle(PdfContentObjectTextSafeHandle handle)
        {
            UInt32 result = ContentObjectText_ToContentObject(handle, out PdfContentObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentObjectTextSafeHandle(PdfContentObjectSafeHandle handle)
        {
            UInt32 result = ContentObjectText_FromContentObject(handle, out PdfContentObjectTextSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentInstructionSafeHandle(PdfContentObjectTextSafeHandle handle)
        {
            return (PdfContentObjectSafeHandle)handle;
        }

        public static implicit operator PdfContentObjectTextSafeHandle(PdfContentInstructionSafeHandle handle)
        {
            return (PdfContentObjectSafeHandle)handle;
        }

        public static implicit operator PdfUnknownSafeHandle(PdfContentObjectTextSafeHandle handle)
        {
            return (PdfContentObjectSafeHandle)handle;
        }

        public static implicit operator PdfContentObjectTextSafeHandle(PdfUnknownSafeHandle handle)
        {
            return (PdfContentObjectSafeHandle)handle;
        }
    }

    internal sealed class PdfContentOperationSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentOperation_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToInstruction = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentOperation_ToInstruction");
        private static ConvertFromUnknownDelegate Convert_FromInstruction = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentOperation_FromInstruction");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentOperationSafeHandle handle, out PdfContentInstructionSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfContentInstructionSafeHandle handle, out PdfContentOperationSafeHandle data);

        public static implicit operator PdfContentInstructionSafeHandle(PdfContentOperationSafeHandle handle)
        {
            UInt32 result = Convert_ToInstruction(handle, out PdfContentInstructionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentOperationSafeHandle(PdfContentInstructionSafeHandle handle)
        {
            UInt32 result = Convert_FromInstruction(handle, out PdfContentOperationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfContentOperationGenericSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentOperationGeneric_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate ContentOperationGeneric_ToContentOperation = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentOperationGeneric_ToContentOperation");
        private static ConvertFromUnknownDelegate ContentOperationGeneric_FromContentOperation = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentOperationGeneric_FromContentOperation");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentOperationGenericSafeHandle handle, out PdfContentOperationSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfContentOperationSafeHandle handle, out PdfContentOperationGenericSafeHandle data);

        public static implicit operator PdfContentOperationSafeHandle(PdfContentOperationGenericSafeHandle handle)
        {
            UInt32 result = ContentOperationGeneric_ToContentOperation(handle, out PdfContentOperationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentOperationGenericSafeHandle(PdfContentOperationSafeHandle handle)
        {
            UInt32 result = ContentOperationGeneric_FromContentOperation(handle, out PdfContentOperationGenericSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfContentOperationTextShowSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentOperationTextShow_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate ContentOperationTextShow_ToContentOperation = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentOperationTextShow_ToContentOperation");
        private static ConvertFromUnknownDelegate ContentOperationTextShow_FromContentOperation = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentOperationTextShow_FromContentOperation");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentOperationTextShowSafeHandle handle, out PdfContentOperationSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfContentOperationSafeHandle handle, out PdfContentOperationTextShowSafeHandle data);

        public static implicit operator PdfContentOperationSafeHandle(PdfContentOperationTextShowSafeHandle handle)
        {
            UInt32 result = ContentOperationTextShow_ToContentOperation(handle, out PdfContentOperationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentOperationTextShowSafeHandle(PdfContentOperationSafeHandle handle)
        {
            UInt32 result = ContentOperationTextShow_FromContentOperation(handle, out PdfContentOperationTextShowSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfContentOperationTextShowArraySafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentOperationTextShowArray_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate ContentOperationShowText_ToContentOperation = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentOperationTextShowArray_ToContentOperation");
        private static ConvertFromUnknownDelegate ContentOperationShowText_FromContentOperation = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentOperationTextShowArray_FromContentOperation");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentOperationTextShowArraySafeHandle handle, out PdfContentOperationSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfContentOperationSafeHandle handle, out PdfContentOperationTextShowArraySafeHandle data);

        public static implicit operator PdfContentOperationSafeHandle(PdfContentOperationTextShowArraySafeHandle handle)
        {
            UInt32 result = ContentOperationShowText_ToContentOperation(handle, out PdfContentOperationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentOperationTextShowArraySafeHandle(PdfContentOperationSafeHandle handle)
        {
            UInt32 result = ContentOperationShowText_FromContentOperation(handle, out PdfContentOperationTextShowArraySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfContentOperationTextFontSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentOperationTextFont_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate ContentOperationTextFont_ToContentOperation = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentOperationTextFont_ToContentOperation");
        private static ConvertFromUnknownDelegate ContentOperationTextFont_FromContentOperation = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentOperationTextFont_FromContentOperation");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentOperationTextFontSafeHandle handle, out PdfContentOperationSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfContentOperationSafeHandle handle, out PdfContentOperationTextFontSafeHandle data);

        public static implicit operator PdfContentOperationSafeHandle(PdfContentOperationTextFontSafeHandle handle)
        {
            UInt32 result = ContentOperationTextFont_ToContentOperation(handle, out PdfContentOperationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentOperationTextFontSafeHandle(PdfContentOperationSafeHandle handle)
        {
            UInt32 result = ContentOperationTextFont_FromContentOperation(handle, out PdfContentOperationTextFontSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    internal sealed class PdfContentParserSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentParser_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentParser_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentParser_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfContentParserSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfContentParserSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfContentParserSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfContentParserSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfContentParserSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    #region Character map data

    internal sealed class PdfBaseFontRangeSafeHandle : PdfSafeHandle
    {
        private static readonly GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("BaseFontRange_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static readonly ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("BaseFontRange_ToUnknown");
        private static readonly ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("BaseFontRange_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(PdfBaseFontRangeSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out PdfBaseFontRangeSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(PdfBaseFontRangeSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator PdfBaseFontRangeSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out PdfBaseFontRangeSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }

    #endregion
}
