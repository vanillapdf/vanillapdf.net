using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.Utils;
using vanillapdf.net.Utils.SafeHandles;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfObject : PdfUnknown
    {
        internal PdfObject(PdfObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public PdfObjectType GetObjectType()
        {
            UInt32 result = NativeMethods.Object_GetType(Handle, out PdfObjectType data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfObjectType>.CheckedCast(data);
        }

        private static class NativeMethods
        {
            public static GetTypeDelgate Object_GetType = LibraryInstance.GetFunction<GetTypeDelgate>("Object_GetType");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetTypeDelgate(PdfObjectSafeHandle handle, out PdfObjectType data);
        }
    }
}
