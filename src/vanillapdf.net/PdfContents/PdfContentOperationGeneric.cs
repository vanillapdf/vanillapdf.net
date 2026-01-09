using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Unresolved operation often containing unknown operator.
    /// </summary>
    public class PdfContentOperationGeneric : PdfContentOperation
    {
        internal PdfContentOperationGenericSafeHandle Handle { get; }

        internal PdfContentOperationGeneric(PdfContentOperationGenericSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfContentOperationGeneric()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperationGenericSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get operator in the current content stream operation
        /// </summary>
        /// <returns>Handle to content stream operator on success, throws exception on failure</returns>
        public PdfContentOperator GetOperator()
        {
            UInt32 result = NativeMethods.ContentOperationGeneric_GetOperator(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentOperator(data);
        }

        /// <summary>
        /// Get number of operands in the current content stream operation
        /// </summary>
        /// <returns>Number of operands on success, throws exception on failure</returns>
        public UInt64 GetOperandsSize()
        {
            UInt32 result = NativeMethods.ContentOperationGeneric_GetOperandsSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get operand at index in the current content stream operation
        /// </summary>
        /// <param name="index">Index of operand to be returned</param>
        /// <returns>Operand at <p>index</p> on success, throws exception on failure</returns>
        public PdfObject GetOperandAt(UInt64 index)
        {
            UInt32 result = NativeMethods.ContentOperationGeneric_GetOperandAt(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfObject(data);
        }

        /// <summary>
        /// Get operand at index in the current content stream operation
        /// </summary>
        /// <typeparam name="T">Type of object the result shall be converted to</typeparam>
        /// <param name="index">Index of operand to be returned</param>
        /// <returns>Operand at <p>index</p> on success, throws exception on failure</returns>
        public T GetOperandAtAs<T>(UInt64 index) where T : PdfObject
        {
            var result = GetOperandAt(index);
            return (T)result.ConvertTo<T>();
        }

        /// <summary>
        /// Convert content operation to a generic type operation
        /// </summary>
        /// <param name="data">Handle to \ref PdfContentOperation to be converted</param>
        /// <returns>A new instance of \ref PdfContentOperationGeneric if the object can be converted, throws exception on failure</returns>
        public static PdfContentOperationGeneric FromContentOperation(PdfContentOperation data)
        {
            return new PdfContentOperationGeneric(data.OperationHandle);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle.Dispose();
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
