using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// Atomic operation modifying graphics state parameters.
    /// </summary>
    public class PdfContentOperation : PdfContentInstruction
    {
        internal PdfContentOperationSafeHandle OperationHandle { get; }

        internal PdfContentOperation(PdfContentOperationSafeHandle handle) : base(handle)
        {
            OperationHandle = handle;
        }

        static PdfContentOperation()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentOperationSafeHandle).TypeHandle);
        }

        public PdfContentOperationType OperationType
        { 
            get { return GetOperationType(); }
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of current content operation on success, throws exception on failure</returns>
        public PdfContentOperationType GetOperationType()
        {
            UInt32 result = NativeMethods.ContentOperation_GetOperationType(OperationHandle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfContentOperationType>.CheckedCast(data);
        }

        /// <summary>
        /// Convert content instruction to content operation
        /// </summary>
        /// <param name="data">Handle to \ref PdfContentInstruction to be converted</param>
        /// <returns>A new instance of \ref PdfContentOperation if the object can be converted, throws exception on failure</returns>
        public static PdfContentOperation FromContentInstruction(PdfContentInstruction data)
        {
            return new PdfContentOperation(data.InstructionHandle);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            OperationHandle?.Dispose();
        }

        private static class NativeMethods
        {
            public static GetOperationTypeDelgate ContentOperation_GetOperationType = LibraryInstance.GetFunction<GetOperationTypeDelgate>("ContentOperation_GetOperationType");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOperationTypeDelgate(PdfContentOperationSafeHandle handle, out Int32 data);
        }
    }
}
