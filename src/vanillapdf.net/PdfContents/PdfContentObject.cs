using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// A sequence of content instructions grouped within a single object.
    /// </summary>
    public class PdfContentObject : PdfContentInstruction
    {
        internal PdfContentObjectSafeHandle ObjectHandle { get; }

        internal PdfContentObject(PdfContentObjectSafeHandle handle) : base(handle)
        {
            ObjectHandle = handle;
        }

        static PdfContentObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentObjectSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get derived type of current object
        /// </summary>
        /// <returns>Type of derived object on success, throws exception on failure</returns>
        public PdfContentObjectType GetObjectType()
        {
            UInt32 result = NativeMethods.ContentObject_GetObjectType(ObjectHandle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfContentObjectType>.CheckedCast(data);
        }

        /// <summary>
        /// Convert content instruction to content object
        /// </summary>
        /// <param name="data">Handle to \ref PdfContentInstruction to be converted</param>
        /// <returns>A new instance of \ref PdfContentObject if the object can be converted, throws exception on failure</returns>
        public static PdfContentObject FromContentInstruction(PdfContentInstruction data)
        {
            return new PdfContentObject(data.InstructionHandle);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            ObjectHandle?.Dispose();
        }

        private static class NativeMethods
        {
            public static GetTypeDelgate ContentObject_GetObjectType = LibraryInstance.GetFunction<GetTypeDelgate>("ContentObject_GetObjectType");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetTypeDelgate(PdfContentObjectSafeHandle handle, out Int32 data);
        }
    }
}
