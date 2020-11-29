using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    public enum PdfContentObjectType
    {
        Undefined = 0,
        Text,
        InlineImage
    };

    public class PdfContentObject : PdfContentInstruction
    {
        internal PdfContentObject(PdfContentObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfContentObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentObjectSafeHandle).TypeHandle);
        }

        public PdfContentObjectType GetObjectType()
        {
            UInt32 result = NativeMethods.ContentObject_GetObjectType(Handle, out Int32 data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfContentObjectType>.CheckedCast(data);
        }

        public static PdfContentObject FromContentInstruction(PdfContentInstruction data)
        {
            return new PdfContentObject(data.Handle);
        }

        private static class NativeMethods
        {
            public static GetTypeDelgate ContentObject_GetObjectType = LibraryInstance.GetFunction<GetTypeDelgate>("ContentObject_GetObjectType");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetTypeDelgate(PdfContentObjectSafeHandle handle, out Int32 data);
        }
    }
}
