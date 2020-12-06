using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// A PDF text object consists of operators that may show text strings, move the text position, and set text state and certain other parameters.
    /// </summary>
    public class PdfContentObjectText : PdfContentObject
    {
        internal PdfContentObjectText(PdfContentObjectTextSafeHandle handle) : base(handle)
        {
        }

        static PdfContentObjectText()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentObjectTextSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get number of operations within current text object
        /// </summary>
        /// <returns>Number of operations within current text object on success, throws exception on failure</returns>
        public UInt64 GetOperationsSize()
        {
            UInt32 result = NativeMethods.ContentObjectText_GetOperationsSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get operation at index
        /// </summary>
        /// <returns>Operation at <p>index</p> on success, throws exception on failure</returns>
        public PdfContentOperation GetOperationAt(UInt64 index)
        {
            UInt32 result = NativeMethods.ContentObjectText_GetOperationAt(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentOperation(data);
        }

        /// <summary>
        /// Convert content object to content text object
        /// </summary>
        /// <param name="data">Handle to \ref PdfContentObject to be converted</param>
        /// <returns>A new instance of \ref PdfContentObjectText if the object can be converted, throws exception on failure</returns>
        public static PdfContentObjectText FromContentObject(PdfContentObject data)
        {
            return new PdfContentObjectText(data.Handle);
        }

        private static class NativeMethods
        {
            public static GetOperationsSizeDelgate ContentObjectText_GetOperationsSize = LibraryInstance.GetFunction<GetOperationsSizeDelgate>("ContentObjectText_GetOperationsSize");
            public static GetOperationAtDelgate ContentObjectText_GetOperationAt = LibraryInstance.GetFunction<GetOperationAtDelgate>("ContentObjectText_GetOperationAt");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOperationsSizeDelgate(PdfContentObjectTextSafeHandle handle, out UIntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetOperationAtDelgate(PdfContentObjectTextSafeHandle handle, UIntPtr at, out PdfContentOperationSafeHandle data);
        }
    }
}
