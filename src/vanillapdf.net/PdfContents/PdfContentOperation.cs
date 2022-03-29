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
    public class PdfContentOperation : PdfUnknown
    {
        internal PdfContentOperation(PdfContentOperationSafeHandle handle) : base(handle)
        {
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
            UInt32 result = NativeMethods.ContentOperation_GetOperationType(Handle, out Int32 data);
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
            return new PdfContentOperation(data.Handle);
        }

        private static class NativeMethods
        {
            public static GetInstructionTypeDelgate ContentOperation_GetOperationType = LibraryInstance.GetFunction<GetInstructionTypeDelgate>("ContentOperation_GetOperationType");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetInstructionTypeDelgate(PdfContentInstructionSafeHandle handle, out Int32 data);
        }
    }
}
