using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;
using vanillapdf.net.Utils.SafeHandles;

namespace vanillapdf.net
{
    public class PdfContentOperationGeneric : PdfContentOperation
    {
        internal PdfContentOperationGeneric(PdfContentOperationGenericSafeHandle handle) : base(handle)
        {
        }

        static PdfContentOperationGeneric()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfContentOperator GetOperator()
        {
            UInt32 result = NativeMethods.ContentOperationGeneric_GetOperator(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentOperator(data);
        }

        public UInt64 GetOperandsSize()
        {
            UInt32 result = NativeMethods.ContentOperationGeneric_GetOperandsSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        public PdfObject GetOperandAt(UInt64 index)
        {
            UInt32 result = NativeMethods.ContentOperationGeneric_GetOperandAt(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        public static PdfContentOperationGeneric FromContentOperation(PdfContentOperation data)
        {
            return new PdfContentOperationGeneric(data.Handle);
        }

        private static class NativeMethods
        {
            public static GetOperatorDelgate ContentOperationGeneric_GetOperator = LibraryInstance.GetFunction<GetOperatorDelgate>("ContentOperationGeneric_GetOperator");
            public static GetOperandsSizeDelgate ContentOperationGeneric_GetOperandsSize = LibraryInstance.GetFunction<GetOperandsSizeDelgate>("ContentOperationGeneric_GetOperandsSize");
            public static GetOperandAtDelgate ContentOperationGeneric_GetOperandAt = LibraryInstance.GetFunction<GetOperandAtDelgate>("ContentOperationGeneric_GetOperandAt");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOperatorDelgate(PdfContentOperationGenericSafeHandle handle, out PdfContentOperatorSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOperandsSizeDelgate(PdfContentOperationGenericSafeHandle handle, out UIntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOperandAtDelgate(PdfContentOperationGenericSafeHandle handle, UIntPtr at, out PdfObjectSafeHandle data);
        }
    }
}
