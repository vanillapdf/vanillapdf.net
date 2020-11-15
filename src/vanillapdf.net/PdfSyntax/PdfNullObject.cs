using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    public class PdfNullObject : PdfObject
    {
        internal PdfNullObject(PdfNullObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfNullObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        public static PdfNullObject Create()
        {
            UInt32 result = NativeMethods.NullObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNullObject(data);
        }

        public static PdfNullObject FromObject(PdfObject data)
        {
            return new PdfNullObject(data.Handle);
        }

        private static class NativeMethods
        {
            public static CreateDelgate NullObject_Create = LibraryInstance.GetFunction<CreateDelgate>("NullObject_Create");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateDelgate(out PdfNullObjectSafeHandle handle);
        }
    }
}
