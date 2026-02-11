using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Utils
{
    internal sealed class PdfContentInstructionSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ContentInstruction_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfContentInstructionCollectionSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ContentInstructionCollection_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    internal sealed class PdfContentInstructionCollectionIteratorSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ContentInstructionCollectionIterator_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    #region ContentOperators

    internal sealed class PdfContentOperatorSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ContentOperator_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    #endregion

    internal sealed class PdfContentObjectSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ContentObject_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfContentInstructionSafeHandle(PdfContentObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentObject_ToInstruction(handle, out PdfContentInstructionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfContentObjectSafeHandle(PdfContentInstructionSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentObject_FromInstruction(handle, out PdfContentObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfContentObjectTextSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ContentObjectText_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfContentObjectSafeHandle(PdfContentObjectTextSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentObjectText_ToContentObject(handle, out PdfContentObjectSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfContentObjectTextSafeHandle(PdfContentObjectSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentObjectText_FromContentObject(handle, out PdfContentObjectTextSafeHandle data);
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
    }

    internal sealed class PdfContentOperationSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ContentOperation_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfContentInstructionSafeHandle(PdfContentOperationSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentOperation_ToInstruction(handle, out PdfContentInstructionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfContentOperationSafeHandle(PdfContentInstructionSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentOperation_FromInstruction(handle, out PdfContentOperationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfContentOperationGenericSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ContentOperationGeneric_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfContentOperationSafeHandle(PdfContentOperationGenericSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentOperationGeneric_ToContentOperation(handle, out PdfContentOperationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfContentOperationGenericSafeHandle(PdfContentOperationSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentOperationGeneric_FromContentOperation(handle, out PdfContentOperationGenericSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfContentOperationTextShowSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ContentOperationTextShow_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfContentOperationSafeHandle(PdfContentOperationTextShowSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentOperationTextShow_ToContentOperation(handle, out PdfContentOperationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfContentOperationTextShowSafeHandle(PdfContentOperationSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentOperationTextShow_FromContentOperation(handle, out PdfContentOperationTextShowSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfContentOperationTextShowArraySafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ContentOperationTextShowArray_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfContentOperationSafeHandle(PdfContentOperationTextShowArraySafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentOperationTextShowArray_ToContentOperation(handle, out PdfContentOperationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfContentOperationTextShowArraySafeHandle(PdfContentOperationSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentOperationTextShowArray_FromContentOperation(handle, out PdfContentOperationTextShowArraySafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfContentOperationTextFontSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ContentOperationTextFont_Release(handle) == PdfReturnValues.ERROR_SUCCESS;

        public static implicit operator PdfContentOperationSafeHandle(PdfContentOperationTextFontSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentOperationTextFont_ToContentOperation(handle, out PdfContentOperationSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }

        public static implicit operator PdfContentOperationTextFontSafeHandle(PdfContentOperationSafeHandle handle)
        {
            UInt32 result = NativeMethods.ContentOperationTextFont_FromContentOperation(handle, out PdfContentOperationTextFontSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return data;
        }
    }

    internal sealed class PdfContentParserSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.ContentParser_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    #region Character map data

    internal sealed class PdfBaseFontRangeSafeHandle : PdfSafeHandle
    {
        protected override bool ReleaseHandle() => NativeMethods.BaseFontRange_Release(handle) == PdfReturnValues.ERROR_SUCCESS;
    }

    #endregion
}
