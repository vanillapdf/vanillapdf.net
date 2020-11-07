using System;
using System.Runtime.InteropServices;
using static vanillapdf.net.Utils.MiscUtils;

namespace vanillapdf.net.Utils.SafeHandles
{
    internal sealed class ContentInstructionSafeHandle : PdfSafeHandle
    {
        private static GenericReleaseDelgate StaticReleaseDelegate = LibraryInstance.GetFunction<GenericReleaseDelgate>("ContentInstruction_Release");
        protected override GenericReleaseDelgate ReleaseDelegate => StaticReleaseDelegate;

        private static ConvertToUnknownDelegate Convert_ToUnknown = LibraryInstance.GetFunction<ConvertToUnknownDelegate>("ContentInstruction_ToUnknown");
        private static ConvertFromUnknownDelegate Convert_FromUnknown = LibraryInstance.GetFunction<ConvertFromUnknownDelegate>("ContentInstruction_FromUnknown");

        [UnmanagedFunctionPointer(LibraryCallingConvention)]
        private delegate UInt32 ConvertToUnknownDelegate(ContentInstructionSafeHandle handle, out PdfUnknownSafeHandle data);

        [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
        private delegate UInt32 ConvertFromUnknownDelegate(PdfUnknownSafeHandle handle, out ContentInstructionSafeHandle data);

        public static implicit operator PdfUnknownSafeHandle(ContentInstructionSafeHandle handle)
        {
            UInt32 result = Convert_ToUnknown(handle, out PdfUnknownSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        public static implicit operator ContentInstructionSafeHandle(PdfUnknownSafeHandle handle)
        {
            UInt32 result = Convert_FromUnknown(handle, out ContentInstructionSafeHandle data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }
    }
}
