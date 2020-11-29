using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// The null object has a type and value that are unequal to those of any other object
    /// </summary>
    public class PdfNullObject : PdfObject
    {
        internal PdfNullObject(PdfNullObjectSafeHandle handle) : base(handle)
        {
        }

        static PdfNullObject()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
        }

        /// <summary>
        /// Create a new instance of \ref PdfNullObject with default value
        /// </summary>
        /// <returns>New instance of \ref PdfNullObject on success, throws exception on failure</returns>
        public static PdfNullObject Create()
        {
            UInt32 result = NativeMethods.NullObject_Create(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfNullObject(data);
        }

        /// <summary>
        /// Convert object to null object
        /// </summary>
        /// <param name="data">Handle to \ref PdfObject to be converted</param>
        /// <returns>A new instance of \ref PdfNullObject if the object can be converted, throws exception on failure</returns>
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
